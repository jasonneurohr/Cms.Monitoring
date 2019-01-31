namespace Cms.Monitoring.Web.Models
{
    public class AggregateCallStatisticsModel
    {
        public int Timestamp { get; set; }
        public int CallLegsActive { get; set; }
        public int CallLegsMaxActive { get; set; }
        public int CallLegsCompleted { get; set; }
    }
}
