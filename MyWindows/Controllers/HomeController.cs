using Microsoft.AspNetCore.Mvc;
using MyWindows.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using Dapper;

namespace MyWindows.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var user = User.Identity.Name;
            var username = User.Identity.Name.Split('\\')[1];

            string query = $"select 1 from [user] where [user] = '{username}'";
            using (SqlConnection db = new SqlConnection("Server=206-4\\SQLEXPRESS;Database=Win;Trusted_Connection=True;"))
            {
                var result = db.ExecuteScalar(query);
                if (result != null)
                {
                    return View();
                }
                else
                {
                  return  RedirectToAction("GetError");
                }
            }
        }
        //   

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult GetError()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}