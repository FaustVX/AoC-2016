``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |     Mean |   Error |  StdDev | Allocated |
|-------- |---------:|--------:|--------:|----------:|
| PartOne | 176.0 μs | 3.41 μs | 6.97 μs |      24 B |
| PartTwo | 180.0 μs | 3.54 μs | 3.64 μs |      24 B |
