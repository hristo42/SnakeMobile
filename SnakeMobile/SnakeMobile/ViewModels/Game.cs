using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Models;

namespace SnakeMobileApp.ViewModels
{
    internal class Game
    {
        public int Id { get; set; }
        public bool IsGameOver { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }
        public Game()
        {
            StartGame();
        }
        public void StartGame()
        {
            // Настройване на конзолата
            Console.Title = "Snake Game";
            Console.CursorVisible = false;
            Console.WindowHeight = 30;
            Console.WindowWidth = 120;

            // Генериране на обекти
            Snake snake = new Snake();
            Food food = new Food();
            Wall wall = new Wall();
            int score = 0;

            // Цикъл на играта
            bool isGameOver = false;
            while (!isGameOver)
            {
                // Преместване на змията
                snake.Update();

                // Ядене на храна
                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Grow();
                    food.Spawn();
                    score += 100;
                }

                // Сблъсък със стена
                if (wall.IsCollidingWith(snake.Head))
                {
                    isGameOver = true;
                }

                // Захапване на опашката
                if (snake.IsCollidingWithTail())
                {
                    isGameOver = true;
                }

                // Принтиране на преместените обекти
                Console.Clear();
                snake.Draw();
                food.Draw();
                wall.Draw();

                // Контролиране на движението на змията
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    snake.ChangeDirection(key);
                }
                else
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            // Край на играта
            IsGameOver = true;
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.WriteLine($"Score = {score}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
