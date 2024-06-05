using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(os => os.Id);
            builder.Property(os => os.Id).ValueGeneratedOnAdd();
            builder.Property(os => os.Name).IsRequired().HasMaxLength(150);
            builder.Property(os => os.UnitPrice).IsRequired();
            builder.Property(os => os.Ticker).IsRequired().HasMaxLength(70);
            builder.Property(os => os.UnitPrice).IsRequired().HasPrecision(10,2);
        }
    }
}
