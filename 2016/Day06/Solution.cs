#nullable enable
namespace AdventOfCode.Y2016.Day06;

[ProblemName("Signals and Noise")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var length = input.IndexOf('\n');
        Span<char> word = stackalloc char[length];
        for (int i = 0; i < length; i++)
            word[i] = FindMostCommon(input, i, length + 1);
        return new string(word);
    }

    static char FindMostCommon(ReadOnlySpan<char> span, int column, int length)
    {
        var most = (c: '\0', count: 0);
        for (var i = column; i < span.Length; i += length)
            if (Count(span, column, length, span[i]) is var c && c > most.count)
                most = (span[i], c);
        return most.c;
    }

    static int Count(ReadOnlySpan<char> span, int column, int length, char c)
    {
        var count = 0;
        for (var i = column; i < span.Length; i += length)
            if (span[i] == c)
                count++;
        return count;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
