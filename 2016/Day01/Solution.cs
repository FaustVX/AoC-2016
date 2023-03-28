#nullable enable
namespace AdventOfCode.Y2016.Day01;

[ProblemName("No Time for a Taxicab")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var span = input.AsSpan();
        var pos = (x: 0, y: 0);
        var orientation = Orientation.North;
        while (true)
        {
            var comma = span.IndexOf(',');
            orientation = (span[0], orientation) switch
            {
                ('L', Orientation.North) or ('R', Orientation.South) => Orientation.West,
                ('L', Orientation.East) or ('R', Orientation.West) => Orientation.North,
                ('L', Orientation.South) or ('R', Orientation.North) => Orientation.East,
                ('L', Orientation.West) or ('R', Orientation.East) => Orientation.South,
                _ => throw new UnreachableException(),
            };
            var dist = int.Parse(comma is -1 ? span[1..] : span[1..comma]);
                    pos = Move(orientation, dist, pos);
            if (comma is -1)
                return Math.Abs(pos.x) + Math.Abs(pos.y);
            span = span[(comma + 2)..];
        }
    }

    private static (int x, int y) Move(Orientation orientation, int dist, (int x, int y) pos)
    => orientation switch
    {
        Orientation.North => (pos.x, pos.y - dist),
        Orientation.East  => (pos.x + dist, pos.y),
        Orientation.South => (pos.x, pos.y + dist),
        Orientation.West => (pos.x - dist, pos.y),
        _ => throw new UnreachableException(),
    };

    public object PartTwo(string input)
    {
        return 0;
    }

    enum Orientation
    {
        North,
        East,
        South,
        West,
    }
}
