using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAuthForm.Models;
using System.Security.Claims;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace MyAuthForm.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user)
        {
            if (true)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, user.Login) };
                var identity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(identity));
                return Redirect("~/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Login failed. Please check Username and/or password");
                return View();
               
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
        public ActionResult Register(User user)
        {


            string conStr = "Server=206-4\\SQLEXPRESS;Database=testdb;Trusted_Connection=True;";
            using (SqlConnection db = new SqlConnection(conStr))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("login", user.Login);
                p.Add("password", user.Password);
                db.Query<User>("pUser", p, commandType: System.Data.CommandType.StoredProcedure);
            }
            var claims = new[] { new Claim(ClaimTypes.Name, user.Login) };
            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return Redirect("~/Home/Index");
        }
    }
}

//create table uuser(
//id int identity,
//Login nvarchar(max),
//Password nvarchar(max))

//alter proc pUser
//@login nvarchar(max),
//@password nvarchar(max)
//as
//insert into uuser values(@login, pwdencrypt(@password))

//pUser 'john', 'doe123123'

//select * from uuser