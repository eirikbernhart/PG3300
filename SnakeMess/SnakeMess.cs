using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

// WARNING: DO NOT code like this. Please. EVER! 
//          "Aaaargh!" 
//          "My eyes bleed!" 
//          "I facepalmed my facepalm." 
//          Etc.
//          I had a lot of fun obfuscating this code! And I can now (proudly?) say that this is the uggliest short piece of code I've ever written!
//          (And yes, it could have been ugglier. But the idea wasn't to make it fuggly-uggly, just funny-uggly or sweet-uggly.)
//
//          -Tomas
//
namespace SnakeMess
{

    class SnakeMess
    {

        public Snake snake { get; private set; }

        public bool GameOver { get; private set; }
        public bool Pause { get; private set; }
        public bool InUse { get; private set; }

        SnakeMess()
        {
            snake = new Snake();
            GameOver = false;
            Pause = false;
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

            bool IsSpotAvailable() {
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

        private void OnGameOver(Snake snake, Point apple, Point tail, Point head)
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

        private void OnSelfCollide()
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
            SnakeMess snakeMess = new SnakeMess();
            Snake snake = snakeMess.snake;

            snakeMess.GameOver = false;
            snakeMess.Pause = false;
            snakeMess.InUse = true;

            Direction newDir = new Direction(Direction.DOWN);
            Direction lastDir = newDir.GetLast();

            Board board = new Board();

            Random random = new Random();

            for (int i = 0; i < 4; i++)
                snakeMess.snake.AddPoint(new Point(10, 10));

            snakeMess.WindowSettings();

            Point apple = snakeMess.PlaceApple(board);

            Stopwatch t = new Stopwatch();
            t.Start();

            while (!snakeMess.GameOver)
            {

                snakeMess.SetKeys();

                if (!snakeMess.Pause)
                {
                    if (t.ElapsedMilliseconds < 100)
                        continue;

                    t.Restart();

                    Point tail = new Point(snake.GetHead());
                    Point head = new Point(snake.GetEnd());
                    Point newHead = snakeMess.MoveHead(head);

                    if (snakeMess.IsWindowCollide(newHead, board))
                        snakeMess.GameOver = true;


                    if (snakeMess.IsHeadOnApple(newHead, apple))
                        snakeMess.OnAppleEaten(board);

                    if (!snakeMess.InUse)
                    {
                        snakeMess.OnSelfCollide();
                    }


                    if (snakeMess.GameOver)
                    {
                        snakeMess.OnGameOver(snake, apple, tail, head);
                    }
                }
            }

        }
    }
}