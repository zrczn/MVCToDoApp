using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Reflection.Emit;
using ToDo.Models;

namespace ToDo.ApplicationDBContext
{
    public class AppDbCon : IdentityDbContext<IdentityUser>
    {
        public AppDbCon(DbContextOptions<AppDbCon> options) : base(options)
        {
            
        }

        public DbSet<ToDoModel> toDoModels { get; set; }
        public DbSet<PhotoModel> photoModels { get; set; }
        public DbSet<AudioModel> audioModels { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
