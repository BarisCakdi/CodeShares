using CodeShares.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeShares.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Code> Codes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) :  base(options) 
        {

        }
    }
}
