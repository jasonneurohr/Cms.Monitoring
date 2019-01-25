namespace Cms.Monitoring.Web.Data
{
    public class CallStatistics
    {
        public int Id { get; set; }
        public string CmsId { get; set; }
        public int Timestamp { get; set; }
        public int CallLegsActive { get; set; }
        public int CallLegsMaxActive { get; set; }
        public int CallLegsCompleted { get; set; }
    }
}
