using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEATHER.API.Data;
using WEATHER.API.Data.Models;

namespace WEATHER.api.Data.EntityTypeConfigurations
{
    public class WeatherEntityTypeConfiguration : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.ToTable(Constants.TableNames.WeatherTableName, Constants.Scheme);
            builder.HasKey(x => x.Id);
            builder
                .Property(x=>x.MinTemperature)
                .HasColumnType("decimal(18,2)");
            builder
                .Property(x=>x.MaxTemperature)
                .HasColumnType("decimal(18,2)");
            //etc.
        }
    }
}