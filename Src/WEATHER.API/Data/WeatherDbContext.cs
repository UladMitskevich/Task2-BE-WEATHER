using Microsoft.EntityFrameworkCore;
using WEATHER.api.Data.EntityTypeConfigurations;
using WEATHER.API.Data.Models;


//Should be in separate assembly for real projects to follow DDD
namespace WEATHER.API.Data
{
    public class WeatherDbContext : DbContext
    {
        public DbSet<Weather> Weather { get; set; }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherEntityTypeConfiguration());
        }
    }
}
