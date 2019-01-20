using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Cms.Lib;
using Cms.Monitoring.Agent.Models;
using Microsoft.Extensions.Configuration;

namespace Cms.Monitoring.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            ISQLiteServices sqliteServices = new SQLiteServices();

            // Use the location of the currently running assembly as the base path,
            // to resolve the appsettings.json location.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: true);

            IConfigurationRoot configuration = builder.Build();

            // Get the log file path from the appsetttings.json
            string logFile = configuration.GetValue<string>("logFilePath");

            // Get a list of CMS server entries as defined in the appsettings.json
            List<CmsModel> cmsList = configuration.GetSection("cmsServers").Get<List<CmsModel>>();

            // Foreach CMS in the list, get the mediaProcessingLoad value and write it to SQLight and text file
            foreach (CmsModel cms in cmsList)
            {
                ICmsServer cmsServer = new CmsServer(cms.Address, cms.Port, cms.Username, cms.Password);
                var load = cmsServer.MediaLoad.Get().Result;

                sqliteServices.Add(new MediaLoadStatisticModel
                {
                    CmsId = cms.Address,
                    Timestamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds(),
                    MediaProcessingLoad = load
                });
            }
        }
    }
}
