using LabASPNET.Models;
using Microsoft.EntityFrameworkCore;

namespace LabASPNET.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options) 
        {
            }

    }
}