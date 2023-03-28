#nullable enable
namespace AdventOfCode.Y2016.Day02;

[ProblemName("Bathroom Security")]
public class Solution : ISolver //, IDisplay
{
    public object PartOne(string input)
    {
        var code = 0;
        var machine = new StateMachine();
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            foreach (var c in line)
                machine.Move(c);
                code = code * 10 + machine.CurrentState.Value;
        }
        return code;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}

class StateMachine
{
    public State CurrentState { get; private set; } = State._5;

    public void Move(char transition)
    => CurrentState = transition switch
    {
        'D' => CurrentState.Down,
        'L' => CurrentState.Left,
        'R' => CurrentState.Right,
        'U' => CurrentState.Up,
        _ => throw new UnreachableException(),
    };

    public class State
    {
        public static readonly State _1 = new(1);
        public static readonly State _2 = new(2);
        public static readonly State _3 = new(3);
        public static readonly State _4 = new(4);
        public static readonly State _5 = new(5);
        public static readonly State _6 = new(6);
        public static readonly State _7 = new(7);
        public static readonly State _8 = new(8);
        public static readonly State _9 = new(9);

        static State()
        {
            _2.Left = _4.Up = _1;
            _1.Right = _3.Left = _5.Up = _2;
            _2.Right = _6.Up = _3;

            _1.Down = _5.Left = _7.Up = _4;
            _2.Down = _4.Right = _6.Left = _8.Up = _5;
            _3.Down = _5.Right = _9.Up = _6;

            _4.Down = _8.Left = _7;
            _5.Down = _7.Right = _9.Left = _8;
            _6.Down = _8.Right = _9;
        }

        public State(int value)
        {
            Value = value;

            Up = Down = Left = Right = this;
        }

        public int Value { get; }

        public State Up { get; private set; }
        public State Down { get; private set; }
        public State Left { get; private set; }
        public State Right { get; private set; }
    }
}
