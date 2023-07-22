namespace MetaProgramming;
public static class ObjectPropertyAccess<T> where T : class
{
    private static readonly ParameterExpression _objectParameter;
    private static readonly PropertyInfo[] _objectProperties;
    private static readonly QueryPredicates _predicateDictionary;
    static ObjectPropertyAccess()
    {
        _objectParameter = ObjectParameterExpression();
        _objectProperties = GetProperties();
        _predicateDictionary = new QueryPredicates();
    }

    /// <summary>
    /// Iterarte Over All Object Properties , Get Property Access (Object.Property),
    /// Generate Lambda Expression Object => Object.Property
    /// </summary>
    /// <returns>QueryPredicated</returns>
    public static QueryPredicates GenerateQueryParameterMap()
    {
        foreach (var property in _objectProperties)
        {
            var propertyAccess = Expression.Property(_objectParameter, property);
            var propertyValueExpression = Expression.Lambda<Func<Vector3d, int>>
                (propertyAccess, _objectParameter);
            _predicateDictionary.TryAdd(property.Name.ToLower(), Expressions.MethodExpressionGenerated(propertyValueExpression));
        }
        return _predicateDictionary;
    }
    private static PropertyInfo[] GetProperties()
    {
        return typeof(T).GetProperties();
    }
    /// <summary>
    /// ObjectParameterExpression is a method that creates a ParameterExpression representing a parameter in a lambda expression.
    /// Like Object=> Object (Type Is The Generic Type)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>ParameterExpression</returns>
    private static ParameterExpression ObjectParameterExpression()
    {
        return Expression.Parameter(typeof(T));
    }



}
