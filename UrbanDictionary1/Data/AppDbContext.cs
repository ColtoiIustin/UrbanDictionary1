using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UrbanDictionary1.Models;

namespace UrbanDictionary1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }
        public DbSet<Expression> Expressions { get; set; }
    }
}
