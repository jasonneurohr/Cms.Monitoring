using Cms.Monitoring.Agent.Models;

namespace Cms.Monitoring.Agent
{
    class SQLiteServices : ISQLiteServices
    {
        private readonly SQLiteContext _context = new SQLiteContext();

        public int Add(MediaLoadStatisticModel m)
        {
            _context.Add(m);
            return _context.SaveChanges();
        }

        public int Add(BandwidthStatisticsModel b)
        {
            _context.Add(b);
            return _context.SaveChanges();
        }

        public int Add(CallStatisticsModel c)
        {
            _context.Add(c);
            return _context.SaveChanges();
        }
    }
}
