using Cms.Monitoring.Web.Data;
using Cms.Monitoring.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cms.Monitoring.Web
{
    public class DbService
    {
        readonly SQLiteContext _context;

        public DbService(SQLiteContext context)
        {
            _context = context;
        }

        public ICollection<string> GetCmsIds()
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();
            return _context.MediaLoadStatistics
                .Where(i => i.Timestamp >= lastSevenDays)
                .Select(c => c.CmsId)
                .Distinct()
                .ToList();
        }

        #region Bandwidth Statistics Methods

        public ICollection<BandwidthStatistics> GetBandwidthStatistics()
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();
            return _context.BandwidthStatistics
                .Where(i => i.Timestamp >= lastSevenDays)
                .Distinct()
                .ToList();
        }

        public ICollection<BandwidthStatistics> GetBandwidthStatistics(string cmsId)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();
            return _context.BandwidthStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.CmsId == cmsId)
                .Distinct()
                .ToList();
        }

        public ICollection<BandwidthStatistics> GetBandwidthStatistics(string cmsId, bool includeZeroDataPoints)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            if (includeZeroDataPoints)
            {
                GetBandwidthStatistics(cmsId);
            }

            return _context.BandwidthStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.CmsId == cmsId && i.AudioBitRateIncoming != 0 && i.AudioBitRateOutgoing !=0 && i.VideoBitRateIncoming != 0 && i.VideoBitRateOutgoing != 0)
                .Distinct()
                .ToList();
        }

        public ICollection<AggregateBandwidthStatisticModel> GetAggregateBandwidthStatistics()
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            return _context.BandwidthStatistics
                .Where(i => i.Timestamp >= lastSevenDays)
                .GroupBy(i => i.Timestamp)
                .Select(g => new AggregateBandwidthStatisticModel()
                {
                    AudioBitRateIncoming = g.Sum(i => i.AudioBitRateIncoming),
                    AudioBitRateOutgoing = g.Sum(i => i.AudioBitRateOutgoing),
                    VideoBitRateIncoming = g.Sum(i => i.VideoBitRateIncoming),
                    VideoBitRateOutgoing = g.Sum(i => i.VideoBitRateOutgoing),
                    TotalIncomingBitrate = g.Sum(i => i.AudioBitRateIncoming) + g.Sum(i => i.VideoBitRateIncoming),
                    TotalOugoingBitrate = g.Sum(i => i.AudioBitRateOutgoing) + g.Sum(i => i.VideoBitRateOutgoing),
                    Timestamp = g.Select(z => z.Timestamp).First()
                }).ToList();
        }

        public ICollection<AggregateBandwidthStatisticModel> GetAggregateBandwidthStatistics(bool includeZeroDataPoints)
        {
            if (includeZeroDataPoints)
            {
                GetAggregateBandwidthStatistics();
            }

            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            return _context.BandwidthStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.AudioBitRateIncoming != 0 && i.AudioBitRateOutgoing != 0 && i.VideoBitRateIncoming != 0 && i.VideoBitRateOutgoing != 0)
                .GroupBy(i => i.Timestamp)
                .Select(g => new AggregateBandwidthStatisticModel()
                {
                    AudioBitRateIncoming = g.Sum(i => i.AudioBitRateIncoming),
                    AudioBitRateOutgoing = g.Sum(i => i.AudioBitRateOutgoing),
                    VideoBitRateIncoming = g.Sum(i => i.VideoBitRateIncoming),
                    VideoBitRateOutgoing = g.Sum(i => i.VideoBitRateOutgoing),
                    TotalIncomingBitrate = g.Sum(i => i.AudioBitRateIncoming) + g.Sum(i => i.VideoBitRateIncoming),
                    TotalOugoingBitrate = g.Sum(i => i.AudioBitRateOutgoing) + g.Sum(i => i.VideoBitRateOutgoing),
                    Timestamp = g.Select(z => z.Timestamp).First()
                }).ToList();
        }

        #endregion

        #region Call Statistics Methods

        public ICollection<CallStatistics> GetCallStatistics()
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();
            return _context.CallStatistics
                .Where(i => i.Timestamp >= lastSevenDays)
                .Distinct()
                .ToList();
        }

        public ICollection<CallStatistics> GetCallStatistics(string cmsId)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();
            return _context.CallStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.CmsId == cmsId)
                .Distinct()
                .ToList();
        }

        public ICollection<CallStatistics> GetCallStatistics(string cmsId, bool includeZeroDataPoints)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            if (includeZeroDataPoints)
            {
                GetCallStatistics(cmsId);
            }

            return _context.CallStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.CmsId == cmsId && i.CallLegsActive != 0)
                .Distinct()
                .ToList();
        }

        public ICollection<AggregateCallStatisticsModel> GetAggregatedCallStatistics()
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            return _context.CallStatistics
                .Where(i => i.Timestamp >= lastSevenDays)
                .GroupBy(i => i.Timestamp)
                .Select(g => new AggregateCallStatisticsModel()
                {
                    CallLegsActive = g.Sum(i => i.CallLegsActive),
                    CallLegsCompleted = g.Sum(i => i.CallLegsCompleted),
                    CallLegsMaxActive = g.Sum(i => i.CallLegsMaxActive),
                    Timestamp = g.Select(i => i.Timestamp).First()
                }).ToList();
        }

        public ICollection<AggregateCallStatisticsModel> GetAggregatedCallStatistics(bool includeZeroDataPoints)
        {
            if (includeZeroDataPoints)
            {
                GetAggregatedCallStatistics();
            }
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            return _context.CallStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.CallLegsActive != 0)
                .GroupBy(i => i.Timestamp)
                .Select(g => new AggregateCallStatisticsModel() {
                    CallLegsActive = g.Sum(i => i.CallLegsActive),
                    CallLegsCompleted = g.Sum(i => i.CallLegsCompleted),
                    CallLegsMaxActive = g.Sum(i => i.CallLegsMaxActive),
                    Timestamp = g.Select(i => i.Timestamp).First()
                }).ToList();
        }

        #endregion

        #region Media Load Statistics Methods

        public ICollection<MediaLoadStatisticModel> GetMediaLoadStatistics()
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();
            return _context.MediaLoadStatistics
                .Where(i => i.Timestamp >= lastSevenDays)
                .Distinct()
                .ToList();
        }

        public ICollection<MediaLoadStatisticModel> GetMediaLoadStatistics(string cmsId)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();
            return _context.MediaLoadStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.CmsId == cmsId)
                .Distinct()
                .ToList();
        }

        public ICollection<MediaLoadStatisticModel> GetMediaLoadStatistics(string cmsId, bool includeZeroDataPoints)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            if (includeZeroDataPoints)
            {
                GetMediaLoadStatistics(cmsId);
            }

            return _context.MediaLoadStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.CmsId == cmsId && i.MediaProcessingLoad != 0)
                .Distinct()
                .ToList();
        }

        public ICollection<AggregateMediaLoadStatisticsModel> GetAggregateMediaLoadStatistics()
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            return _context.MediaLoadStatistics
                .Where(i => i.Timestamp >= lastSevenDays)
                .GroupBy(i => i.Timestamp)
                .Select(g => new AggregateMediaLoadStatisticsModel()
                {
                    MediaProcessingLoad = g.Sum(i => i.MediaProcessingLoad),
                    Timestamp = g.Select(i => i.Timestamp).First()
                }).ToList();
        }

        public ICollection<AggregateMediaLoadStatisticsModel> GetAggregateMediaLoadStatistics(bool includeZeroDataPoints)
        {
            if (includeZeroDataPoints)
            {
                GetAggregateMediaLoadStatistics();
            }

            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            return _context.MediaLoadStatistics
                .Where(i => i.Timestamp >= lastSevenDays && i.MediaProcessingLoad != 0)
                .GroupBy(i => i.Timestamp)
                .Select(g => new AggregateMediaLoadStatisticsModel()
                {
                    MediaProcessingLoad = g.Sum(i => i.MediaProcessingLoad),
                    Timestamp = g.Select(i => i.Timestamp).First()
                }).ToList();
        }

        #endregion
    }
}
