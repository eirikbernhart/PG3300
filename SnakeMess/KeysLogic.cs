using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class KeysLogic : UserKeys
    {

        Snake snake;

        public KeysLogic(Snake snake)
        {
            this.snake = snake;
        }

        public void LoadKeysLogic()
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

        public void DownArrow()
        {
            throw new NotImplementedException();
        }

        public void Escape()
        {
            throw new NotImplementedException();
        }

        public void LeftArrow()
        {
            throw new NotImplementedException();
        }

        public void RightArrow()
        {
            throw new NotImplementedException();
        }

        public void Space()
        {
            throw new NotImplementedException();
        }

        public void UpArrow()
        {
            throw new NotImplementedException();
        }
    }
}
