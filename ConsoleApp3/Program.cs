using System.Runtime.CompilerServices;
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
    if (py + 1 < map.GetLength(0) && py + 1 >= 0 && px < map.GetLength(1) && px >= 0 && !IsWall(map[px, py + 1]))
    {
        result.Add(new Point(px, py + 1));
    }
    if (py - 1 < map.GetLength(0) && py - 1 >= 0 && px < map.GetLength(1) && px >= 0 && !IsWall(map[px, py - 1]))
    {
        result.Add(new Point(px, py - 1));
    }
    if (py < map.GetLength(0) && py >= 0 && px + 1 < map.GetLength(1) && px + 1 >= 0 && !IsWall(map[px + 1, py]))
    {
        result.Add(new Point(px+1, py));
    }
    if (py < map.GetLength(0) && py >= 0 && px - 1 < map.GetLength(1) && px - 1 >= 0 && !IsWall(map[px - 1, py]))
    {
        result.Add(new Point(px-1, py));
    }

    return result;
}

List<Point> GetShortestPath(string[,] map, Point start, Point goal)
{
    Queue<Point> frontier = new Queue<Point>();
    Dictionary<Point, Point?> CameFrom = new Dictionary<Point, Point?>();
    frontier.Enqueue(start);

    while (frontier.Count > 0)
    {
        Point p = frontier.Dequeue();
        
        CameFrom.Add(start, null);
        if (IsEqual(p, goal))
        {
            break;
        }
        
        //get neighbors
        // foreach (Point neighbor in GetNeighbors(p.Row, p.Column, map))
        // {
        //     if (CameFrom.TryGetValue(neighbor, out _))
        //     {
        //         CameFrom.Add(neighbor, p);
        //         frontier.Enqueue(neighbor);
        //     }
        // }
    }
    return new List<Point>();
}

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 15,
    Seed = 12312,
});

string[,] map = generator.Generate();

List<Point> path = new List<Point>(new Point[]
{
    new Point(0, 0),
    new Point(1, 0),
    new Point(2, 0),
    new Point(3, 0),
});

Point p = new Point(12, 7);

List<Point> n = GetNeighbors(map,p);
foreach (Point neighbor in GetNeighbors(map, p))
{
    map[p.Column, p.Row] = "N";
}


PrintMap(map, path);

// placing a cross
// map[14, 6] = "X";

// prints width and height
// Console.WriteLine(map.GetLength(0));
// Console.WriteLine(map.GetLength(1));
