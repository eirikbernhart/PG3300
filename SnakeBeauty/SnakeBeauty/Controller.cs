using System;
using System.Diagnostics;
using System.Linq;

namespace SnakeBeauty
{
    //Controls the game logic
    internal class Controller
    {
        private Snake Snake { get; }

        private Point Apple { get; set; }

        private bool GameOver { get; set; }
        private bool Pause { get; set; }
        private bool InUse { get; set; }

        private Controller()
        {
            Snake = new Snake();
            GameOver = false;
            Pause = false;
            InUse = false;
        }

        //Checks for keypresses and does some functionality based on the key pressed.
        private void CheckKeys(Direction last)
        {
            if (!Console.KeyAvailable) return;
            var cki = Console.ReadKey(true);
            switch (cki.Key)
            {
                case ConsoleKey.Escape:
                    GameOver = true;
                    break;
                case ConsoleKey.Spacebar:
                    Pause = !Pause;
                    break;
                case ConsoleKey.UpArrow when last != 2:
                    Snake.ChangeDirection(Direction.Up);
                    break;
                case ConsoleKey.RightArrow when last != 3:
                    Snake.ChangeDirection(Direction.Right);
                    break;
                case ConsoleKey.DownArrow when last != 0:
                    Snake.ChangeDirection(Direction.Down);
                    break;
                case ConsoleKey.LeftArrow when last != 1:
                    Snake.ChangeDirection(Direction.Left);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        //Moves the head of the snake and returns it's new position (Point)
        private Point MoveHead(Point head)
        {

            var newH = new Point(head);
            var dir = Snake.GetDirection();
            if (dir == Direction.Up)
                newH.Y -= 1;
            else if (dir == Direction.Right)
                newH.X += 1;
            else if (dir == Direction.Down)
                newH.Y += 1;
            else
                newH.X -= 1;

            return newH;
        }
        //Does functionality like setting mouse pointer visibility, title, color, and writes the snake head at startup
        private static void WindowSettings()
        {
            Console.CursorVisible = false;
            Console.Title = "Westerdals Oslo ACT - SNAKE";
            Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(10, 10); Console.Write("@");
        }
        //Places an apple at a random point on the board, returns the apples "Point"
        private Point PlaceApple(Board board)
        {
            while (true)
            {
                var apple = Point.GetRandomPoint(board);

                bool IsSpotAvailable()
                {
                    if (!Snake.Points.Any(i => i.X != apple.X && i.Y != apple.Y)) return false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(apple.X, apple.Y);
                    Console.Write("$");
                    InUse = true;
                    return true;
                }

                if (!IsSpotAvailable()) continue;
                return apple;
            }
        }

        //Returns true if the snake heads "Point" has the same position on the board as the apple
        private bool IsHeadOnApple(Point head)
        {
            return head.X == Apple.X && head.Y == Apple.Y;
        }
        //Functionality for what happens when an apple is eaten.
        private void OnAppleEaten(Board board)
        {
            if (Snake.Length() + 1 >= board.Width * board.Height)
                // No more room to place apples - game over.
                GameOver = true;
            else
            {
                Apple = PlaceApple(board);
            }
        }
        //Returns true if Snake head collided with the window (hits the corner of the board)
        private static bool IsWindowCollide(Point head, Board board)
        {
            if (head.X < 0 || head.X >= board.Width)
                return true;
            else if (head.Y < 0 || head.Y >= board.Height)
                return true;

            return false;
        }
        //Write updated snake to console
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
        //Functionality for what happens if the snake collides with itself
        private void CheckForSelfCollide(Point newH)
        {
            Snake.RemovePointAt(0);
            foreach (var x in Snake.Points)
                if (x.X == newH.X && x.Y == newH.Y)
                {
                    // Death by accidental self-cannibalism.
                    GameOver = true;
                    break;
                }
        }

        //The games algorithm
        public static void Main(string[] arguments)
        {
            var ctrlr = new Controller();
            var snake = ctrlr.Snake;

            ctrlr.GameOver = false;
            ctrlr.Pause = false;
            ctrlr.InUse = false;

            var board = new Board();

            for (var i = 0; i < 4; i++)
                ctrlr.Snake.AddPoint(new Point(10, 10));

            WindowSettings();

            ctrlr.Apple = ctrlr.PlaceApple(board);

            var lastDir = new Direction(Direction.Down);

            var t = new Stopwatch();
            t.Start();

            while (!ctrlr.GameOver)
            {
                ctrlr.CheckKeys(lastDir);

                if (ctrlr.Pause) continue;
                if (t.ElapsedMilliseconds < 100)
                    continue;

                t.Restart();

                var tail = new Point(snake.GetEnd());
                var head = new Point(snake.GetHead());
                var newHead = ctrlr.MoveHead(head);

                if (IsWindowCollide(newHead, board))
                    ctrlr.GameOver = true;


                if (ctrlr.IsHeadOnApple(newHead))
                    ctrlr.OnAppleEaten(board);

                if (!ctrlr.InUse)
                {
                    ctrlr.CheckForSelfCollide(newHead);
                }


                if (ctrlr.GameOver) continue;
                ctrlr.WriteTail(snake, tail, newHead);
                lastDir = snake.GetDirection();
            }

        }
    }
}
