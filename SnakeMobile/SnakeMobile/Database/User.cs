using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SnakeMobile.Database
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50), NotNull]
        public string Username { get; set; }
        public int Score { get; set; }
        [NotNull]
        public string PasswordHash { get; set; }
        public string PreferredSnakeColor { get; set; }
    }
}
