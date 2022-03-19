Testing the performance of various Key/Value collections and item update workloads. Current results:

| Method                 | Size      |       Mean |     Error |    StdDev |     Median |
| ---------------------- | --------- | ---------: | --------: | --------: | ---------: |
| Map                    | 10        |   986.3 ns |  19.51 ns |  38.06 ns |   972.5 ns |
| Dictionary             | 10        |   253.2 ns |   3.57 ns |   3.17 ns |   252.4 ns |
| ValueWrappedDictionary | 10        |   104.8 ns |   1.99 ns |   1.86 ns |   104.0 ns |
| DictionaryGetRef       | 10        |   119.3 ns |   1.58 ns |   1.48 ns |   119.0 ns |
| Map                    | 100       | 2,062.4 ns |  39.33 ns |  36.79 ns | 2,053.8 ns |
| Dictionary             | 100       |   325.2 ns |   6.18 ns |   6.34 ns |   324.7 ns |
| ValueWrappedDictionary | 100       |   132.3 ns |   2.08 ns |   1.95 ns |   131.7 ns |
| DictionaryGetRef       | 100       |   124.5 ns |   2.52 ns |   3.10 ns |   124.2 ns |
| Map                    | 1_000     | 3,468.0 ns |  68.38 ns | 130.09 ns | 3,394.7 ns |
| Dictionary             | 1_000     |   299.8 ns |   5.99 ns |  10.02 ns |   298.1 ns |
| ValueWrappedDictionary | 1_000     |   132.2 ns |   1.06 ns |   0.94 ns |   132.2 ns |
| DictionaryGetRef       | 1_000     |   130.9 ns |   2.52 ns |   2.69 ns |   130.1 ns |
| Map                    | 10_000    | 4,383.1 ns |  86.00 ns |  80.45 ns | 4,393.2 ns |
| Dictionary             | 10_000    |   322.4 ns |   2.15 ns |   1.79 ns |   322.6 ns |
| ValueWrappedDictionary | 10_000    |   150.8 ns |   0.90 ns |   0.80 ns |   150.6 ns |
| DictionaryGetRef       | 10_000    |   141.4 ns |   2.12 ns |   1.77 ns |   140.9 ns |
| Map                    | 100_000   | 5,571.8 ns |  90.32 ns |  84.48 ns | 5,569.3 ns |
| Dictionary             | 100_000   |   338.0 ns |   2.71 ns |   2.26 ns |   338.6 ns |
| ValueWrappedDictionary | 100_000   |   151.3 ns |   0.75 ns |   0.63 ns |   151.4 ns |
| DictionaryGetRef       | 100_000   |   130.5 ns |   1.13 ns |   1.05 ns |   130.2 ns |
| Map                    | 1_000_000 | 7,695.1 ns | 150.65 ns | 263.86 ns | 7,690.7 ns |
| Dictionary             | 1_000_000 |   369.2 ns |   2.69 ns |   2.10 ns |   369.5 ns |
| ValueWrappedDictionary | 1_000_000 |   153.3 ns |   1.21 ns |   1.13 ns |   153.2 ns |
| DictionaryGetRef       | 1_000_000 |   152.1 ns |   1.09 ns |   0.91 ns |   152.1 ns |