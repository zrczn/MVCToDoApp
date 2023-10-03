using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository;
using ToDo.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ToDo.Utility;
using ToDo.Areas.User.Controllers;
using ToDo.Filters;

namespace ToDo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<UserIdAsyncFilter>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbCon>(opt => opt.UseSqlServer(
                builder.Configuration.GetConnectionString("DbConn")
                ));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbCon>()
                .AddDefaultTokenProviders();
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<BaseController>();
            builder.Services.AddScoped<UserIdAsyncFilter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=User}/{controller=ToDo}/{action=Index}/{id?}");

            app.Run();
        }
    }
}