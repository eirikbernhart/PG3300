using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class UserKeys
    {
        UserKeys(Snake snake)
        {
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Escape)
                Escape();
            else if (cki.Key == ConsoleKey.Spacebar)
                Space();
            else if (cki.Key == ConsoleKey.UpArrow && last != 2)
                UpArrow()
            else if (cki.Key == ConsoleKey.RightArrow && last != 3)
                RightArrow();
            else if (cki.Key == ConsoleKey.DownArrow && last != 0)
                DownArrow();
            else if (cki.Key == ConsoleKey.LeftArrow && last != 1)
                LeftArrow();

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape)
                    gg = true;
                else if (cki.Key == ConsoleKey.Spacebar)
                    pause = !pause;
                else if (cki.Key == ConsoleKey.UpArrow && last != 2)
                    newDir = 0;
                else if (cki.Key == ConsoleKey.RightArrow && last != 3)
                    newDir = 1;
                else if (cki.Key == ConsoleKey.DownArrow && last != 0)
                    newDir = 2;
                else if (cki.Key == ConsoleKey.LeftArrow && last != 1)
                    newDir = 3;
            }
        }

        void Escape() { }
        void Space() { }
        void UpArrow() { }
        void RightArrow() { }
        void DownArrow() { }
        void LeftArrow() { }
    }
}
