#nullable enable
namespace AdventOfCode.Y2016.Day12;

class Computer
{
    public int[] Registers  { get; } = new int[4];
    public int PC { get; set; }
    public required ReadOnlyMemory<IInstruction> Instructions { get; init; }

    public void Run()
    {
        while (PC < Instructions.Length)
            Instructions.Span[PC++].Execute(this);
    }
}
