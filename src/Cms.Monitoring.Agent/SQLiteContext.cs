using Cms.Monitoring.Agent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Cms.Monitoring.Agent
{
    class SQLiteContext : DbContext
    {
        public DbSet<MediaLoadStatisticModel> MediaLoadStatistics { get; set; }
        public DbSet<BandwidthStatisticsModel> BandwidthStatistics { get; set; }
        public DbSet<CallStatisticsModel> CallStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Uses the location of the currently running assembly as the base path,
            // to resolve the appsettings.json location.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: true);

            IConfigurationRoot configuration = builder.Build();

            // Get the SQLite DB file location
            optionsBuilder.UseSqlite(@"Data Source=" + configuration.GetValue<string>("dbPath"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MediaLoadStatisticModel>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<MediaLoadStatisticModel>()
                .HasIndex(i => i.CmsId);

            modelBuilder.Entity<MediaLoadStatisticModel>()
                .HasIndex(i => i.Timestamp);

            modelBuilder.Entity<BandwidthStatisticsModel>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<BandwidthStatisticsModel>()
                .HasIndex(i => i.CmsId);

            modelBuilder.Entity<BandwidthStatisticsModel>()
                .HasIndex(i => i.Timestamp);

            modelBuilder.Entity<CallStatisticsModel>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<CallStatisticsModel>()
                .HasIndex(i => i.CmsId);

            modelBuilder.Entity<CallStatisticsModel>()
                .HasIndex(i => i.Timestamp);
        }
    }
}
