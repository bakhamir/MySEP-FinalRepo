using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAuthForm.Models;
using OfficeOpenXml;
using System.Diagnostics;
using System.Security.Claims;
using Dapper;
using System.Data.SqlClient;

namespace MyAuthForm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
  
        private readonly ILogger<HomeController> _logger;
        IConfiguration config;
        public HomeController(IConfiguration config)
        {
            this.config = config;
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["role"] = User.FindFirstValue(ClaimTypes.Role);
            ViewBag.id = User.FindFirstValue("id");
            return View();
        }

        public IActionResult Privacy()
        {
            if (User.FindFirstValue(ClaimTypes.Role) != "admin")
                return Redirect("~/Home/Error/UnAuthorized");
            return View();
        }

        public IActionResult ExportToExcel()
        {
            // Получаем данные из базы данных
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                List<User> users = db.Query<User>("pGetAllUsers", commandType: System.Data.CommandType.StoredProcedure).ToList();
                List<Roles> roles = db.Query<Roles>("pGetAllRoles", commandType: System.Data.CommandType.StoredProcedure).ToList();

            }

            // Создаем путь к файлу Excel
            var fileName = $"users_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

            // Записываем данные в Excel файл
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.Add("Users");

                // Записываем заголовки
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Login";
                worksheet.Cells[1, 3].Value = "Password";
                worksheet.Cells[1, 4].Value = "Role";

                // Записываем данные из базы данных
                int row = 2;
                foreach (var user in users)
                {
                    worksheet.Cells[row, 1].Value = user.Id;
                    worksheet.Cells[row, 2].Value = user.Login;
                    worksheet.Cells[row, 3].Value = user.Password;
                    // Найдем роль пользователя по Id
                    var roleName = roles.Find(r => r.Id == user.RoleId)?.Name;
                    worksheet.Cells[row, 4].Value = roleName;
                    row++;
                }

                package.Save();
            }

            // Возвращаем файл пользователю
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public ActionResult Error(string id)
        {
            ViewData["textError"] = id;
            return View();
        }
    }
}