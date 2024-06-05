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
    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.HasKey(os => os.Id);
            builder.Property(os => os.Id).ValueGeneratedOnAddOrUpdate();
            
            
            builder
               .HasOne(x => x.Role)
               .WithMany(x => x.Claims)
               .HasForeignKey(x => x.RoleId)
               .OnDelete(DeleteBehavior.Cascade) ;
        }
    }
}
