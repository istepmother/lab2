using ConsoleApp3;

// instead of adding methods to Point
bool IsEqual(Point a, Point b)
{
    return a.Column == b.Column && a.Row == b.Row;
}

void PrintMap(string[,] map, List<Point> path)
{
    Point start = path[0];
    Point end = path[^1];

    foreach (Point p in path)
    {
        if (IsEqual(p, start))
        {
            map[p.Column, p.Row] = "A";
        }
        else if (IsEqual(p, end))
        {
            map[p.Column, p.Row] = "B";
        }
        else
        {
            map[p.Column, p.Row] = ".";
        }
    }
    
    new MapPrinter().Print(map);
}

bool IsWall(string s)
{
    return s == "█";
}

List<Point> GetNeighbors(string[,] map, Point p)
{
    List<Point> result = new List<Point>();

    int px = p.Column;
    int py = p.Row;
    
    // zroby optymalnishe cherez offset
    if (py + 1 < map.GetLength(1) && py + 1 >= 0 && px < map.GetLength(0) && px >= 0 && !IsWall(map[px, py + 1]))
    {
        result.Add(new Point(px, py + 1));
    }
    if (py - 1 < map.GetLength(1) && py - 1 >= 0 && px < map.GetLength(0) && px >= 0 && !IsWall(map[px, py - 1]))
    {
        result.Add(new Point(px, py - 1));
    }
    if (py < map.GetLength(1) && py >= 0 && px + 1 < map.GetLength(0) && px + 1 >= 0 && !IsWall(map[px + 1, py]))
    {
        result.Add(new Point(px+1, py));
    }
    if (py < map.GetLength(1) && py >= 0 && px - 1 < map.GetLength(0) && px - 1 >= 0 && !IsWall(map[px - 1, py]))
    {
        result.Add(new Point(px-1, py));
    }

    return result;
}

List<Point> GetShortestPath(string[,] map, Point start, Point goal)
{
    Queue<Point> frontier = new Queue<Point>();
    Dictionary<Point, Point?> cameFrom = new Dictionary<Point, Point?>();
    
    cameFrom.Add(start, null);
    frontier.Enqueue(start);

    while (frontier.Count > 0)
    {
        Point cur = frontier.Dequeue();
        if (IsEqual(cur, goal))
        {
            break;
        }

        // get neighbors
        foreach (Point neighbor in GetNeighbors(map, cur))
        {
            if (cameFrom.TryGetValue(neighbor, out _))
            {
                cameFrom.Add(neighbor, cur);
                frontier.Enqueue(neighbor);
            }
        }
    }

    List<Point> path = new List<Point>();
    Point? current = goal;

    while (current != null && !IsEqual(current.Value, start))
    {
        path.Add(current.Value);
        cameFrom.TryGetValue(current.Value, out current);
    }
    path.Add(start);
    
    path.Reverse();
    return path;
}

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 15,
    Seed = 12312,
});

string[,] map = generator.Generate();

Point start = new Point(0, 0);
Point goal = new Point(0, 2);
List<Point> path = GetShortestPath(map, start, goal);

PrintMap(map, path);

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

