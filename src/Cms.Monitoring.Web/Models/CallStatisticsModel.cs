using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Monitoring.Web.Models
{
    public class CallStatisticsModel
    {
        public string CmsId { get; set; }
        public string Timestamp { get; set; }
        public int CallLegsActive { get; set; }
        public int CallLegsMaxActive { get; set; }
        public int CallLegsCompleted { get; set; }
    }
}
