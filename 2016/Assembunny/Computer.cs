#nullable enable
namespace AdventOfCode.Y2016.Assembunny;

class Computer
{
    public int[] Registers  { get; } = new int[4];
    public int PC { get; set; }
    public required ReadOnlyMemory<IInstruction> Instructions { get; init; }

    public void Run()
    {
        while (PC < Instructions.Length)
            this[PC++]?.Execute(this);
    }

    public IInstruction? this[int pos]
    {
        get
        {
            if (pos >= 0 && pos < Instructions.Length)
                return Instructions.Span[pos];
            return null;
        }
    }
}
