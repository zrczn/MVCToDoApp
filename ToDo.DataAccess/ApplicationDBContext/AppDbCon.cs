using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ToDo.Models;

namespace ToDo.ApplicationDBContext
{
    public class AppDbCon : DbContext
    {
        public AppDbCon(DbContextOptions<AppDbCon> options) : base(options)
        {
            
        }

        public DbSet<ToDoModel> toDoModels { get; set; }
        public DbSet<PhotoModel> photoModels { get; set; }
        public DbSet<AudioModel> audioModels { get; set; }

    }
}
