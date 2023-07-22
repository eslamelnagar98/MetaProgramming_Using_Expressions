var queryParameterMap = new QueryPredicates();
queryParameterMap = ObjectPropertyAccess<Vector3d>.GenerateQueryParameterMap();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Graph>();
builder.Services.AddSingleton(queryParameterMap);
var app = builder.Build();
app.MapGet("/", QueryFilter);
app.Run();
IEnumerable<Vector3d> QueryFilter(HttpRequest request, QueryPredicates queryPredicates, Graph graph)
{
    var query = Unsafe.As<Graph, IEnumerable<Vector3d>>(ref graph);
    foreach (var (key, values) in request.Query)
    {
        var value = values;
        if (!string.IsNullOrWhiteSpace(value) && queryPredicates.TryGetValue(key, out var predicate))
        {
            query = query.Where(predicate(value));
        }
    }
    return query;
}



