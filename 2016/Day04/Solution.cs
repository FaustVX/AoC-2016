#nullable enable
using System.Collections;

namespace AdventOfCode.Y2016.Day04;

[ProblemName("Security Through Obscurity")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var sum = 0;
        var mem = input.AsMemory();
        while (true)
        {
            var eol = mem.Span.IndexOf('\n');
            sum += IsARealRoom(eol is > 0 ? mem[..eol] : mem);
            if (eol < 0)
                return sum;
            mem = mem[(eol + 1)..];
        }
    }

    static int IsARealRoom(ReadOnlyMemory<char> line)
    {
        var name = line[..^11]; // 11 because "-123[abcde]" is 11 length from the end
        var checksum = line[^6..^1];
        var groups = name.ToArray()
            .Where(static c => c != '-')
            .GroupBy(static c => c)
            .OrderByDescending(static g => g.Count()).ThenBy(static g => g.Key)
            .Select(static g => g.Key)
            .Take(5)
            .ToArray();
        return checksum.Span.SequenceEqual(groups) ? int.Parse(line.Span[^10..^7]) : 0;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
