using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SnakeMess
{
    class Controller
    {

        public Snake snake { get; private set; }

        public bool GameOver { get; private set; }
        public bool Pause { get; private set; }
        public bool InUse { get; private set; }

        Controller()
        {
            snake = new Snake();
            GameOver = false;
            Pause = false;
            InUse = false;
        }

        private void SetKeys()
        {
            if (Console.KeyAvailable)
            {

                Direction lastSnakeDir = snake.GetDirection().GetLast();
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape)
                    GameOver = true;
                else if (cki.Key == ConsoleKey.Spacebar)
                    Pause = false;
                else if (cki.Key == ConsoleKey.UpArrow && lastSnakeDir != 2)
                    snake.GetDirection().Set(Direction.UP);
                else if (cki.Key == ConsoleKey.RightArrow && lastSnakeDir != 3)
                    snake.GetDirection().Set(Direction.RIGHT);
                else if (cki.Key == ConsoleKey.DownArrow && lastSnakeDir != 0)
                    snake.GetDirection().Set(Direction.DOWN);
                else if (cki.Key == ConsoleKey.LeftArrow && lastSnakeDir != 1)
                    snake.GetDirection().Set(Direction.LEFT);
            }
        }

        private Point MoveHead(Point head)
        {

            Point newH = new Point(head);
            switch (snake.GetDirection().AsInt())
            {
                case Direction.UP:
                    newH.Y -= 1;
                    break;
                case Direction.RIGHT:
                    newH.X += 1;
                    break;
                case Direction.DOWN:
                    newH.Y += 1;
                    break;
                default:
                    newH.X -= 1;
                    break;
            }

            snake.SetHead(newH);
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
                        return true;
                    }
                return false;
            }

            if (!IsSpotAvailable()) return PlaceApple(board);
            else return apple;
        }

        private bool IsHeadOnApple(Point head, Point apple)
        {
            return head.X == apple.X && head.Y == apple.Y;
        }

        private void OnAppleEaten(Board board)
        {
            if (snake.Length() + 1 >= board.Width * board.Height)
                // No more room to place apples - game over.
                GameOver = true;
            else
            {
                PlaceApple(board);
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

        private void writeTail(Snake snake, Point apple, Point tail, Point head)
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
                Console.SetCursorPosition(apple.X, apple.Y);
                Console.Write("$");
                InUse = false;
            }

            snake.AddPoint(snake.GetHead());
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(snake.GetHead().X, snake.GetHead().Y);
            Console.Write("@");
        }

        private void CheckForSelfCollide()
        {
            snake.RemovePointAt(0);
            foreach (Point x in snake.Points)
                if (x.X == snake.GetHead().X && x.Y == snake.GetHead().Y)
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

            Direction newDir = new Direction(Direction.DOWN);
            Direction lastDir = newDir.GetLast();

            Board board = new Board();

            Random random = new Random();

            for (int i = 0; i < 4; i++)
                ctrlr.snake.AddPoint(new Point(10, 10));

            ctrlr.WindowSettings();

            Point apple = ctrlr.PlaceApple(board);

            Stopwatch t = new Stopwatch();
            t.Start();

            while (!ctrlr.GameOver)
            {

                ctrlr.SetKeys();

                if (!ctrlr.Pause)
                {
                    if (t.ElapsedMilliseconds < 100)
                        continue;

                    t.Restart();

                    Point tail = new Point(snake.GetHead());
                    Point head = new Point(snake.GetEnd());
                    Point newHead = ctrlr.MoveHead(head);

                    if (ctrlr.IsWindowCollide(newHead, board))
                        ctrlr.GameOver = true;


                    if (ctrlr.IsHeadOnApple(newHead, apple))
                        ctrlr.OnAppleEaten(board);

                    if (!ctrlr.InUse)
                    {
                        ctrlr.CheckForSelfCollide();
                    }


                    if (!ctrlr.GameOver)
                    {
                        ctrlr.writeTail(snake, apple, tail, head);
                    }
                }
            }

        }
    }
}
