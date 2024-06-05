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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
        
            builder.HasKey(os => os.Id);
            builder.Property(os => os.Id).ValueGeneratedOnAdd();
            builder.Property(os => os.AccountId).IsRequired();
            builder.Property(os => os.AssetName).IsRequired().HasMaxLength(32);
            builder.Property(os => os.Price).IsRequired().HasPrecision(10, 2);
            builder.Property(os => os.Operation).IsRequired();
            builder.Property(os => os.TotalAmount).HasPrecision(10, 2);

        }
    }
}
