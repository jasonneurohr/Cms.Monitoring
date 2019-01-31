using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cms.Monitoring.Web.ViewModels
{
    public class NavViewModel
    {
        private readonly DbService _service;

        public IEnumerable<SelectListItem> CmsIdList { get; set; }

        public string CmsId { get; set; }

        public NavViewModel(DbService service)
        {
            _service = service;
            CmsIdList = new SelectList(_service.GetCmsIds());
        }
    }
}
