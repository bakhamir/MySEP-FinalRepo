using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WinAuthHw.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        [Authorize(Users = "user*")]
        public ActionResult Index1()
        {
            return Content("This is Index1 method. You are allowed to access because your username starts with 'user'.");
        }

        [Authorize(Users = "admin*")]
        public ActionResult Index2()
        {
            return Content("This is Index2 method. You are allowed to access because your username starts with 'admin'.");
        }
    }
}
