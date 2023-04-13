#nullable enable
namespace AdventOfCode.Y2016.Assembunny;

interface IInstruction
{
    void Execute(Computer computer);

    public static IInstruction Parse(ReadOnlySpan<char> line)
    => line.EnumerateSplits(" ") switch
    {
        ["cpy", var from, var to] => new Copy(from, to[0]),
        ["inc", var reg] => new Increment(reg[0]),
        ["dec", var reg] => new Decrement(reg[0]),
        ["jnz", var from, var offset] => new JumpNonZero(from, offset),
        _ => throw new UnreachableException(),
    };
}

file sealed record class Copy(IntOrReg Value, char To) : IInstruction
{
    void IInstruction.Execute(Computer computer)
    => computer.Registers[To - 'a'] = Value.Compute(computer);
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

file sealed record class JumpNonZero(IntOrReg Value, IntOrReg Offset) : IInstruction
{
    void IInstruction.Execute(Computer computer)
    {
        if (Value.Compute(computer) != 0)
            computer.PC += Offset.Compute(computer) - 1;
    }
}

[StructLayout(LayoutKind.Explicit)]
file readonly struct IntOrReg
{
    [FieldOffset(0)]
    private readonly bool _isReg;
    [FieldOffset(1)]
    public readonly int Value;

    [FieldOffset(1)]
    public readonly char Reg;

    public int Compute(Computer computer)
    {
        if (_isReg)
            return computer.Registers[Reg - 'a'];
        return Value;
    }

    public IntOrReg(ReadOnlySpan<char> input)
    {
        if (int.TryParse(input, out var value))
            (Value, _isReg) = (value, false);
        else
            (Reg, _isReg) = (input[0], true);
    }

    public static implicit operator IntOrReg(ReadOnlySpan<char> input)
    => new(input);
}
