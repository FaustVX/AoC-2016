``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |       Mean |    Error |   StdDev |     Gen0 | Allocated |
|-------- |-----------:|---------:|---------:|---------:|----------:|
| PartOne | 1,912.4 μs | 37.60 μs | 57.42 μs | 242.1875 |   2.91 MB |
| PartTwo |   925.2 μs | 18.31 μs | 44.93 μs | 117.1875 |   1.41 MB |
