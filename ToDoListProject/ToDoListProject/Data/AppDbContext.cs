using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDoListProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoListProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}