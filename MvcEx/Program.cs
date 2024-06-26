using Microsoft.AspNetCore.Authentication.Cookies;
using MvcEx.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MvcEx
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<UserRepository>(new UserRepository("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Book;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
           .AddCookie(options =>
           {
               options.Cookie.Name = "MyIdentity";
               options.ExpireTimeSpan = TimeSpan.FromHours(8);
               options.SlidingExpiration = true;
               options.Cookie.MaxAge = TimeSpan.FromHours(8);
               options.LoginPath = new PathString("/Account/Login");
           });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
             
                endpoints.MapControllerRoute(
                    name: "account",
                    pattern: "Account/{action=Login}/{id?}",
                    defaults: new { controller = "Account", action = "Login" });


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

         

            app.Run();
        }
    }
}