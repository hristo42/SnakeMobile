using Microsoft.EntityFrameworkCore;
using SQLite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SnakeMobile.Database
{
    public class UserRepo(DatabaseService databaseService) : IUserRepo
    {
        private readonly SQLiteAsyncConnection _connection = databaseService.Connection;

        public async Task<User> Login(string username, string password)
        {
            var user = await _connection.Table<User>().FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && user.PasswordHash == HashPass(password))
            {
                return user;
            }

            return null;
        }

        public async Task Register(string username, string password)
        {
            var existingUser = await _connection.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return; // User already exists
            }

            var user = new User
            {
                Username = username,
                PasswordHash = HashPass(password)
            };

            await _connection.InsertAsync(user);
        }
        private string HashPass(string pass)
        {
            using (var sha256 = SHA256.Create()) // Create an instance of SHA256
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pass);

                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (var byteValue in hashBytes)
                {
                    hashStringBuilder.Append(byteValue.ToString("x2"));
                }

                return hashStringBuilder.ToString();
            }
        }

    }
}
