#nullable enable

namespace AdventOfCode.Y2016.Day10;

[ProblemName("Balance Bots")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        Span<Bot> bots = stackalloc Bot[Globals.IsTestInput ? 3 : 210];
        Span<int> outputs = stackalloc int[Globals.IsTestInput ? 3 : 21];

        Execute(input, bots, outputs);

        for (var id = 0; id < bots.Length; id++)
            if (bots[id].IsComparedValues)
                return id;
        throw new UnreachableException("Bot not found");
    }

    static void Execute(ReadOnlySpan<char> input, Span<Bot> bots, Span<int> outputs)
    {
        foreach (var line in input.EnumerateLines())
            switch (line.EnumerateSplits(" "))
            {
                case ["bot", (i: var id), _, _, _, var lowKind, (i: var lowId), _, _, _, var hightKind, (i: var hightId)]:
                    bots[id] = bots[id] with
                    {
                        GiveLowTo = (lowKind is "bot", lowId),
                        GiveHighTo = (hightKind is "bot", hightId),
                    };
                    bots[id].GiveValues(bots, outputs);
                    break;
                case ["value", (i: var value), _, _, _, (i: var to)]:
                    bots[to].AcceptValue(value, bots, outputs);
                    break;
                default:
                    throw new UnreachableException("Unknown command");
            }
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}

static class Ext
{
    public static void Deconstruct(this ReadOnlySpan<char> span, out int i)
    => int.TryParse(span, default, out i);
}
