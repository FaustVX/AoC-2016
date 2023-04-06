#nullable enable
namespace AdventOfCode.Y2016.Day12;

interface IInstruction
{
    void Execute(Computer computer);

    public static IInstruction Parse(ReadOnlySpan<char> line)
    => line.EnumerateSplits(" ") switch
    {
        ["cpy", var from, var to] when int.TryParse(from, out var value) => new CopyValue(value, to[0]),
        ["cpy", var from, var to] => new CopyRegister(from[0], to[0]),
        ["inc", var reg] => new Increment(reg[0]),
        ["dec", var reg] => new Decrement(reg[0]),
        ["jnz", var from, var offset] when int.TryParse(from, out var value) => new JumpNonZeroValue(value, int.Parse(offset)),
        ["jnz", var from, var offset] => new JumpNonZeroRegister(from[0], int.Parse(offset)),
        _ => throw new UnreachableException(),
    };
}

file sealed record class CopyValue(int Value, char To) : IInstruction
{
    void IInstruction.Execute(Computer computer)
    => computer.Registers[To - 'a'] = Value;
}

file sealed record class CopyRegister(char From, char To) : IInstruction
{
    void IInstruction.Execute(Computer computer)
    => computer.Registers[To - 'a'] = computer.Registers[From - 'a'];
}

file sealed record class Increment(char Register) : IInstruction
{
    void IInstruction.Execute(Computer computer)
    => computer.Registers[Register - 'a']++;
}

file sealed record class Decrement(char Register) : IInstruction
{
    void IInstruction.Execute(Computer computer)
    => computer.Registers[Register - 'a']--;
}

file sealed record class JumpNonZeroValue(int Value, int Offset) : IInstruction
{
    void IInstruction.Execute(Computer computer)
    {
        if (Value != 0)
            computer.PC += Offset - 1;
    }
}

file sealed record class JumpNonZeroRegister(char Register, int Offset) : IInstruction
{
    void IInstruction.Execute(Computer computer)
    {
        if (computer.Registers[Register - 'a'] != 0)
            computer.PC += Offset - 1;
    }
}
