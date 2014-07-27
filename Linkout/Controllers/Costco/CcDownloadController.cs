using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using ComHub;

namespace Linkout.Controllers.Costco
{
    public class CcDownloadController : ApiController
    {
        IAppSettingsService appSettings;
        ICHFtpService chFtp;
        IGnuPGService gnuPG;

        public CcDownloadController(IAppSettingsService appSettingsService, ICHFtpService chFtpService, IGnuPGService gnuPGService)
        {
            appSettings = appSettingsService;
            chFtp = chFtpService;
            gnuPG = gnuPGService;
        }

        // GET download/orders
        [HttpGet]
        public HttpResponseMessage Orders()
        {
            var orders = chFtp.GetCostcoOrders();
            return Request.CreateResponse(HttpStatusCode.OK, orders);
            
            //return Request.CreateResponse(HttpStatusCode.OK, new string[] {"11","22"});
        }

        // GET download/orders/filename.ext/
        [HttpGet]
        public HttpResponseMessage Orders(string id)
        {
            FileInfo encryptedFile = chFtp.DownloadCostoOrder(id, appSettings);
            FileInfo decryptedFile = null;
            if (encryptedFile.Exists)
            {
                decryptedFile = gnuPG.DecryptCostcoOrderFile(encryptedFile.FullName, appSettings);
            };

            DecryptFile decryptModel = new DecryptFile(decryptedFile, encryptedFile);

            return Request.CreateResponse(HttpStatusCode.OK, decryptModel);
            
            //return Request.CreateResponse(HttpStatusCode.OK, new { order = "123" });
        }

    }
}
