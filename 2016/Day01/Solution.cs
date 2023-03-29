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
        foreach (var split in input.EnumerateSplits(", "))
        {
            orientation = (split[0], orientation) switch
            {
                ('L', Orientation.North) or ('R', Orientation.South) => Orientation.West,
                ('L', Orientation.East) or ('R', Orientation.West) => Orientation.North,
                ('L', Orientation.South) or ('R', Orientation.North) => Orientation.East,
                ('L', Orientation.West) or ('R', Orientation.East) => Orientation.South,
                _ => throw new UnreachableException(),
            };
            var dist = int.Parse(split[1..]);
            pos = Move(orientation, dist, pos);
        }
        return Math.Abs(pos.x) + Math.Abs(pos.y);
    }

    private static (int x, int y) Move(Orientation orientation, int dist, (int x, int y) pos)
    => Dir(orientation).Times(dist).Add(pos);

    private static (int x, int y) Dir(Orientation orientation)
    => orientation switch
    {
        Orientation.North => (0, -1),
        Orientation.East  => (+1, 0),
        Orientation.South => (0, +1),
        Orientation.West  => (-1, 0),
        _ => throw new UnreachableException(),
    };

    public object PartTwo(string input)
    {
        var span = input.AsSpan();
        var pos = (x: 0, y: 0);
        var orientation = Orientation.North;
        var set = new HashSet<(int x, int y)>()
        {
            pos,
        };
        foreach (var split in input.EnumerateSplits(", "))
        {
            orientation = (split[0], orientation) switch
            {
                ('L', Orientation.North) or ('R', Orientation.South) => Orientation.West,
                ('L', Orientation.East) or ('R', Orientation.West) => Orientation.North,
                ('L', Orientation.South) or ('R', Orientation.North) => Orientation.East,
                ('L', Orientation.West) or ('R', Orientation.East) => Orientation.South,
                _ => throw new UnreachableException(),
            };
            var dist = int.Parse(split[1..]);
            var dir = Dir(orientation);
            for (var i = 0; i < dist; i++)
            {
                pos = pos.Add(dir);
                if (!set.Add(pos))
                    return Math.Abs(pos.x) + Math.Abs(pos.y);
            }
        }
        return Math.Abs(pos.x) + Math.Abs(pos.y);
    }

    enum Orientation
    {
        North,
        East,
        South,
        West,
    }
}
