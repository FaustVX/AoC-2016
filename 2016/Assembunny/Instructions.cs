#nullable enable
using System.Runtime.InteropServices;

namespace AdventOfCode.Y2016.Assembunny;

interface IInstruction
{
    void Execute(Computer computer)
    => (ToggledInstruction ?? this).ExecuteSelf(computer);
    void ExecuteSelf(Computer computer);
    void Toggle();
    IInstruction? ToggledInstruction { get; set; }

    public static IInstruction Parse(ReadOnlySpan<char> line)
    => line.EnumerateSplits(" ") switch
    {
        ["cpy", var from, var to] => new Copy(from, to[0]),
        ["tgl", var reg] => new Toggle(reg),
        ["inc", var reg] => new Increment(reg[0]),
        ["dec", var reg] => new Decrement(reg[0]),
        ["jnz", var from, var offset] => new JumpNonZero(from, offset),
        _ => throw new UnreachableException(),
    };
}

file sealed record class Invalid() : IInstruction
{
    IInstruction? IInstruction.ToggledInstruction { get; set; }

    void IInstruction.ExecuteSelf(Computer computer)
    { }

    void IInstruction.Toggle()
    => throw new NotImplementedException();
}

file sealed record class Copy(IntOrReg Value, char To) : IInstruction
{
    void IInstruction.Toggle()
    => ((IInstruction)this).ToggledInstruction = ((IInstruction)this).ToggledInstruction switch
    {
        null => new JumpNonZero(Value, new(To)),
        _ => null,
    };
    IInstruction? IInstruction.ToggledInstruction { get; set; } = null;

    void IInstruction.ExecuteSelf(Computer computer)
    => computer.Registers[To - 'a'] = Value.Compute(computer);
}

file sealed record class Toggle(IntOrReg Register) : IInstruction
{
    void IInstruction.Toggle()
    => ((IInstruction)this).ToggledInstruction = (((IInstruction)this).ToggledInstruction, Register) switch
    {
        (null, { IsReg: true, Reg: var reg }) => new Increment(reg),
        (null, { IsReg: false }) => new Invalid(),
        _ => null,
    };
    IInstruction? IInstruction.ToggledInstruction { get; set; } = null;

    void IInstruction.ExecuteSelf(Computer computer)
    => computer.Instructions.Span[Register.Compute(computer)].Toggle();
}

file sealed record class Increment(char Register) : IInstruction
{
    void IInstruction.Toggle()
    => ((IInstruction)this).ToggledInstruction = ((IInstruction)this).ToggledInstruction switch
    {
        null => new Decrement(Register),
        _ => null,
    };
    IInstruction? IInstruction.ToggledInstruction { get; set; } = null;

    void IInstruction.ExecuteSelf(Computer computer)
    => computer.Registers[Register - 'a']++;
}

file sealed record class Decrement(char Register) : IInstruction
{
    void IInstruction.Toggle()
    => ((IInstruction)this).ToggledInstruction = ((IInstruction)this).ToggledInstruction switch
    {
        null => new Increment(Register),
        _ => null,
    };
    IInstruction? IInstruction.ToggledInstruction { get; set; } = null;

    void IInstruction.ExecuteSelf(Computer computer)
    => computer.Registers[Register - 'a']--;
}

file sealed record class JumpNonZero(IntOrReg Value, IntOrReg Offset) : IInstruction
{
    void IInstruction.Toggle()
    => ((IInstruction)this).ToggledInstruction = (((IInstruction)this).ToggledInstruction, Offset) switch
    {
        (null, { IsReg: true, Reg: var reg }) => new Copy(Value, reg),
        (null, { IsReg: false }) => new Invalid(),
        _ => null,
    };
    IInstruction? IInstruction.ToggledInstruction { get; set; } = null;

    void IInstruction.ExecuteSelf(Computer computer)
    {
        if (Value.Compute(computer) != 0)
            computer.PC += Offset.Compute(computer) - 1;
    }
}

[StructLayout(LayoutKind.Explicit)]
file readonly struct IntOrReg
{
    [FieldOffset(0)]
    public readonly bool IsReg;
    [FieldOffset(1)]
    public readonly int Value;

    [FieldOffset(1)]
    public readonly char Reg;

    public int Compute(Computer computer)
    {
        if (IsReg)
            return computer.Registers[Reg - 'a'];
        return Value;
    }

    public IntOrReg(ReadOnlySpan<char> input)
    {
        if (int.TryParse(input, out var value))
            (Value, IsReg) = (value, false);
        else
            (Reg, IsReg) = (input[0], true);
    }

    public IntOrReg(int input)
    => (Value, IsReg) = (input, false);

    public IntOrReg(char input)
    => (Reg, IsReg) = (input, true);

    public static implicit operator IntOrReg(ReadOnlySpan<char> input)
    => new(input);
}
