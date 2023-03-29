#nullable enable
namespace AdventOfCode.Y2016.Day02;

[ProblemName("Bathroom Security")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var code = 0;
        var machine = new StateMachine<StateP1>() { CurrentState = StateP1._5 };
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            foreach (var c in line)
                machine.Move(c);
            code = code * 10 + (machine.CurrentState.Value - '0');
        }
        return code;
    }

    public object PartTwo(string input)
    {
        var code = "";
        var machine = new StateMachine<StateP2>() { CurrentState = StateP2._5 };
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            foreach (var c in line)
                machine.Move(c);
            code += machine.CurrentState.Value;
        }
        return code;
    }
}

class StateMachine<TState>
where TState : StateMachine<TState>.IState
{
    public required IState CurrentState { get; set; }

    public void Move(char transition)
    => CurrentState = transition switch
    {
        'D' => CurrentState.Down,
        'L' => CurrentState.Left,
        'R' => CurrentState.Right,
        'U' => CurrentState.Up,
        _ => throw new UnreachableException(),
    };

    public interface IState
    {
        public abstract char Value { get; }
        public abstract TState Up { get; }
        public abstract TState Down { get; }
        public abstract TState Left { get; }
        public abstract TState Right { get; }
    }
}

public class StateP1 : StateMachine<StateP1>.IState
{
    private static readonly Dictionary<(int x, int y), StateP1> _states = new(capacity: 9);
    public static readonly StateP1 _1 = new('1', (0, 0));
    public static readonly StateP1 _2 = new('2', (1, 0));
    public static readonly StateP1 _3 = new('3', (2, 0));
    public static readonly StateP1 _4 = new('4', (0, 1));
    public static readonly StateP1 _5 = new('5', (1, 1));
    public static readonly StateP1 _6 = new('6', (2, 1));
    public static readonly StateP1 _7 = new('7', (0, 2));
    public static readonly StateP1 _8 = new('8', (1, 2));
    public static readonly StateP1 _9 = new('9', (2, 2));

    static StateP1()
    {
        foreach (var ((x, y), state) in _states)
        {
            if (_states.TryGetValue((x, y - 1), out var up))
                state.Up = up;
            if (_states.TryGetValue((x, y + 1), out var down))
                state.Down = down;
            if (_states.TryGetValue((x - 1, y), out var left))
                state.Left = left;
            if (_states.TryGetValue((x + 1, y), out var right))
                state.Right = right;
        }
    }

    private StateP1(char value, (int x, int y) pos)
    {
        Value = value;
        Up = Down = Left = Right = this;
        _states[pos] = this;
    }

    public char Value { get; }

    public StateP1 Up { get; private set; }
    public StateP1 Down { get; private set; }
    public StateP1 Left { get; private set; }
    public StateP1 Right { get; private set; }
}


public class StateP2 : StateMachine<StateP2>.IState
{
    private static readonly Dictionary<(int x, int y), StateP2> _states = new(capacity: 13);
    public static readonly StateP2 _1 = new('1', (2, 0));
    public static readonly StateP2 _2 = new('2', (1, 1));
    public static readonly StateP2 _3 = new('3', (2, 1));
    public static readonly StateP2 _4 = new('4', (3, 1));
    public static readonly StateP2 _5 = new('5', (0, 2));
    public static readonly StateP2 _6 = new('6', (1, 2));
    public static readonly StateP2 _7 = new('7', (2, 2));
    public static readonly StateP2 _8 = new('8', (3, 2));
    public static readonly StateP2 _9 = new('9', (4, 2));
    public static readonly StateP2 _A = new('A', (1, 3));
    public static readonly StateP2 _B = new('B', (2, 3));
    public static readonly StateP2 _C = new('C', (3, 3));
    public static readonly StateP2 _D = new('D', (2, 4));

    static StateP2()
    {
        foreach (var ((x, y), state) in _states)
        {
            if (_states.TryGetValue((x, y - 1), out var up))
                state.Up = up;
            if (_states.TryGetValue((x, y + 1), out var down))
                state.Down = down;
            if (_states.TryGetValue((x - 1, y), out var left))
                state.Left = left;
            if (_states.TryGetValue((x + 1, y), out var right))
                state.Right = right;
        }
    }

    private StateP2(char value, (int x, int y) pos)
    {
        Value = value;
        Up = Down = Left = Right = this;
        _states[pos] = this;
    }

    public char Value { get; }

    public StateP2 Up { get; private set; }
    public StateP2 Down { get; private set; }
    public StateP2 Left { get; private set; }
    public StateP2 Right { get; private set; }
}
