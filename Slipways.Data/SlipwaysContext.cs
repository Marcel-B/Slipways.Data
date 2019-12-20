using com.b_velop.Slipways.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace com.b_velop.Slipways.Data
{
    public class SlipwaysContext : DbContext
    {
        public DbSet<Water> Waters { get; set; }
        public DbSet<Slipway> Slipways { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ManufacturerService> ManufacturerServices { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<SlipwayExtra> SlipwayExtras { get; set; }

        private readonly ILogger<SlipwaysContext> _logger;

        public SlipwaysContext(
            DbContextOptions options,
            ILogger<SlipwaysContext> logger) : base(options)
        {
            _logger = logger;
        }

        protected async override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));
            //await modelBuilder.Seed();
        }
    }
}
