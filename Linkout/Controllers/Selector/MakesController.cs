﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using Linkout;

namespace Linkout.Controllers
{
    public class MakesController : SelectorController
    {
        public HttpResponseMessage Get()
        {
            AddQuery("type", "makes");
            return JsonResponse();
        }
    }
}
