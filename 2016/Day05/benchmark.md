``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |    Mean |    Error |   StdDev | Allocated |
|-------- |--------:|---------:|---------:|----------:|
| PartOne | 3.809 s | 0.0754 s | 0.1007 s |      1 KB |
| PartTwo | 6.000 s | 0.1173 s | 0.1396 s |   1.45 KB |
