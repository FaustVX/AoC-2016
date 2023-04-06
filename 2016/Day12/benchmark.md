``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |      Mean |     Error |    StdDev | Allocated |
|-------- |----------:|----------:|----------:|----------:|
| PartOne |  3.070 ms | 0.0604 ms | 0.1119 ms |   1.38 KB |
| PartTwo | 91.128 ms | 1.8173 ms | 5.0356 ms |   1.46 KB |
