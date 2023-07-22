namespace MetaProgramming;
public static class Expressions
{
    public static Func<string, Func<Vector3d, bool>> CreateCondition(Func<Vector3d, int> coordinateSelector)
    {
        return value => value switch
        {
            ['e', .. var rest] => vector => coordinateSelector(vector) == int.Parse(rest),
            ['l', .. var rest] => vector => coordinateSelector(vector) < int.Parse(rest),
            ['g', .. var rest] => vector => coordinateSelector(vector) > int.Parse(rest),
            _ => throw new NotSupportedException()
        };
    }
    public static Func<string, Func<Vector3d, bool>> MethodExpressionGenerated(Expression<Func<Vector3d, int>> valueExpression)
    {
        return stringValue =>
        {
            var comparison = GetComparison(stringValue[0]);
            var number = int.Parse(stringValue[1..]);
            var lambdaValue = valueExpression.Compile();
            return vector => comparison(lambdaValue(vector), number);
        };
    }
    public static Func<int, int, bool> GetComparison(char @char)
    {
        return @char switch
        {
            'e' => Equal,
            'l' => LessThan,
            'g' => GreaterThan,
            _ => throw new NotSupportedException()
        };
    }

    private static bool Equal(int firstNumber, int secodnNumber) => firstNumber.CompareTo(secodnNumber) == 0;
    private static bool LessThan(int firstNumber, int secodnNumber) => firstNumber.CompareTo(secodnNumber) < 0;
    private static bool GreaterThan(int firstNumber, int secodnNumber) => firstNumber.CompareTo(secodnNumber) > 0;

}
