using Microsoft.AspNetCore.Mvc;
using MvcEx.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using static System.Reflection.Metadata.BlobBuilder;

namespace MvcEx.Controllers
{
    [Authorize]
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
   
            List<Books> books;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();                                              
                books = connection.Query<Books>("SELECT * FROM Books").AsList();
            }

            
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
           

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("EXEC RateBook @id, @rating", new { id, rating });
            }

            return RedirectToAction("Index");
        }

        public ActionResult BookDetails(int id)
        {

            Books book;
            List<string> comments;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                book = connection.QueryFirstOrDefault<Books>("SELECT * FROM Books WHERE id = @id", new { id });
                comments = connection.Query<string>("SELECT comment FROM Comments WHERE bookId = @id", new { id }).AsList();
            }
            book.comments = comments;

          
            return View(book);
        }

        [HttpPost]
        public ActionResult AddComment(int bookId, string comment)
        {
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Comments (bookId, comment) VALUES (@bookId, @comment)", new { bookId, comment });
            }

            
            return RedirectToAction("BookDetails", new { id = bookId });
        }
        public ActionResult Search(string searchTerm)
        {
           
            List<Books> books;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                books = connection.Query<Books>("SELECT * FROM Books WHERE title LIKE @searchTerm", new { searchTerm = "%" + searchTerm + "%" }).AsList();
            }

            
            return View("Index", books);
        }
        public ActionResult SortByRating()
{
    
    List<Books> books;
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        books = connection.Query<Books>("SELECT * FROM Books ORDER BY rating DESC").AsList();
    }

    
    return View("Index", books);
}

        [HttpGet]
        public async Task<ActionResult> AddBook()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBook(Books book)
        {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    var result = db.Query<Books>($"INSERT INTO Books (title, genre, written, rating, author, contents)" +
                        $"VALUES ('{book.title}', '{book.genre}', '{book.written}', '{book.rating}', '{book.author}', '{book.contents}');\r\n");
                }
            return RedirectToAction("Index", "Home");
        }


    }
}