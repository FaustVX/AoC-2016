#nullable enable
namespace AdventOfCode.Y2016.Day03;

[ProblemName("Squares With Three Sides")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var count = 0;
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            var (a, b, c) = ParseTriangle(line);
            if (a + b > c && a + c >= b && b + c > a)
                count++;
        }
        return count;
    }

    static (int, int, int) ParseTriangle(ReadOnlySpan<char> span)
    => (int.Parse(span.Slice(0, 5)), int.Parse(span.Slice(5, 5)), int.Parse(span.Slice(10, 5)));

    public object PartTwo(string input)
    {
        return 0;
    }
}
