using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Cms.Monitoring.Web.Models
{
    public class StatisticsViewModel
    {
        private readonly DbService _service;
        public IEnumerable<SelectListItem> selectListItems;
        public List<BandwidthStatisticsModel> BandwidthStatisticsRecords;
        //public BandwidthStatisticsModel BandwidthStatistics;
        public List<CallStatisticsModel> CallStatisticsRecords;
        public List<MediaLoadStatisticsModel> MediaLoadStatisticsRecords;

        // Hold the ID selected in the form
        public string SelectedCmsId { get; set; }

        public StatisticsViewModel(DbService service)
        {
            _service = service;
            selectListItems = new SelectList(_service.GetCmsIds());
            BandwidthStatisticsRecords = new List<BandwidthStatisticsModel>();
            CallStatisticsRecords = new List<CallStatisticsModel>();
            MediaLoadStatisticsRecords = new List<MediaLoadStatisticsModel>();

            
            BandwidthStatisticsRecords.Add(new BandwidthStatisticsModel
            {
                CmsId = "0",
                Timestamp = "0",
                AudioBitRateIncoming = 0,
                AudioBitRateOutgoing = 0,
                VideoBitRateIncoming = 0,
                VideoBitRateOutgoing = 0,
                TotalIncomingBitrate = 0,
                TotalOugoingBitrate = 0
            });

            CallStatisticsRecords.Add(new CallStatisticsModel
            {
                CmsId = "0",
                Timestamp = "0",
                CallLegsActive = 0,
                CallLegsCompleted = 0,
                CallLegsMaxActive = 0
            });
            
            MediaLoadStatisticsRecords.Add(new MediaLoadStatisticsModel
            {
                CmsId = "0",
                Timestamp = "0",
                MediaProcessingLoad = 0
            });
        }

        public StatisticsViewModel(DbService service, string cmsId)
        {
            _service = service;
            selectListItems = new SelectList(_service.GetCmsIds());
            BandwidthStatisticsRecords = new List<BandwidthStatisticsModel>();
            CallStatisticsRecords = new List<CallStatisticsModel>();
            MediaLoadStatisticsRecords = new List<MediaLoadStatisticsModel>();
            
            var bandwidthStatistics = _service.GetBandwidthStatistics(cmsId);
            var callStatistics = _service.GetCallStatistics(cmsId);
            var mediaLoadStatistics = _service.GetMediaLoadStatistics(cmsId);

            foreach (var rec in bandwidthStatistics)
            {
                BandwidthStatisticsRecords.Add(new BandwidthStatisticsModel
                {
                    CmsId = rec.CmsId,
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(rec.Timestamp).ToLocalTime().ToString(),
                    AudioBitRateIncoming = (int)(rec.AudioBitRateIncoming * 0.001),
                    AudioBitRateOutgoing = (int)(rec.AudioBitRateOutgoing * 0.001),
                    VideoBitRateIncoming = (int)(rec.VideoBitRateIncoming * 0.001),
                    VideoBitRateOutgoing = (int)(rec.VideoBitRateOutgoing * 0.001),
                    TotalIncomingBitrate = (int)((rec.AudioBitRateIncoming + rec.VideoBitRateIncoming) * 0.001),
                    TotalOugoingBitrate = (int)((rec.AudioBitRateOutgoing + rec.VideoBitRateOutgoing) * 0.001)
                });
            }

            foreach (var rec in callStatistics)
            {
                CallStatisticsRecords.Add(new CallStatisticsModel
                {
                    CmsId = rec.CmsId,
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(rec.Timestamp).ToLocalTime().ToString(),
                    CallLegsActive = rec.CallLegsActive,
                    CallLegsCompleted = rec.CallLegsCompleted,
                    CallLegsMaxActive = rec.CallLegsMaxActive
                });
            }

            foreach (var rec in mediaLoadStatistics)
            {
                MediaLoadStatisticsRecords.Add(new MediaLoadStatisticsModel
                {
                    CmsId = rec.CmsId,
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(rec.Timestamp).ToLocalTime().ToString(),
                    MediaProcessingLoad = rec.MediaProcessingLoad
                });
            }
        }
    }
}
