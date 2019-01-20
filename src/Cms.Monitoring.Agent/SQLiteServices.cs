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
    }
}
