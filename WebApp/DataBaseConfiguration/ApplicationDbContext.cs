using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.DataBaseConfiguration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Miniature> Miniature { get; set; }
    }
}
