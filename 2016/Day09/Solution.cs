#nullable enable
namespace AdventOfCode.Y2016.Day09;

[ProblemName("Explosives in Cyberspace")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var length = 0;
        foreach (var marker in new MarkerEnumerable() { Buffer = input })
            length += marker.TotalLength;
        return length;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}

ref struct MarkerEnumerable
{
    private ReadOnlySpan<char> _buffer;
    public required ReadOnlySpan<char> Buffer
    {
        get => _buffer;
        init => _buffer = value;
    }

    public Marker Current { get; private set; }

    [DebuggerStepThrough]
    public MarkerEnumerable GetEnumerator()
    => this;

    public bool MoveNext()
    {
        if (_buffer.Length == 0)
            return false;
        Current = new()
        {
            TextBefore = _buffer,
            Repeat = 0,
            TextRepeated = null,
        };
        if (!SplitAt(ref _buffer, '(', out var before))
        {
            _buffer = null;
            return true;
        }
        Current = Current with { TextBefore = before };
        if (!SplitAt(ref _buffer, 'x', out var countSpan))
            return false;
        var count = int.Parse(countSpan);
        if (!SplitAt(ref _buffer, ')', out var repeatSpan))
            return false;
        Current = Current with {
            Repeat = int.Parse(repeatSpan),
            TextRepeated = _buffer[..count],
        };
        _buffer = _buffer[count..];

        return true;

        static bool SplitAt(scoped ref ReadOnlySpan<char> buffer, char c, scoped out ReadOnlySpan<char> extracted)
        {
            extracted = null;
            var length = buffer.IndexOf(c);
            if (length < 0)
                return false;
            extracted = buffer[..length];
            buffer = buffer[(length + 1)..];
            return true;
        }
    }
}

readonly ref struct Marker
{
    [DebuggerStepThrough]
    public Marker()
    { }

    public readonly required ReadOnlySpan<char> TextBefore { get; init; }
    public readonly required int Repeat { get; init; }
    public readonly required ReadOnlySpan<char> TextRepeated { get; init; }
    public readonly int Count => TextRepeated.Length;
    public readonly int TotalLength => TextBefore.Length + Count * Repeat;
}
