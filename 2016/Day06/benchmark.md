``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1696/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.104
  [Host]     : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.4 (7.0.423.11508), X64 RyuJIT AVX2


```
|  Method |     Mean |     Error |    StdDev | Allocated |
|-------- |---------:|----------:|----------:|----------:|
| PartOne | 2.276 ms | 0.0201 ms | 0.0178 ms |      42 B |
| PartTwo | 2.321 ms | 0.0464 ms | 0.1301 ms |      42 B |
