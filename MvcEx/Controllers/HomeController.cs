using Microsoft.AspNetCore.Mvc;
using MvcEx.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using Dapper;
namespace MvcEx.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Book;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            // Получение списка книг из базы данных
            List<Books> books;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();                                              
                books = connection.Query<Books>("SELECT * FROM Books").AsList();
            }

            // Передача списка книг в представление
            return View(books);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult RateBook(int id, int rating)
        {
            // Здесь нужно реализовать логику для изменения рейтинга книги с определенным id в базе данных
            // Пример: вызов хранимой процедуры для обновления рейтинга книги
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("EXEC RateBook @id, @rating", new { id, rating });
            }

            // Перенаправление на главную страницу после обновления рейтинга
            return RedirectToAction("Index");
        }

        public ActionResult BookDetails(int id)
        {
            // Получение информации о книге с определенным id из базы данных
            Books book;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                book = connection.QueryFirstOrDefault<Books>("SELECT * FROM Books WHERE id = @id", new { id });
            }

            // Передача информации о книге в представление для отображения
            return View(book);
        }
    }
}