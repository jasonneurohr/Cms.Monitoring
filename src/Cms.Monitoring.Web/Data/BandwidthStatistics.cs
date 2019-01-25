namespace Cms.Monitoring.Web.Data
{
    public class BandwidthStatistics
    {
        public int Id { get; set; }
        public string CmsId { get; set; }
        public int Timestamp { get; set; }
        public int AudioBitRateOutgoing { get; set; }
        public int AudioBitRateIncoming { get; set; }
        public int VideoBitRateOutgoing { get; set; }
        public int VideoBitRateIncoming { get; set; }
    }
}
