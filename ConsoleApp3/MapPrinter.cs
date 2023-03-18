namespace ConsoleApp3;
// При малюванні карти, зробіть так, щоб перша точка відображалась символом А,
// остання - В, а всі інші - крапками. Якщо ви почнете з цього, буде значно легше дебажити всю програму,
// оскільки завжди буде легко виводити шлях на екран , List<Point> path
public class MapPrinter
{
    public void Print(string[,] maze)
        {
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
