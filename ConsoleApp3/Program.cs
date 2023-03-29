using ConsoleApp3;

bool IsWall(string s)
{
    return s == "█";
}

List<Point> GetNeighbours(string[,] map, Point p)
{
    List<Point> result = new List<Point>();

    int px = p.Column;
    int py = p.Row;
    
    // zrobyla optymalnishe cherez offset
    int[,] offsets = {
        { 0, 1 },
        { 0, -1 },
        { 1, 0 },
        { -1, 0 }
    };

    for (int i = 0; i < offsets.GetLength(0); i++) {
        int dx = offsets[i, 0];
        int dy = offsets[i, 1];
        int nx = px + dx;
        int ny = py + dy;

        if (nx >= 0 && nx < map.GetLength(0) && ny >= 0 && ny < map.GetLength(1) && !IsWall(map[nx, ny])) {
            result.Add(new Point(nx, ny));
        }
    }


    return result;
}

List<Point> GetShortestPath(string[,] map, Point start, Point goal)
{
    var localPath = new List<Point> {start};
    var lastPoint = goal;
    var costSoFar = new Dictionary<Point, int>();
    var cameFrom = new Dictionary<Point, Point>();
    var frontier = new Queue<Point>();
    frontier.Enqueue(start);
    costSoFar.Add(start, 0);
    while (frontier.Count != 0)
    {
        var current = frontier.Dequeue();
        var available = GetNeighbours(map, current);
        foreach (var point in available)
        {
            if (!cameFrom.ContainsKey(point))
            {
                frontier.Enqueue(point);
                cameFrom.Add(point, current);
                if (!costSoFar.ContainsKey(point))
                {
                    costSoFar.Add(point, costSoFar[current] + 1);
                }
            }
            else if (costSoFar[current] + 1 < costSoFar[point])
            {
                cameFrom[point] = current;
                costSoFar[point] = costSoFar[current] + 1;
            }
        }

        if (current.Equals(goal))
        {
            lastPoint = goal;
            break;
        }
    }

    var lenOf = costSoFar[lastPoint];
    for (var i = 0; i != lenOf; i++)
    {
        var path = cameFrom[lastPoint];
        localPath.Add(path);
        lastPoint = path;
    }

    localPath.Add(goal);
    return localPath;
    
    
    // Queue<Point> frontier = new Queue<Point>();
    // Dictionary<Point, Point?> cameFrom = new Dictionary<Point, Point?>();
    //
    // cameFrom.Add(start, null);
    // frontier.Enqueue(start);
    //
    // while (frontier.Count > 0)
    // {
    //     Point cur = frontier.Dequeue();
    //     if (IsEqual(cur, goal))
    //     {
    //         break;
    //     }
    //
    //     // get neighbors
    //     foreach (Point neighbor in GetNeighbors(map, cur))
    //     {
    //         if (!cameFrom.TryGetValue(neighbor, out _))
    //         {
    //             cameFrom.Add(neighbor, cur);
    //             frontier.Enqueue(neighbor);
    //         }
    //     }
    // }
    //
    // List<Point> path = new List<Point>();
    // Point? current = goal;
    //
    // while (!IsEqual(current.Value, start))
    // {
    //     path.Add(current.Value);
    //     cameFrom.TryGetValue(current.Value, out current);
    // }
    // path.Add(start);
    //
    // path.Reverse();
    // return path;
}

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 15,
    Seed = 123,
});

string[,] map = generator.Generate();

Point start = new Point(6, 3);
Point goal = new Point(10, 5);
List<Point> path = GetShortestPath(map, start, goal);

new MapPrinter().Print(map, path);

// new MapPrinter().Print(map);

// placing a cross
// map[14, 6] = "X";

// prints width and height
// Console.WriteLine(map.GetLength(0));
// Console.WriteLine(map.GetLength(1));

// List<Point> path = new List<Point>(new Point[]
// {
//     new Point(0, 0),
//     new Point(1, 0),
//     new Point(2, 0),
//     new Point(3, 0),
// });
//
// Point p = new Point(12, 7);
//
// map[p.Column, p.Row] = "X";

// foreach (Point neighbor in GetNeighbors(map, p))
// {
//     map[neighbor.Column, neighbor.Row] = "N";
// }


