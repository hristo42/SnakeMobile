using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SnakeMobile.Database
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _connection;

        public DatabaseService(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<User>().Wait(); // Ensure the table is created
        }

        public SQLiteAsyncConnection Connection => _connection;
    }
}
