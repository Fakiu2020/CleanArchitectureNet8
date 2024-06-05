using Domain.Entities;
using Infrastructure.Configuration;
using Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;


namespace Infrastructure.Persistence
{
    public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
            #region Seeds
            builder.Entity<Asset>().HasData(AssetSeed.GetAssets());
            #endregion

        }


        public DbSet<Asset> Asset { get; set; }
        public DbSet<Order> Orders { get; set; }

        #region User
        public DbSet<User> Users { get; set; }
        #endregion

    

    }
}
