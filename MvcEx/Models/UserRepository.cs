using Dapper;
using System.Data.SqlClient;

namespace MvcEx.Models
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Users GetUserById(string id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Users>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
            }
        }

        public Users GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Users>("SELECT * FROM Users WHERE UserName = @UserName", new { UserName = username });
            }
        }

        public void CreateUser(string UserName,string PasswordHash,string email)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Users ( UserName, PasswordHash, Email) VALUES (@UserName, @PasswordHash, @Email)",new { UserName, PasswordHash, email });
            }
        }

        internal object GetUserByEmail(string email)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Users>("SELECT * FROM Users WHERE email = @email", new { email = email });
            }
        }

        // Добавьте методы для обновления и удаления пользователей при необходимости
    }
}
