using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SnakeGame.Models;

namespace SnakeMobileApp.ViewModels
{
    internal class Game
    {
        //public int Id { get; set; }
        public bool IsGameOver { get; set; }
        //public DateTime StartTime { get; set; }
        //public DateTime EndTime { get; set; }
        //[ForeignKey(nameof(User))]
        //public int UserId { get; set; }
        //public User? User { get; set; }
        public Game()
        {
        }
    }
}
