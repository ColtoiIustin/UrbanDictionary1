using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UrbanDictionary1.Areas.Identity.Data;
using UrbanDictionary1.Models;
using Microsoft.AspNetCore.Identity;

namespace UrbanDictionary1.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options) 
            : base (options)
        {
            
        }
        public DbSet<Expression> Expressions { get; set; }
        public DbSet<Likes> Likes { get; set; }



    }
}
