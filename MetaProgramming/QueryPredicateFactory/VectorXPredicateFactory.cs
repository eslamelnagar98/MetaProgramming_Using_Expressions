namespace MetaProgramming.QueryPredicateFactory;
public class VectorXPredicateFactory<T> : BaseVectorPreidicateFactory, IPredicateFactory<T>
    where T : Vector3d
{
    public Func<T, bool> Create(string value)
    {
        return vector => Compare(vector.X, value);
    }
}
