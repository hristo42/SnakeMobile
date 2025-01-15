using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeMobile.Database
{
    public interface IUserRepo
    {
        public Task<User> Login(string username, string password);
        public Task Register(string username, string password);
    }
}
