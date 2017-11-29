using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHomeWebApp.Models;
using SmartHome_WebApp.Models;

namespace SmartHomeWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext():base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        #region ContainedMembers

        public DbSet<Design> Designs { get; set; }
        public DbSet<DataSample> DataSamples { get; set; }
        public DbSet<BuildingBlock> BuildingBlocks { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Design>().ToTable($"{nameof(Design)}");
            builder.Entity<DataSample>().ToTable($"{nameof(DataSample)}");
            builder.Entity<BuildingBlock>().ToTable($"{nameof(BuildingBlock)}");
        }

    }
}
