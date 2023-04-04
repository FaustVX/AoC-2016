``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |        Mean |       Error |      StdDev |   Gen0 | Allocated |
|-------- |------------:|------------:|------------:|-------:|----------:|
| PartOne |    576.3 ns |    11.52 ns |    18.93 ns | 0.0019 |      24 B |
| PartTwo | 64,961.4 ns | 1,271.68 ns | 2,227.25 ns |      - |      24 B |
