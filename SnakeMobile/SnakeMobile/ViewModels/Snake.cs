using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMobile.ViewModels
{
    internal class Snake
    {
        public List<CoordinatePoint> Body;
        public CoordinatePoint Head
        {
            get
            {
                return Body[0];
            }
        }

        public Color SnakeColor = Colors.Blue;

        private int directionX;
        private int directionY;

        // Създава змия в началото на координатната система
        public Snake()
        {
            Body = new List<CoordinatePoint>
            {
                new CoordinatePoint(4, 1),
                new CoordinatePoint(3, 1),
                new CoordinatePoint(2, 1),
                new CoordinatePoint(1, 1)
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

        // Контролиране на движението на змията
        public void ChangeDirectionUp()
        {
            if (directionY == 0)
            {
                directionX = 0;
                directionY = -1;
            }
        }

        public void ChangeDirectionDown()
        {
            if (directionY == 0)
            {
                directionX = 0;
                directionY = 1;
            }
        }
        public void ChangeDirectionLeft()
        {
            if (directionX == 0)
            {
                directionX = -1;
                directionY = 0;
            }
        }

        public void ChangeDirectionRight()
        {
            if (directionX == 0)
            {
                directionX = 1;
                directionY = 0;
            }
        }

        // Разтеж на змията
        public void Grow()
        {
            CoordinatePoint newTail = new CoordinatePoint(Body[^1].X, Body[^1].Y);
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
