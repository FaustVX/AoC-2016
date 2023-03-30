#nullable enable
namespace AdventOfCode.Y2016.Day07;

[ProblemName("Internet Protocol Version 7")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var count = 0;
        foreach (var ip in input.AsSpan().EnumerateLines())
            if (SupportsTLS(ip))
                count++;
        return count;
    }

    static bool SupportsTLS(ReadOnlySpan<char> ip)
    {
        var isInHypernet = false;
        var supportTLS = false;
        foreach (var sequence in new SpanSplitEnumerator(ip, stackalloc char[] { '[', ']' }, separateOnAny: true))
        {
            if (ContainsABBASequence(sequence))
                if (isInHypernet)
                    return false;
                else
                    supportTLS = true;
            isInHypernet ^= true; // use 'a XOR true' to invert a
        }
        return supportTLS;
    }

    static bool ContainsABBASequence(ReadOnlySpan<char> span)
    {
        for (var i = 3; i < span.Length; i++)
            if (span.Slice(i - 3, 4) is [var a, var b, var c, var d] && a == d && b == c && a != b)
                return true;
        return false;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
