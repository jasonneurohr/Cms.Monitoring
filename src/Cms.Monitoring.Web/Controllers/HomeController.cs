using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cms.Monitoring.Web.Models;
using Microsoft.Extensions.Options;
using Cms.Monitoring.Web.ViewModels;

namespace Cms.Monitoring.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbService _service;
        private readonly IOptions<AppConfiguration> _config;

        public HomeController(DbService service, IOptions<AppConfiguration> config)
        {
            _service = service;
            _config = config;
        }

        [HttpGet]
        public IActionResult Index()
        {
            GlobalViewModel globalViewModel = new GlobalViewModel()
            {
                AggregateStatisticsViewModel = new AggregateStatisticsViewModel(_service, _config),
                NavViewModel = new NavViewModel(_service)
            };

            return View("AggregateStatistics", globalViewModel);
        }

        [HttpPost]
        public IActionResult Index(CmsIdViewModel cmsIdViewModel)
        {
            GlobalViewModel globalViewModel = new GlobalViewModel()
            {
                StatisticsViewModel = new StatisticsViewModel(_service, cmsIdViewModel.CmsId, _config),
                NavViewModel = new NavViewModel(_service)
            };

            return View(globalViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
