using Dapper;
using LoginLogOffHw.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

public class AccountController : Controller
{
    private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=testdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
     
    [HttpPost]
    public ActionResult Login(string username, string password)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var user = connection.QueryFirstOrDefault<_User>(
                "check_user",
                new { Username = username, Password = password },
                commandType: System.Data.CommandType.StoredProcedure
            );

            if (user != null)
            { 
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                ViewBag.ErrorMessage = "Неправильное имя пользователя или пароль";
                return View();
            }
        }
    }
     
    [HttpPost]
    public ActionResult Register(string username, string password)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            connection.Execute(
                "create_user",
                new { Username = username, Password = password },
                commandType: System.Data.CommandType.StoredProcedure
            );
             
            return RedirectToAction("Login");  
        }
    }
    [HttpPost]
    public ActionResult LogOff()
    {
        // Выполните необходимые действия для выхода пользователя (например, удалите куки аутентификации)
        return RedirectToAction("Login"); // Перенаправление на страницу входа после выхода
    }
}

//create table _user (
//    id int primary key identity,
//    username nvarchar(50) unique not null,
//    password_hash nvarchar(200) not null
//);
//create procedure check_user
//    @username nvarchar(50),
//    @password nvarchar(200)
//as
//begin
//    if exists(select 1 from _user where username = @username and pwdcompare(@password, password_hash) = 1)
//        select 1 as authenticated;
//    else
//    select 0 as authenticated;
//end;

//create procedure create_user
//    @username nvarchar(50),
//    @password nvarchar(200)
//as
//begin
//    insert into _user (username, password_hash)
//    values(@username, pwdencrypt(@password));
//end;
