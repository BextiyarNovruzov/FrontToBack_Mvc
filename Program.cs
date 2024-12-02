using FrontToBackMvc.DataAccess;
using FrontToBackMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<UniqloDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MySql")));


            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Lockout.MaxFailedAccessAttempts = 1;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<UniqloDbContext>();

            var app = builder.Build();

            app.MapControllerRoute(
               name: "Register",
                              pattern: "Register",

                defaults:new  {controller= "Account" ,action= "Register"});


            app.MapControllerRoute(
                name: "area",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


            app.MapControllerRoute(name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseStaticFiles();

            app.Run();
        }
    }
}
