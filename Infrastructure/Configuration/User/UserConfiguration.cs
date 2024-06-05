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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(os => os.Id);
            builder.Property(os => os.Id).IsRequired().ValueGeneratedOnAddOrUpdate();

            builder.Property(m => m.NormalizedEmail).HasMaxLength(85);
            builder.Property(m => m.NormalizedUserName).HasMaxLength(85);
        }
    }
}
