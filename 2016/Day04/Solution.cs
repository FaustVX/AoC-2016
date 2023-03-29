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
            sum += IsARealRoom(eol is > 0 ? mem[..eol] : mem, out _);
            if (eol < 0)
                return sum;
            mem = mem[(eol + 1)..];
        }
    }

    static int IsARealRoom(ReadOnlyMemory<char> line, out char[] name)
    {
        name = line[..^11].ToArray(); // 11 because "-123[abcde]" is 11 length from the end
        var checksum = line[^6..^1];
        var groups = name
            .Where(static c => c != '-')
            .GroupBy(static c => c)
            .OrderByDescending(static g => g.Count()).ThenBy(static g => g.Key)
            .Select(static g => g.Key)
            .Take(5)
            .ToArray();
        return checksum.Span.SequenceEqual(groups) ? int.Parse(line.Span[^10..^7]) : 0;
    }

    static bool Decipher(Span<char> name, int id)
    {
        const string reference = "north";
        foreach (ref var c in name)
            c = DecipherLetter(c, id);
        return name.IndexOf(reference) >= 0;

        static char DecipherLetter(char letter, int id)
        => letter is '-' ? ' ' : (char)(((letter - 'a') + id) % 26 + 'a');
    }

    public object PartTwo(string input)
    {
        var mem = input.AsMemory();
        while (true)
        {
            var eol = mem.Span.IndexOf('\n');
            var id = IsARealRoom(eol is > 0 ? mem[..eol] : mem, out var name);
            mem = mem[(eol + 1)..];
            if (id is 0)
                if (eol < 0)
                    throw new UnreachableException();
                else
                    continue;
            if (Decipher(name, id))
                return id;
            if (eol < 0)
                throw new UnreachableException();
        }
    }
}
