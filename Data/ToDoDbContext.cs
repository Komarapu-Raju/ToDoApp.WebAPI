using Microsoft.EntityFrameworkCore;
using ToDoApp.WebAPI.Models;

namespace ToDoApp.WebAPI.Data
{
    public class ToDoDbContext: DbContext
    {
        public ToDoDbContext(DbContextOptions options): base(options) { }

        public DbSet<ToDo> ToDos { get; set; }
    }
}