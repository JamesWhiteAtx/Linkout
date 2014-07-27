using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ComHub;

namespace Linkout.Controllers.Costco
{
    public class CcUploadController : ApiController
    {
        IAppSettingsService appSettings;
        ICHFtpService chFtp;
        IGnuPGService gnuPG;

        public CcUploadController(IAppSettingsService appSettingsService, ICHFtpService chFtpService, IGnuPGService gnuPGService)
        {
            appSettings = appSettingsService;
            chFtp = chFtpService;
            gnuPG = gnuPGService;
        }

        // PUT upload/confirm/file.ext/
        [HttpPut]
        public HttpResponseMessage Confirm(string id)
        {
            if (chFtp.UploadCostcoConfirm(id.Trim(), appSettings))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "File "+id+" was uploaded.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Failed to upload file " + id +".");
            }
        }

        // PUT upload/FA/file.ext/
        [HttpPut]
        public HttpResponseMessage FA(string id)
        {
            if (chFtp.UploadCostcoFA(id.Trim(), appSettings))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "File " + id + " was uploaded.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Failed to upload file " + id + ".");
            }
        }
    }
}
