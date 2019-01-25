using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Monitoring.Agent.Models
{
    class CallStatisticsModel
    {
        public int Id { get; set; }
        public string CmsId { get; set; }
        public int Timestamp { get; set; }
        public int CallLegsActive { get; set; }
        public int CallLegsMaxActive { get; set; }
        public int CallLegsCompleted { get; set; }
    }
}
