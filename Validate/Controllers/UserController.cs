using Microsoft.AspNetCore.Mvc;
using Validate.Models;
using System.Data.SqlClient;
using Dapper;
namespace Validate.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly string _connectionString;

        public UserController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Проверка уникальности логина
                int existingCount = connection.QuerySingleOrDefault<int>(
                    "SELECT COUNT(*) FROM Users WHERE Login = @Login", new { user.Login });

                if (existingCount > 0)
                {
                    return false; // Логин уже занят
                }

                // Добавление нового пользователя
                connection.Execute(
                    "INSERT INTO Users (Login, Password, Email, DateTime) VALUES (@Login, @Password, @Email, @DateTime)",
                    new
                    {
                        user.Login,
                        user.Password,
                        user.Email,
                        user.DateTime
                    });
            }

            return true; // Пользователь успешно добавлен
        }
  
    }
}
