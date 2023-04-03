#nullable enable
using CommunityToolkit.HighPerformance.Enumerables;

namespace AdventOfCode.Y2016.Day08;

[ProblemName("Two-Factor Authentication")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    => Execute(input).CountOn();

    static Screen Execute(ReadOnlySpan<char> input)
    {
        var screen = new Screen();
        foreach (var line in input.EnumerateLines())
            switch (new SpanSplitEnumerator(line, " x=", true))
            {
                case ["rect", var a, var b]:
                    screen.Fill(int.Parse(a), int.Parse(b));
                    break;
                case [_, "row", _, var y, _, var by]:
                    screen.RotateRow(int.Parse(y), int.Parse(by));
                    break;
                case [_, "column", _, _, var x, _, var by]:
                    screen.RotateColumn(int.Parse(x), int.Parse(by));
                    break;
                default:
                    throw new UnreachableException();
            }
        return screen;
    }

    public object PartTwo(string input)
    => Execute(input).ToString();
}

readonly ref struct Screen
{
    public static int Width => Globals.IsTestInput ? 7 : 50;
    public static int Height => Globals.IsTestInput ? 3 : 6;
    readonly Span2D<bool> _screen = new(new bool[Width * Height], Width, Height);

    public Screen()
    { }

    public void Fill(int width, int height)
    => _screen[..width, ..height].Fill(true);

    public void RotateRow(int row, int by)
    => Rotate(_screen.GetColumn(row), by);

    public void RotateColumn(int col, int by)
    => Rotate(_screen.GetRow(col), by);

    public int CountOn()
    {
        var count = 0;
        foreach (var pixel in _screen)
            if (pixel)
                count++;
        return count;
    }

    private static void Rotate<T>(RefEnumerable<T> enumerator, int by)
    {
        by %= enumerator.Length;
        for (var i = 0; i < by; i++)
            for (var j = enumerator.Length - 1; j >= 1; j--)
                (enumerator[j - 1], enumerator[j]) = (enumerator[j], enumerator[j - 1]);
    }

    public void Print()
    {
        for (var y = 0; y < _screen.Width; y++)
        {
            foreach (var pixel in _screen.GetColumn(y))
                Console.Write(pixel ? '#' : ' ');
            Console.WriteLine();
        }
    }

    public override string ToString()
    => Globals.IsTestInput ? "" : OCR.GetOCR<OCR.Char5x6>(_screen, 0);
}
