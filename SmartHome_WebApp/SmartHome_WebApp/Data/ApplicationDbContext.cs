using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHomeWebApp.Models;
using SmartHome_WebApp.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace SmartHomeWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
