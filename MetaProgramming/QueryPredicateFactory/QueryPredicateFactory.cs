namespace MetaProgramming.QueryPredicateFactory;
public class QueryPredicateFactory<T> : IQueryPredicateFactory<T>
    where T : Vector3d
{
    public IPredicateFactory<T> Create(string key) => key.ToLower() switch
    {
        "x" => new VectorXPredicateFactory<T>(),
        "y" => new VectorYPredicateFactory<T>(),
        "z" => new VectorZPredicateFactory<T>(),
        _ => throw new NotImplementedException()
    };
}
