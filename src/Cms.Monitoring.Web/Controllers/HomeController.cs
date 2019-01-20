using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cms.Monitoring.Web.Models;

namespace Cms.Monitoring.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbService _service;

        public HomeController(DbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CmsIdViewModel(_service));
        }

        [HttpPost]
        public IActionResult Index(CmsIdViewModel cmsIdViewModel)
        {
            _service.GetRecords(cmsIdViewModel.SelectedCmsId);
            ViewData["timestamps"] = _service.GetTimeStamps(cmsIdViewModel.SelectedCmsId);
            ViewData["loadpoints"] = _service.GetLoadPoints(cmsIdViewModel.SelectedCmsId);

            return View(new CmsIdViewModel(_service));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
