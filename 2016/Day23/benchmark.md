``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |            Mean |         Error |        StdDev | Allocated |
|-------- |----------------:|--------------:|--------------:|----------:|
| PartOne |        424.5 μs |       8.17 μs |       9.41 μs |   1.94 KB |
| PartTwo | 28,403,076.3 μs | 286,412.42 μs | 253,897.15 μs |   2.31 KB |
