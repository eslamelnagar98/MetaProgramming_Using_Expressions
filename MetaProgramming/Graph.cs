namespace MetaProgramming;
public record Vector3d(int X, int Y, int Z);
public class Graph : List<Vector3d>
{
    public Graph()
    {
        var vectorList = Enumerable.Range(0, 1000).Select(_ =>
        new Vector3d(
            Random.Shared.Next(100),
            Random.Shared.Next(100),
            Random.Shared.Next(100)))
            .ToList();

        AddRange(vectorList);

    }
}
