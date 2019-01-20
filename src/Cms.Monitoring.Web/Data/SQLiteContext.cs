using Cms.Monitoring.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Cms.Monitoring.Web
{
    public class SQLiteContext : DbContext
    {
        public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) { }

        public DbSet<MediaLoadStatisticModel> MediaLoadStatistics { get; set; }
    }
}
