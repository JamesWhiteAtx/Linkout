using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers.Costco
{
    public class CcContentController : ApiController
    {
        private IContentService _contentService;

        public CcContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        // GET api/Content/
        [HttpGet]
        public IEnumerable<ColorModel> Colors()
        {
            return _contentService.ColorModels();
        }
    }
}
