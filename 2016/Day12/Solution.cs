#nullable enable
namespace AdventOfCode.Y2016.Day12;

[ProblemName("Leonardo's Monorail")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var computer = new Computer()
        {
            Instructions = ParseInstructions(input.AsMemory()).ToArray(),
        };
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
        computer.Registers[2] = 1;
        computer.Run();
        return computer.Registers[0];
    }
}
