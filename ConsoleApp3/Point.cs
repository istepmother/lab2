namespace ConsoleApp3;

public struct Point
{
    public int Column { get; }
    public int Row { get; }

    public Point(int column, int row)
    {
        Column = column;
        Row = row;
    }
    
    public bool IsEqual(Point b)
    {
        return this.Column == b.Column && this.Row == b.Row;
    }
}
