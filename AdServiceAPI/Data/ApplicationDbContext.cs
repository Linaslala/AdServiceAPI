using AdServiceAPI.Ads;
using Microsoft.EntityFrameworkCore;

namespace AdServiceAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Blir en tabell
        public DbSet<Ad> Ads { get; set; }

    }
}
