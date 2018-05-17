using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHome.Model;

namespace SmartHome.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        private static DbContextOptions<ApplicationDbContext> _connOptions;

        public ApplicationDbContext() : base(_connOptions)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _connOptions = options;
            Database.Migrate();
        }

        #region ContainedMembers

        public DbSet<Design> Designs { get; set; }
        public DbSet<DataSample> DataSamples { get; set; }
        public DbSet<BuildingBlock> BuildingBlocks { get; set; }
        public DbSet<MasterUnit> MasterUnits { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
