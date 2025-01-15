using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMobile.ViewModels
{
    internal class Food
    {
        public CoordinatePoint Location;

        // Конатруктор + начална позиция
        public Food(int rows, int columns)
        {
            Spawn(rows, columns);
        }

        // Генериране на храната
        public void Spawn(int rows, int columns)
        {
            Random random = new Random();
            int x = random.Next(1, columns - 1);
            int y = random.Next(1, rows - 1);
            Location = new CoordinatePoint(x, y);
        }
    }
}
