using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    abstract class Keys
    {

        Snake snake;
        Keys(Snake snake)
        {
            this.snake = snake;
        }

        public void LoadKeys()
        {
            Direction lastSnakeDir = snake.GetDirection().GetLast();
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Escape)
                Escape();
            else if (cki.Key == ConsoleKey.Spacebar)
                Space();
            else if (cki.Key == ConsoleKey.UpArrow && lastSnakeDir != 2)
                UpArrow();
            else if (cki.Key == ConsoleKey.RightArrow && lastSnakeDir != 3)
                RightArrow();
            else if (cki.Key == ConsoleKey.DownArrow && lastSnakeDir != 0)
                DownArrow();
            else if (cki.Key == ConsoleKey.LeftArrow && lastSnakeDir != 1)
                LeftArrow();
        }

        public abstract void Escape();
        public abstract void Space();
        public abstract void UpArrow();
        public abstract void RightArrow();
        public abstract void DownArrow();
        public abstract void LeftArrow();
    }
}
