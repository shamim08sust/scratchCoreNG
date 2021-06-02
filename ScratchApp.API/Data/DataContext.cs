using Microsoft.EntityFrameworkCore;
using ScratchApp.API.Models;

namespace ScratchApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Values> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
}