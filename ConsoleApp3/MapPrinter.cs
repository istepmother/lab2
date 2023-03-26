namespace ConsoleApp3;
// При малюванні карти, зробіть так, щоб перша точка відображалась символом А,
// остання - В, а всі інші - крапками. Якщо ви почнете з цього, буде значно легше дебажити всю програму,
// оскільки завжди буде легко виводити шлях на екран , List<Point> path
public class MapPrinter
{
    public void Print(string[,] maze, List<Point> path)
        {
            Point start = path[0];
            Point end = path[^1];

            foreach (Point p in path)
            {
                if (p.IsEqual(start))
                {
                    maze[p.Column, p.Row] = "A";
                }
                else if (p.IsEqual(end))
                {
                    maze[p.Column, p.Row] = "B";
                }
                else 
                {
                    maze[p.Column, p.Row] = ".";
                }
            }
            
            PrintTopLine();
            for (var row = 0; row < maze.GetLength(1); row++)
            {
                Console.Write($"{row}\t");
                for (var column = 0; column < maze.GetLength(0); column++)
                {
                    Console.Write(maze[column, row]);
                }

                Console.WriteLine();
            }

            void PrintTopLine()
            {
                Console.Write($" \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10 == 0? i / 10 : " ");
                }
    
                Console.Write($"\n \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10);
                }
    
                Console.WriteLine("\n");
            }
        }
}
