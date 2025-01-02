using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMobileApp.ViewModels
{
    internal class Snake
    {
        public List<Point> Body { get; set; }
        public Point Head
        {
            get
            {
                return Body[0];
            }
        }

        private int directionX;
        private int directionY;

        public Snake()
        {
            Body = new List<Point>
            {
                new Point(20, 10),
                new Point(19, 10),
                new Point(18, 10),
                new Point(17, 10)
            };
            directionX = 1;
            directionY = 0;
        }

        //Преместване на змията
        public void Update()
        {
            // Премеване на тялото на змията
            for (int i = Body.Count - 1; i > 0; i--)
            {
                Body[i].X = Body[i - 1].X;
                Body[i].Y = Body[i - 1].Y;
            }

            // Преместване на главата на змията
            Head.X += directionX;
            Head.Y += directionY;
        }

        // Принтиране на змията
        public void Draw()
        {
            foreach (var point in Body)
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write("*");
            }
        }

        // Контролиране на движението на змията
        public void ChangeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (directionY == 0)
                    {
                        directionX = 0;
                        directionY = -1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (directionY == 0)
                    {
                        directionX = 0;
                        directionY = 1;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (directionX == 0)
                    {
                        directionX = -1;
                        directionY = 0;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (directionX == 0)
                    {
                        directionX = 1;
                        directionY = 0;
                    }
                    break;
            }
        }

        // Разтеж на змията
        public void Grow()
        {
            Point newTail = new Point(Body[Body.Count - 1].X, Body[Body.Count - 1].Y);
            Body.Add(newTail);
        }

        // Проверка за захапване
        public bool IsCollidingWithTail()
        {
            for (int i = 1; i < Body.Count; i++)
            {
                if (Head.X == Body[i].X && Head.Y == Body[i].Y)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
