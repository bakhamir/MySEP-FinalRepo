using Microsoft.AspNetCore.Authentication.Cookies;
using MyMusic.Abstract;
using MyMusic.Models;
using MyMusic.Service;
namespace MyMusic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddAuthentication
            (CookieAuthenticationDefaults.AuthenticationScheme)
            //AddCookie(o => o.LoginPath = new PathString("/Account/Login"));
            .AddCookie(options =>
            {
            options.Cookie.Name = "MyMusic";
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            options.SlidingExpiration = true;
            options.Cookie.MaxAge = TimeSpan.FromHours(8);
            options.LoginPath = new PathString("/Account/Login");
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IMusic<song>, MusService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

 
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Mus}/{action=Index}/{id?}");

            app.Run();
        }
    }
}