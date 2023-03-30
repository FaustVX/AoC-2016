#nullable enable
using System.Security.Cryptography;

namespace AdventOfCode.Y2016.Day05;

// Copied from 2015/Day04
[ProblemName("How About a Nice Game of Chess?")]
public class Solution : ISolver //, IDisplay
{
    delegate bool IsValid(Span<byte> span, out byte hexa);
    public object PartOne(string input)
    {
        Span<char> hexa = stackalloc char[8];

        var index = Execute(input, 0, IsValid, out hexa[0]);
        index = Execute(input, index + 1, IsValid, out hexa[1]);
        index = Execute(input, index + 1, IsValid, out hexa[2]);
        index = Execute(input, index + 1, IsValid, out hexa[3]);
        index = Execute(input, index + 1, IsValid, out hexa[4]);
        index = Execute(input, index + 1, IsValid, out hexa[5]);
        index = Execute(input, index + 1, IsValid, out hexa[6]);
        index = Execute(input, index + 1, IsValid, out hexa[7]);

        return new string(hexa);

        static bool IsValid(Span<byte> destination, out byte hexa)
        {
            if (destination is [0x00, 0x00, (0, var val), ..])
            {
                hexa = val;
                return true;
            }
            hexa = default;
            return false;
        }
    }

    static int Execute(string input, int startIndex, IsValid isValid, out char hexa)
    {
        ReadOnlySpan<byte> nines = stackalloc byte[]
        {
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
        };
        Span<byte> destination = stackalloc byte[16];
        Span<byte> source = new byte[input.Length + GetLength(startIndex)];
        Encoding.UTF8.GetBytes(input, source);
        for (var i = startIndex; i < int.MaxValue; i++)
        {
            ToSpan(i, source[input.Length..]);
            MD5.HashData(source, destination);
            if (isValid(destination, out var val))
            {
                hexa = (char)(val <= 9 ? val + '0' : (val - 10) + 'a');
                return i;
            }
            if (source[input.Length..].SequenceEqual(nines[..(source[input.Length..].Length)]))
                Enlarge(ref source);
        }

        throw new UnreachableException();

        static void Enlarge(ref Span<byte> source)
        {
            var span = new byte[source.Length + 1];
            source.CopyTo(span);
            source = span;
        }

        static int ToSpan(int i, Span<byte> span)
        {
            switch (i)
            {
                case < 0:
                    throw new UnreachableException();
                case < 10:
                    WriteInt(i, out span[0]);
                    return 1;
                default:
                    var written = ToSpan(i / 10, span);
                    WriteInt(i % 10, out span[written]);
                    return written + 1;
            }

            static void WriteInt(int i, out byte value)
            => value = (byte)(((byte)i) + '0');
        }

        static int GetLength(int i)
        => i switch
        {
            < 0 => throw new UnreachableException(),
            < 10 => 1,
            _ => GetLength(i / 10) + 1,
        };
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}

static class Ext
{
    public static void Deconstruct(this byte b, out byte upper, out byte lower)
    => (upper, lower) = ((byte)(b >> 4), (byte)(b & 0xf));
}
