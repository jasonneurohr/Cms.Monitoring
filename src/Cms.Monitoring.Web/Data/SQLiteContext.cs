using Cms.Monitoring.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Cms.Monitoring.Web
{
    public class SQLiteContext : DbContext
    {
        public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) { }

        public DbSet<MediaLoadStatisticModel> MediaLoadStatistics { get; set; }

        public DbSet<BandwidthStatistics> BandwidthStatistics { get; set; }

        public DbSet<CallStatistics> CallStatistics { get; set; }
    }
}
