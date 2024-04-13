using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcEx.Models;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

namespace MvcEx.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Users user)
        {
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                var result = await db.QueryFirstOrDefaultAsync("pUsers;2", new { login = user.username, pwd = user.pwd }, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    ModelState.AddModelError("", "Login failed. Please check Username and/or password");
                    return View();
                }
                else
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, "User"), 
                new Claim("id", result.id.ToString())
                 
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                         
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Users user)
        {
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                var p = new DynamicParameters();
                p.Add("@username", user.username);
                p.Add("@pwd", user.pwd);
                var result = await db.ExecuteAsync("pUsers", p, commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction("Login");  
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public ActionResult RedirectToRegister()
        {
            return RedirectToAction("Register", "Account");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUsername(string newUsername)
        {
           
   
            var userId = User.FindFirstValue("id");
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {

                db.Query($"UPDATE Users SET username = '{newUsername}' WHERE id = {userId}");

                TempData["SuccessMessage"] = "name changed.";

            }

            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser()
        {
 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

 
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                var Id = User.FindFirstValue("id");
                db.Query($"DELETE FROM Users WHERE id = {Id}");

            }

      
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

 
            return RedirectToAction("Index", "Home");
        }
    }
}