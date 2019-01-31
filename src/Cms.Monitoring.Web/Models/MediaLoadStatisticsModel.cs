namespace Cms.Monitoring.Web.Models
{
    public class MediaLoadStatisticsModel
    {
        public string CmsId { get; set; }
        public string Timestamp { get; set; }
        public int MediaProcessingLoad { get; set; }
    }
}
