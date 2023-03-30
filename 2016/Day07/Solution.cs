#nullable enable
namespace AdventOfCode.Y2016.Day07;

[ProblemName("Internet Protocol Version 7")]
public class Solution : ISolver //, IDisplay
{
    delegate bool Function(ReadOnlySpan<char> ip);

    public object PartOne(string input)
    => Count(input, SupportsTLS);

    static int Count(ReadOnlySpan<char> input, Function func)
    {
        var count = 0;
        foreach (var ip in input.EnumerateLines())
            if (func(ip))
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

    static bool SupportsSSL(ReadOnlySpan<char> ip)
    {
        var isInHypernet = false;
        foreach (var sequence in new SpanSplitEnumerator(ip, stackalloc char[] { '[', ']' }, separateOnAny: true))
        {
            for (var i = 2; i < sequence.Length; i++)
                if (!isInHypernet && IsABASequence(sequence.Slice(i - 2, 3), out var a, out var b) && ContainsBABSequenceInHypernet(ip, a, b))
                    return true;
            isInHypernet ^= true; // use 'a XOR true' to invert a
        }
        return false;
    }

    static bool IsABASequence(ReadOnlySpan<char> span, out char a, out char b)
    {
        if (span is [var c, var d, var e] && c == e && c != d)
        {
            (a, b) = (c, d);
            return true;
        }
        a = b = '\0';
        return false;
    }

    static bool ContainsBABSequenceInHypernet(ReadOnlySpan<char> ip, char a, char b)
    {
        var isInHypernet = false;
        foreach (var sequence in new SpanSplitEnumerator(ip, stackalloc char[] { '[', ']' }, separateOnAny: true))
        {
            if (isInHypernet && ContainsBABSequence(sequence, a, b))
                return true;
            isInHypernet ^= true; // use 'a XOR true' to invert a
        }
        return false;
    }

    static bool ContainsBABSequence(ReadOnlySpan<char> span, char a, char b)
    {
        for (var i = 2; i < span.Length; i++)
            if (span.Slice(i - 2, 3) is [var c, var d, var e] && c == b && d == a && e == b)
                return true;
        return false;
    }

    public object PartTwo(string input)
    => Count(input, SupportsSSL);
}
