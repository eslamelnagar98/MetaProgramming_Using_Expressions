namespace MetaProgramming.QueryPredicateFactory;
public abstract class BaseVectorPreidicateFactory
{
    protected bool Compare(int vectorProperty, string value)
    {
        var comparisonChar = value[0];
        var number = int.Parse(value[1..]);
        return comparisonChar switch
        {
            'e' => vectorProperty == number,
            'l' => vectorProperty < number,
            'g' => vectorProperty > number,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
