``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |     Mean |    Error |   StdDev |   Gen0 | Allocated |
|-------- |---------:|---------:|---------:|-------:|----------:|
| PartOne | 57.10 μs | 1.012 μs | 0.947 μs |      - |     352 B |
| PartTwo | 61.48 μs | 0.845 μs | 0.749 μs | 0.7324 |   10608 B |
