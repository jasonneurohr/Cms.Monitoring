using Cms.Monitoring.Web.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Cms.Monitoring.Web.ViewModels
{
    public class AggregateStatisticsViewModel
    {
        private readonly DbService _service;
        private readonly IOptions<AppConfiguration> _config;
        public List<AggregateBandwidthStatisticViewModel> AggregateBandwidthStatisticsRecords;
        public List<AggregateCallStatisticsViewModel> AggregateCallStatisticsRecords;
        public List<AggregateMediaLoadStatisticsViewModel> AggregateMediaLoadStatisticsRecords;


        public AggregateStatisticsViewModel(DbService service, IOptions<AppConfiguration> config)
        {
            _service = service;
            _config = config;

            AggregateBandwidthStatisticsRecords = new List<AggregateBandwidthStatisticViewModel>();
            AggregateCallStatisticsRecords = new List<AggregateCallStatisticsViewModel>();
            AggregateMediaLoadStatisticsRecords = new List<AggregateMediaLoadStatisticsViewModel>();

            ICollection<AggregateBandwidthStatisticModel> aggregateBandwidthStatistics;
            ICollection<AggregateCallStatisticsModel> aggregateCallStatistics;
            ICollection<AggregateMediaLoadStatisticsModel> aggregateMediaLoadStatistics;

            if (_config.Value.IgnoreZeroDataPoints)
            {
                aggregateBandwidthStatistics = _service.GetAggregateBandwidthStatistics(true);
                aggregateCallStatistics = _service.GetAggregatedCallStatistics(true);
                aggregateMediaLoadStatistics = _service.GetAggregateMediaLoadStatistics(true);
            }
            else
            {
                aggregateBandwidthStatistics = _service.GetAggregateBandwidthStatistics(false);
                aggregateCallStatistics = _service.GetAggregatedCallStatistics(false);
                aggregateMediaLoadStatistics = _service.GetAggregateMediaLoadStatistics(false);
            }

            foreach (var rec in aggregateBandwidthStatistics)
            {
                AggregateBandwidthStatisticsRecords.Add(new AggregateBandwidthStatisticViewModel()
                {
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(rec.Timestamp).ToLocalTime().ToString(),
                    AudioBitRateIncoming = (int)(rec.AudioBitRateIncoming * 0.001),
                    AudioBitRateOutgoing = (int)(rec.AudioBitRateOutgoing * 0.001),
                    VideoBitRateIncoming = (int)(rec.VideoBitRateIncoming * 0.001),
                    VideoBitRateOutgoing = (int)(rec.VideoBitRateOutgoing * 0.001),
                    TotalIncomingBitrate = (int)((rec.AudioBitRateIncoming + rec.VideoBitRateIncoming) * 0.001),
                    TotalOugoingBitrate = (int)((rec.AudioBitRateOutgoing + rec.VideoBitRateOutgoing) * 0.001)
                });
            }

            foreach (var rec in aggregateCallStatistics)
            {
                AggregateCallStatisticsRecords.Add(new AggregateCallStatisticsViewModel()
                {
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(rec.Timestamp).ToLocalTime().ToString(),
                    CallLegsActive = rec.CallLegsActive,
                    CallLegsCompleted = rec.CallLegsCompleted,
                    CallLegsMaxActive = rec.CallLegsMaxActive
                });
            }

            foreach (var rec in aggregateMediaLoadStatistics)
            {
                AggregateMediaLoadStatisticsRecords.Add(new AggregateMediaLoadStatisticsViewModel()
                {
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(rec.Timestamp).ToLocalTime().ToString(),
                    MediaProcessingLoad = rec.MediaProcessingLoad
                });
            }
        }
    }
}
