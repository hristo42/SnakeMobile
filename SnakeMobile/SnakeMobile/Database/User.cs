using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMobile.Database
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
        public string PreferredSnakeColor { get; set; }
    }
}
