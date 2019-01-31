using Cms.Monitoring.Web.Data;
using Cms.Monitoring.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Cms.Monitoring.Web.ViewModels
{
    public class StatisticsViewModel
    {
        private readonly DbService _service;
        private readonly IOptions<AppConfiguration> _config;
        public List<BandwidthStatisticsModel> BandwidthStatisticsRecords;
        public List<CallStatisticsModel> CallStatisticsRecords;
        public List<MediaLoadStatisticsModel> MediaLoadStatisticsRecords;

        public StatisticsViewModel(DbService service, string cmsId, IOptions<AppConfiguration> config)
        {
            _service = service;
            _config = config;

            BandwidthStatisticsRecords = new List<BandwidthStatisticsModel>();
            CallStatisticsRecords = new List<CallStatisticsModel>();
            MediaLoadStatisticsRecords = new List<MediaLoadStatisticsModel>();

            ICollection<BandwidthStatistics> bandwidthStatistics;
            ICollection<CallStatistics> callStatistics;
            ICollection<MediaLoadStatisticModel> mediaLoadStatistics;

            if (_config.Value.IgnoreZeroDataPoints)
            {
                bandwidthStatistics = _service.GetBandwidthStatistics(cmsId, true);
                callStatistics = _service.GetCallStatistics(cmsId, true);
                mediaLoadStatistics = _service.GetMediaLoadStatistics(cmsId, true);
            }
            else
            {
                bandwidthStatistics = _service.GetBandwidthStatistics(cmsId, false);
                callStatistics = _service.GetCallStatistics(cmsId, false);
                mediaLoadStatistics = _service.GetMediaLoadStatistics(cmsId, false);
            }

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
