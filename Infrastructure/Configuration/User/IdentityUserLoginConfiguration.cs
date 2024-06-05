using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {

            builder.Property(m => m.LoginProvider).IsRequired().HasMaxLength(85);
            builder.Property(m => m.ProviderKey).IsRequired().HasMaxLength(85);
            builder.Property(m => m.UserId).IsRequired().HasMaxLength(85);

        }
    }
}
