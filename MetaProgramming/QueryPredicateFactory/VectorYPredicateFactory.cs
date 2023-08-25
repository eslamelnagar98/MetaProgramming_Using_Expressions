namespace MetaProgramming.QueryPredicateFactory;
public class VectorYPredicateFactory<T> : BaseVectorPreidicateFactory, IPredicateFactory<T> 
    where T : Vector3d
{
    public Func<T, bool> Create(string value)
    {
        return vector=> Compare(vector.Y, value);
    }
}
