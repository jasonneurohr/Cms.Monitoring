namespace Cms.Monitoring.Web.ViewModels
{
    public class AggregateBandwidthStatisticViewModel
    {
        public string Timestamp { get; set; }
        public int AudioBitRateOutgoing { get; set; }
        public int AudioBitRateIncoming { get; set; }
        public int VideoBitRateOutgoing { get; set; }
        public int VideoBitRateIncoming { get; set; }
        public int TotalOugoingBitrate { get; set; }
        public int TotalIncomingBitrate { get; set; }
    }
}
