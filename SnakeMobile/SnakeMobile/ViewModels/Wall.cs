using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMobileApp.ViewModels
{
    internal class Wall
    {
        private List<Point> body;

        public Wall()
        {
            body = new List<Point>();
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                body.Add(new Point(i, 0));
                body.Add(new Point(i, Console.WindowHeight - 1));
            }
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                body.Add(new Point(0, i));
                body.Add(new Point(Console.WindowWidth - 1, i));
            }
        }

        // Принтиране на стената
        public void Draw()
        {
            foreach (var point in body)
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write("#");
            }
        }

        // Проверка за сблъсък
        public bool IsCollidingWith(Point point)
        {
            foreach (var wallPoint in body)
            {
                if (point.X == wallPoint.X && point.Y == wallPoint.Y)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
