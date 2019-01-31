﻿namespace Cms.Monitoring.Web.Models
{
    public class BandwidthStatisticsModel
    {
        public string CmsId { get; set; }
        public string Timestamp { get; set; }
        public int AudioBitRateOutgoing { get; set; }
        public int AudioBitRateIncoming { get; set; }
        public int VideoBitRateOutgoing { get; set; }
        public int VideoBitRateIncoming { get; set; }
        public int TotalOugoingBitrate { get; set; }
        public int TotalIncomingBitrate { get; set; }
    }
}
