namespace Cms.Monitoring.Web.ViewModels
{
    public class AggregateCallStatisticsViewModel
    {
        public string Timestamp { get; set; }
        public int CallLegsActive { get; set; }
        public int CallLegsMaxActive { get; set; }
        public int CallLegsCompleted { get; set; }
    }
}
