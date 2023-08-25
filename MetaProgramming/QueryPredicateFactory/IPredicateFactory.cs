namespace MetaProgramming.QueryPredicateFactory;
public interface IPredicateFactory<T>
{
    Func<T, bool> Create(string value);
}
