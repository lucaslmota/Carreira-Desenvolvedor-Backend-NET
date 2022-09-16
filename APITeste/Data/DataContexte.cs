using APITeste.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeste.Data
{
    public class DataContexte : DbContext
    {
        public DataContexte(DbContextOptions<DataContexte> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}