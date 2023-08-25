namespace MetaProgramming.QueryPredicateFactory;
public interface IQueryPredicateFactory<T>
{
    IPredicateFactory<T> Create(string key);
}
