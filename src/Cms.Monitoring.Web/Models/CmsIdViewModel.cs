using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cms.Monitoring.Web.Models
{
    public class CmsIdViewModel
    {
        private readonly DbService _service;
        public IEnumerable<SelectListItem> Items2;

        public CmsIdViewModel() { }

        public CmsIdViewModel(DbService service)
        {
            _service = service;
            Items2 = new SelectList(_service.GetCmsIds());
        }

        // Hold the ID selected in the form
        public string SelectedCmsId { get; set; }
    }
}
