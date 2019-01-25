using Cms.Monitoring.Agent.Models;

namespace Cms.Monitoring.Agent
{
    interface ISQLiteServices
    {
        int Add(MediaLoadStatisticModel m);
        int Add(BandwidthStatisticsModel b);
        int Add(CallStatisticsModel c);
    }
}
