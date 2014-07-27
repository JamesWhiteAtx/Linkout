using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ComHub;
using System.IO;

namespace Linkout.Controllers.Costco
{
    public class CcDecryptController : ApiController
    {
        IAppSettingsService appSettings;
        IFileService fileSrvc;
        IGnuPGService gnuPG;

        public CcDecryptController(IAppSettingsService appSettingsService, IFileService fileService, IGnuPGService gnuPGService)
        {
            appSettings = appSettingsService;
            fileSrvc = fileService;
            gnuPG = gnuPGService;
        }

        // GET decrypt/order/filename.ext/
        [HttpGet]
        public HttpResponseMessage Order(string id)
        {
            FileInfo encryptedFile = fileSrvc.CostcoEncryptFileOrder(id.Trim(), appSettings);
            
            FileInfo decryptedFile = null;
            if (encryptedFile.Exists)
            {
                decryptedFile = gnuPG.DecryptCostcoOrderFile(encryptedFile.FullName, appSettings);
            };

            DecryptFile decryptModel = new DecryptFile(decryptedFile, encryptedFile);

            return Request.CreateResponse(HttpStatusCode.OK, decryptModel);
        }
    }
}
