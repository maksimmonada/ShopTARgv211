using Microsoft.EntityFrameworkCore;
using ShopTARgv21.Core.Domain;

namespace ShopTARgv21.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options) { }

        public DbSet<Spaceship> Spaceship { get; set; }
            )

    }
}
