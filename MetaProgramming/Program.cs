var queryParameterMap = ObjectPropertyAccess<Vector3d>.GenerateQueryParameterMap();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Graph>();
builder.Services.AddSingleton(queryParameterMap);
builder.Services.AddSingleton(typeof(IQueryPredicateFactory<>),typeof(QueryPredicateFactory<>));
var app = builder.Build();
app.MapGet("/", QueryFilter);
app.MapGet("/OOP", QueryFilterOOP);
app.Run();
//Beanchmark Is 17.66 Mean And Memory Consumption Is 8.95 KB But Less Readable
IEnumerable<Vector3d> QueryFilter(HttpRequest request, QueryPredicates<Vector3d> queryParameterMap, Graph graph)
{
    var query = Unsafe.As<Graph, IEnumerable<Vector3d>>(ref graph);
    foreach (var (key, values) in request.Query)
    {
        var value = values;
        if (!string.IsNullOrWhiteSpace(value) && queryParameterMap.TryGetValue(key, out var predicate))
        {
            query = query.Where(predicate(value));
        }
    }
    return query;
}

//Beanchmark Is 54.25 Mean And Memory Consumption Is 93.52 KB But More Readable
IEnumerable<Vector3d> QueryFilterOOP(HttpRequest request, IQueryPredicateFactory<Vector3d> queryPredicateFactory, Graph graph)
{
    var query = Unsafe.As<Graph, IEnumerable<Vector3d>>(ref graph);
    foreach (var (key, values) in request.Query)
    {
        var predicateFactory = queryPredicateFactory.Create(key);
        string? value = values;
        if (!string.IsNullOrWhiteSpace(value))
        {
            var predicate = predicateFactory.Create(value);
            query = query.Where(predicate);
        }
    }
    return query;
}



