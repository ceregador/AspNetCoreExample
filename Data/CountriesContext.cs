using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CountriesDbContext : DbContext
    {
        public CountriesDbContext (DbContextOptions<CountriesDbContext> options) : base (options)
        {
        }

        public DbSet<Country> Countries { get; set; }
    }
}