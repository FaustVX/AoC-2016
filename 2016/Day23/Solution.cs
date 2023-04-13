#nullable enable
using AdventOfCode.Y2016.Assembunny;

namespace AdventOfCode.Y2016.Day23;

[ProblemName("Safe Cracking")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var computer = new Computer()
        {
            Instructions = ParseInstructions(input.AsMemory()).ToArray(),
        };
        if (!Globals.IsTestInput)
            computer.Registers[0] = 7;
        computer.Run();
        return computer.Registers[0];
    }

    static IEnumerable<IInstruction> ParseInstructions(ReadOnlyMemory<char> input)
    {
        foreach (var line in input.EnumerateLines())
            yield return IInstruction.Parse(line.Span);
    }

    public object PartTwo(string input)
    {
        var computer = new Computer()
        {
            Instructions = ParseInstructions(input.AsMemory()).ToArray(),
        };
        if (!Globals.IsTestInput)
            computer.Registers[0] = 12;
        computer.Run();
        return computer.Registers[0];
    }
}
