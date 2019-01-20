using System.ComponentModel.DataAnnotations;

namespace Cms.Monitoring.Agent.Models
{
    class MediaLoadStatisticModel
    {
        [Key]
        public int Id { get; set; }

        public string CmsId { get; set; }

        public int Timestamp { get; set; }

        public int MediaProcessingLoad { get; set; }
    }
}
