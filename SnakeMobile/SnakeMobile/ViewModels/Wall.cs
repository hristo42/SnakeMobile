using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMobile.ViewModels
{
    internal class Wall
    {
        public List<CoordinatePoint> Body = new List<CoordinatePoint>();

        // Конструктор зависещ от размера на координатната система
        public Wall(int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                Body.Add(new CoordinatePoint(0, i));
                Body.Add(new CoordinatePoint(columns - 1, i));
            }
            for (int i = 0; i < columns; i++)
            {
                Body.Add(new CoordinatePoint(i, 0));
                Body.Add(new CoordinatePoint(i, rows - 1));
            }
        }

        // Проверка за сблъсък със змията
        public bool IsCollidingWithWall(CoordinatePoint snakePoint)
        {
            foreach (var wallPoint in Body)
            {
                if (snakePoint.X == wallPoint.X && snakePoint.Y == wallPoint.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }

    

    }
