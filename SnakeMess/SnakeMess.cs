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
                    gameOver = true;
                else if (cki.Key == ConsoleKey.Spacebar)
                    pause = !pause;
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

        private void MoveHead()
        {
            Point tail = new Point(snake.GetHead());
            Point head = new Point(snake.GetEnd());
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

        public static void Main(string[] arguments)
        {
            SnakeMess snakeMess = new SnakeMess();
            Snake snake = snakeMess.snake;

            snakeMess.GameOver = false;
            snakeMess.Pause = false;
            snakeMess.InUse = false;

            Direction newDir = new Direction(Direction.DOWN); // 0 = up, 1 = right, 2 = down, 3 = left
            Direction lastDir = newDir.GetLast();

            Board board = new Board();

            Random random = new Random();

            for (int i = 0; i < 4; i++)
                snakeMess.snake.AddPoint(new Point(10, 10));

            snakeMess.WindowSettings();

            snakeMess.PlaceApple(board);

            Stopwatch t = new Stopwatch();
            t.Start();

            

			while (!snakeMess.GameOver) {

                snakeMess.SetKeys();

				if (!snakeMess.Pause) {
					if (t.ElapsedMilliseconds < 100)
						continue;

					t.Restart();
                    snakeMess.MoveHead();
				}


					if (newH.X < 0 || newH.X >= boardW)
						snakeMess.GameOver = true;
					else if (newH.Y < 0 || newH.Y >= boardH)
                        snakeMess.GameOver = true;


					if (newH.X == app.X && newH.Y == app.Y) {
						if (snake.Count + 1 >= boardW * boardH)
                            // No more room to place apples - game over.
                            snakeMess.GameOver = true;
						else {
							while (true) {
								app.X = random.Next(0, boardW); app.Y = random.Next(0, boardH);
								bool found = true;
								foreach (Point i in snake)
									if (i.X == app.X && i.Y == app.Y) {
										found = false;
										break;
									}
								if (found) {
									inUse = true;
									break;
								}
							}
						}
					}


					if (!inUse) {
						snake.RemoveAt(0);
						foreach (Point x in snake)
							if (x.X == newH.X && x.Y == newH.Y) {
								// Death by accidental self-cannibalism.
								gg = true;
								break;
							}
					}


					if (!gg) {
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.SetCursorPosition(head.X, head.Y); Console.Write("0");
						if (!inUse) {
							Console.SetCursorPosition(tail.X, tail.Y); Console.Write(" ");
						} else {
							Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(app.X, app.Y); Console.Write("$");
							inUse = false;
						}
						snake.Add(newH);
						Console.ForegroundColor = ConsoleColor.Yellow; Console.SetCursorPosition(newH.X, newH.Y); Console.Write("@");
						last = newDir;
					}
				}
			}







		
    }
}