using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Monitoring.Agent.Models
{
    class BandwidthStatisticsModel
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
