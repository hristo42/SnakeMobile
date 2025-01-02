using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SnakeGame.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        public int Score { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string PreferredSnakeColour { get; set; }

    }
}
