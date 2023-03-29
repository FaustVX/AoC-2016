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
            if (IsTriangle(a, b, c))
                count++;
        }
        return count;
    }

    static (int, int, int) ParseTriangle(ReadOnlySpan<char> span)
    => (int.Parse(span.Slice(0, 5)), int.Parse(span.Slice(5, 5)), int.Parse(span.Slice(10, 5)));

    static bool IsTriangle(int a, int b, int c)
    => a + b > c && a + c > b && b + c > a;

    static bool IsTriangle<T>(Span<T> span, Func<T, int> func)
    => IsTriangle(func(span[0]), func(span[1]), func(span[2]));

    static bool ParseTriangles(scoped Span<(int a, int b, int c)> triangles, ref SpanLineEnumerator enumerator)
    {
        if (!enumerator.MoveNext())
            return false;
        triangles[0] = ParseTriangle(enumerator.Current);
        enumerator.MoveNext();
        triangles[1] = ParseTriangle(enumerator.Current);
        enumerator.MoveNext();
        triangles[2] = ParseTriangle(enumerator.Current);
        return true;
    }

    public object PartTwo(string input)
    {
        var count = 0;
        Span<(int a, int b, int c)> triangles = stackalloc (int, int, int)[3];
        var enumerator = input.AsSpan().EnumerateLines();

        while (ParseTriangles(triangles, ref enumerator))
        {
            if (IsTriangle(triangles, static tri => tri.a))
                count++;
            if (IsTriangle(triangles, static tri => tri.b))
                count++;
            if (IsTriangle(triangles, static tri => tri.c))
                count++;
        }
        return count;
    }
}
