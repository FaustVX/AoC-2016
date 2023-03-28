``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |     Mean |    Error |   StdDev | Allocated |
|-------- |---------:|---------:|---------:|----------:|
| PartOne | 15.80 μs | 0.303 μs | 0.815 μs |      48 B |
| PartTwo | 16.43 μs | 0.323 μs | 0.317 μs |     272 B |
