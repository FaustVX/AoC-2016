#nullable enable
namespace AdventOfCode.Y2016.Day10;

public struct Bot
{
    private static (int hight, int low) Compare
    => Globals.IsTestInput ? (5, 2) : (61, 17);

    private int _value1, _value2;
    public (bool isBot, int id) GiveLowTo, GiveHighTo;

    public readonly bool Has2Values
    => _value1 != 0 && _value2 != 0;
    public readonly bool IsInit
    => GiveLowTo != GiveHighTo;
    public bool IsComparedValues
    => (_value2, _value1) == Compare;

    public void GiveValues(ReadOnlySpan<Bot> bots, Span<int> outputs)
    {
        if (!IsInit || !Has2Values)
            return;
        (_value1, _value2) = (Math.Min(_value1, _value2), Math.Max(_value1, _value2));

        GiveValue(GiveLowTo, bots, outputs, _value1);
        GiveValue(GiveHighTo, bots, outputs, _value2);

        static void GiveValue((bool isBot, int id) giveTo, ReadOnlySpan<Bot> bots, Span<int> outputs, int value)
        {
            if (giveTo.isBot)
                bots[giveTo.id].AcceptValue(value, bots, outputs);
            else
                outputs[giveTo.id] = value;
        }
    }

    public void AcceptValue(int value, ReadOnlySpan<Bot> bots, Span<int> outputs)
    {
        if (_value1 == 0)
            _value1 = value;
        else if (_value2 == 0)
        {
            _value2 = value;
            GiveValues(bots, outputs);
        }
        else
            throw new UnreachableException("_value1 and _value2 are both initialized");
    }
}
