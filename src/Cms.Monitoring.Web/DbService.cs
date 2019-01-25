using Cms.Monitoring.Web.Data;
using Newtonsoft.Json;
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

        public ICollection<MediaLoadStatisticModel> GetRecords(string key)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();
            var x =_context.MediaLoadStatistics
                .Where(i => i.CmsId.Contains(key) && i.Timestamp >= lastSevenDays).ToList();


            return _context.MediaLoadStatistics.ToList();
        }

        public string GetTimeStamps(string key)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            List<int> x = _context.MediaLoadStatistics
                .Where(i => i.CmsId.Contains(key) && i.Timestamp >= lastSevenDays)
                .Select(c => c.Timestamp)
                .ToList();

            List<string> convertedTimestamps = new List<string>();

            foreach (var cmsTimestamp in x)
            {
                // UTC Timestamp
                //convertedTimestamps.Add(DateTimeOffset.FromUnixTimeSeconds(cmsTimestamp).DateTime.ToString());

                // Local Timestamp
                convertedTimestamps.Add(DateTimeOffset.FromUnixTimeSeconds(cmsTimestamp).ToLocalTime().ToString());
            }

            return JsonConvert.SerializeObject(convertedTimestamps).ToString();
        }

        public string GetLoadPoints(string key)
        {
            int lastSevenDays = (int)DateTimeOffset.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToUnixTimeSeconds();

            return JsonConvert.SerializeObject(
                _context.MediaLoadStatistics
                .Where(i => i.CmsId.Contains(key) && i.Timestamp >= lastSevenDays)
                .Select(c => c.MediaProcessingLoad)
                .ToList())
                    .ToString();
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
    }
}
