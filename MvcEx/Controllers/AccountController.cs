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
                new Claim(ClaimTypes.Role, "YourRole"), // Замените "YourRole" на реальную роль пользователя
                new Claim("id", result.id.ToString())
                // Добавьте другие утверждения, если необходимо
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        // Здесь вы можете настроить свойства аутентификации, если необходимо
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> Register(Users user)
        {
            using (SqlConnection db = new SqlConnection(_config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters(user);
                var result = await db.QueryAsync<Users>("pUsers", p, commandType: CommandType.StoredProcedure);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}