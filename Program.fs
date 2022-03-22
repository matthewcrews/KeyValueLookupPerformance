open System
open System.Collections.Generic
open System.Runtime.InteropServices
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open BenchmarkDotNet.Diagnosers


// Enum for the different size
[<Struct>]
type Size =
    | ``10`` = 0
    | ``100`` = 1
    | ``1_000`` = 2
    | ``10_000`` = 3
    | ``100_000`` = 4
    | ``1_000_000`` = 5

type ValueWrapper<'T when 'T : struct> (value: 'T) =
    member val Value = value with get, set

type Benchmarks () =

    // The number of lookups I will perform in each test
    let lookupCount = 10
    // A random number generator to create random indices
    // into the collections.
    let rng = Random 1337

    // Lookup array to map Size -> Count
    let sizeToCount =
        [|
            10
            100
            1_000
            10_000
            100_000
            1_000_000
        |]


    // An array of different Arrays for each size in Size
    let arrays =
        sizeToCount
        |> Array.map (fun count ->
            [|1 .. count - 1|]
            |> Array.map (fun i -> string i, ValueWrapper 0.0)
            )

    // An array of different Maps for each size in Size
    let maps =
        sizeToCount
        |> Array.map (fun count ->
            Map [for i in 0 .. count - 1 -> string i, 0.0]
            )

    // An array of different Dictionaries for each size in Size
    let dictionaries =
        sizeToCount
        |> Array.map (fun count ->
            Dictionary [for i in 0 .. count - 1 -> KeyValuePair (string i, 0.0)]
            )

    let wrappedValueDictionaries =
        sizeToCount
        |> Array.map (fun count ->
            Dictionary [for i in 0 .. count - 1 -> KeyValuePair (string i, ValueWrapper 0.0)]
            )

    // Generate a set of random indices for each Size to lookup
    let keysForSize =
        sizeToCount
        |> Array.map (fun keyUpperBound ->
            [|for _ in 0 .. lookupCount - 1 -> string (rng.Next (0, keyUpperBound)) |]
            )


    [<Params(Size.``10``)>]//, Size.``100``, Size.``1_000``, Size.``10_000``, Size.``100_000``, Size.``1_000_000``)>]
    member val Size = Size.``10`` with get, set


    [<Benchmark>]
    member b.Map () =
        // We using mutation to ensure the compiler doesn't eliminate unnecessary work
        let mutable map = maps[int b.Size]
        let keys = keysForSize[int b.Size]

        // We are making memory access pattern as predictable as possible
        // to eliminate cache hits from the work of getting the key. We don't use
        // IEnumerable to reduce the overhead.
        for i = 0 to keys.Length - 1 do
            let key = keys[i]
            let newValue = map[key] + 1.0
            map <- map.Add (key, newValue) // Do a minimal amount of work

        map

    [<Benchmark>]
    member b.Array () =
        // We using mutation to ensure the compiler doesn't eliminate unnecessary work
        let mutable array = arrays[int b.Size]
        let keys = keysForSize[int b.Size]

        for i = 0 to keys.Length - 1 do
            let key = keys[i]
            let index =  array |> Array.findIndex (fun (k,_) -> k = key)
            let _, v = array.[index]
            v.Value <- v.Value + 1.0
        array



    [<Benchmark>]
    member b.Dictionary () =
        let dictionary = dictionaries[int b.Size]
        let keys = keysForSize[int b.Size]

        for i = 0 to keys.Length - 1 do
            let key = keys[i]
            dictionary[key] <- dictionary[key] + 1.0 // Do a minimal amount of work

        dictionary


    [<Benchmark>]
    member b.ValueWrappedDictionary () =
        let valueWrappedDictionary = wrappedValueDictionaries[int b.Size]
        let keys = keysForSize[int b.Size]

        for i = 0 to keys.Length - 1 do
            let key = keys[i]
            let v = valueWrappedDictionary[key]
            v.Value <- v.Value + 1.0 // Do a minimal amount of work

        valueWrappedDictionary


    [<Benchmark>]
    member b.DictionaryGetRef () =
        let dictionary = dictionaries[int b.Size]
        let keys = keysForSize[int b.Size]
        let mutable wasFound = Unchecked.defaultof<_>

        for i = 0 to keys.Length - 1 do
            let key = keys[i]
            let valueRef = &CollectionsMarshal.GetValueRefOrAddDefault (dictionary, key, &wasFound)
            valueRef <- valueRef + 1.0

        dictionary



[<EntryPoint>]
let main _ =

    // I don't care about what Run returns so I'm ignoring it
    let _ = BenchmarkRunner.Run<Benchmarks>()
    0
