namespace MetaProgramming;
public static class Expressions
{
    public static Func<string, Func<Vector3d, bool>> MethodExpressionGenerated(MemberExpression propertyAccess,
                                                                               ParameterExpression objectParameter)
    {
        return stringValue =>
        {
            var numberParameter = Expression.Parameter(typeof(int));
            var charParameter = Expression.Parameter(typeof(char));
            var equalComparison = Expression.Equal(propertyAccess, numberParameter);
            var greaterThanComparison = Expression.GreaterThan(propertyAccess, numberParameter);
            var lessThanComparison = Expression.LessThan(propertyAccess, numberParameter);
            var equalCondition = BuildEqualityExpressionTree(charParameter, equalComparison, greaterThanComparison, lessThanComparison);
            var final = Expression.Lambda<Func<Vector3d, char, int, bool>>(equalCondition, objectParameter, charParameter, numberParameter)
            .Compile();  
            var comparisonChar = stringValue[0];
            var number = int.Parse(stringValue[1..]);
            return vector => final(vector, comparisonChar, number);
        };
    }

    private static ConditionalExpression BuildEqualityExpressionTree(ParameterExpression charParameter,
                                                                     BinaryExpression equalComparison,
                                                                     BinaryExpression greaterThanComparison,
                                                                     BinaryExpression lessThanComparison)
    {
        var equalConstant = Expression.Constant('e');
        var lessThanConstant = Expression.Constant('l');
        var greaterThanConstant = Expression.Constant('g');
        var argOutOfRangeException = Expression.Constant(new ArgumentOutOfRangeException("Bad Charcter Parameter"));

        var greaterThanCondition = Expression.Condition(
                Expression.NotEqual(charParameter, greaterThanConstant),
                    Expression.Throw(argOutOfRangeException, typeof(bool)),
                        greaterThanComparison);

        var lessThanCondition = Expression.Condition(
                Expression.NotEqual(charParameter, lessThanConstant),
                    greaterThanCondition,
                        lessThanComparison);

        var equalCondition = Expression.Condition(
                Expression.NotEqual(charParameter, equalConstant),
                    lessThanCondition,
                        equalComparison);
        return equalCondition;
    }



}
