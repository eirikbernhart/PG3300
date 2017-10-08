using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SnakeBeauty
{
    class Controller
    {

        Snake snake { get; set; }

        Point Apple { get; set; }

        bool GameOver { get; set; }
        bool Pause { get; set; }
        bool InUse { get; set; }

        Controller()
        {
            snake = new Snake();
            GameOver = false;
            Pause = false;
            InUse = false;
        }

        private void CheckKeys(Direction last)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape)
                    GameOver = true;
                else if (cki.Key == ConsoleKey.Spacebar)
                    Pause = false;
                else if (cki.Key == ConsoleKey.UpArrow && last != 2)
                    snake.ChangeDirection(Direction.UP);
                else if (cki.Key == ConsoleKey.RightArrow && last != 3)
                    snake.ChangeDirection(Direction.RIGHT);
                else if (cki.Key == ConsoleKey.DownArrow && last != 0)
                    snake.ChangeDirection(Direction.DOWN);
                else if (cki.Key == ConsoleKey.LeftArrow && last != 1)
                    snake.ChangeDirection(Direction.LEFT);
            }
        }

        private Point MoveHead(Point head)
        {

            Point newH = new Point(head);
            Direction dir = snake.GetDirection();
            if (dir == Direction.UP)
                newH.Y -= 1;
            else if (dir == Direction.RIGHT)
                newH.X += 1;
            else if (dir == Direction.DOWN)
                newH.Y += 1;
            else
                newH.X -= 1;

            return newH;
        }

        private void WindowSettings()
        {
            Console.CursorVisible = false;
            Console.Title = "Westerdals Oslo ACT - SNAKE";
            Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(10, 10); Console.Write("@");
        }

        private Point PlaceApple(Board board)
        {
            Point apple = Point.GetRandomPoint(board);

            bool IsSpotAvailable()
            {
                foreach (Point i in snake.Points)
                    if (i.X != apple.X && i.Y != apple.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(apple.X, apple.Y);
                        Console.Write("$");
                        InUse = true;
                        return true;
                    }
                return false;
            }

            if (!IsSpotAvailable()) return PlaceApple(board);
            else return apple;
        }

        private bool IsHeadOnApple(Point head)
        {
            return head.X == Apple.X && head.Y == Apple.Y;
        }

        private void OnAppleEaten(Board board)
        {
            if (snake.Length() + 1 >= board.Width * board.Height)
                // No more room to place apples - game over.
                GameOver = true;
            else
            {
                Apple = PlaceApple(board);
            }
        }

        private bool IsWindowCollide(Point head, Board board)
        {
            if (head.X < 0 || head.X >= board.Width)
                return true;
            else if (head.Y < 0 || head.Y >= board.Height)
                return true;

            return false;
        }

        private void WriteTail(Snake snake, Point tail, Point newH)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(snake.GetHead().X, snake.GetHead().Y);
            Console.Write("0");

            if (!InUse)
            {
                Console.SetCursorPosition(tail.X, tail.Y);
                Console.Write(" ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(Apple.X, Apple.Y);
                Console.Write("$");
                InUse = false;
            }

            snake.SetHead(newH);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(newH.X, newH.Y);
            Console.Write("@");
        }

        private void CheckForSelfCollide(Point newH)
        {
            snake.RemovePointAt(0);
            foreach (Point x in snake.Points)
                if (x.X == newH.X && x.Y == newH.Y)
                {
                    // Death by accidental self-cannibalism.
                    GameOver = true;
                    break;
                }
        }

        public static void Main(string[] arguments)
        {
            Controller ctrlr = new Controller();
            Snake snake = ctrlr.snake;

            ctrlr.GameOver = false;
            ctrlr.Pause = false;
            ctrlr.InUse = false;

            Board board = new Board();

            for (int i = 0; i < 4; i++)
                ctrlr.snake.AddPoint(new Point(10, 10));

            ctrlr.WindowSettings();

            ctrlr.Apple = ctrlr.PlaceApple(board);

            Direction lastDir = new Direction(Direction.DOWN);

            Stopwatch t = new Stopwatch();
            t.Start();

            while (!ctrlr.GameOver)
            {
                ctrlr.CheckKeys(lastDir);

                if (!ctrlr.Pause)
                {
                    if (t.ElapsedMilliseconds < 100)
                        continue;

                    t.Restart();

                    Point tail = new Point(snake.GetEnd());
                    Point head = new Point(snake.GetHead());
                    Point newHead = ctrlr.MoveHead(head);

                    if (ctrlr.IsWindowCollide(newHead, board))
                        ctrlr.GameOver = true;


                    if (ctrlr.IsHeadOnApple(newHead))
                        ctrlr.OnAppleEaten(board);

                    if (!ctrlr.InUse)
                    {
                        ctrlr.CheckForSelfCollide(newHead);
                    }


                    if (!ctrlr.GameOver)
                    {
                        ctrlr.WriteTail(snake, tail, newHead);
                        lastDir = snake.GetDirection();
                    }
                }
            }

        }
    }
}
