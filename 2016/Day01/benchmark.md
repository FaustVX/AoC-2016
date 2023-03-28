``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |      Mean |     Error |    StdDev |   Gen0 |   Gen1 | Allocated |
|-------- |----------:|----------:|----------:|-------:|-------:|----------:|
| PartOne |  2.523 μs | 0.0499 μs | 0.0875 μs |      - |      - |      24 B |
| PartTwo | 14.129 μs | 0.1245 μs | 0.0972 μs | 2.7466 | 0.2441 |   34504 B |
