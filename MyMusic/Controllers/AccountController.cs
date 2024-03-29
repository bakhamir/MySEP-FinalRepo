﻿using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAuthForm.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;

namespace MyMusic.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        IConfiguration config;
        public AccountController(IConfiguration config)
        {
            this.config = config;
        }

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
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                var result = db.Query<dynamic>("pUsers", new { login = user.Login, pwd = user.Password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (result == null)
                {
                    ModelState.AddModelError("", "Login failed. Please check Username and/or password");
                    return View();
                }

                int id = result.id;
                string login = result.login;
                string role_name = result.role_name;
                var claims = new[] { new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, role_name),
                    new Claim("id", id.ToString())
                };
                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity)); 
                return Redirect("~/Mus/Index");

            }
            if (true)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim("c1", "text")
                };
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
        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    using (SqlConnection db = new SqlConnection(config["conStr"]))
        //    {
        //        var result = db.Query<Roles>("pRoles", new { login = user.Login, pwd = user.Password }, commandType: CommandType.StoredProcedure);
        //        ViewBag.Roles = new SelectList(result, "id", "name");
        //    }
        //    return View();
        //}


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(User user)
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters(user);
                var result = db.Query<User>("pUsers;2", p, commandType: CommandType.StoredProcedure);
            }
            return View();
        }
    }
}