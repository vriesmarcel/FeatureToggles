using System.Data.Entity;

namespace MvcMusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {
        public MusicStoreEntities()
        {
            var connectionstringFromEnvironment = System.Environment.GetEnvironmentVariable(this.GetType().Name);
            if (!string.IsNullOrEmpty(connectionstringFromEnvironment))
            {
                this.Database.Connection.ConnectionString = connectionstringFromEnvironment;
            }
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<FeatureToggle> FeatureToggles { get; set; }
    }
}