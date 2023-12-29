
using firstAppAsp.Models;

namespace firstAppAsp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

      public DbSet<Game> Games { get; set; }
      public DbSet<Category> Category { get; set; }  
      public DbSet<Device> Devices { get; set; }
      public DbSet<GameDevice> GameDevices { get; set; }    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category{Id=1,Name="Sports"},
                    new Category{Id=2,Name="Actions"},
                    new Category{Id=3,Name="Racing"},
                    new Category{Id=4,Name="Fighting"},
                    new Category{Id=5,Name="Climbing"},
                });
            modelBuilder.Entity<Device>()
                .HasData(new Device[]
                {
                    new Device { Id=1,Name="Playstation",Icon="gh"},
                    new Device { Id=2,Name="X-Box",Icon="fh"},
                    new Device { Id=3,Name="Pc",Icon="jh"},
                });
            modelBuilder.Entity<GameDevice>().HasKey(e=> new {e.DeviceId,e.GameId});
            base.OnModelCreating(modelBuilder);
        }
    }
}
