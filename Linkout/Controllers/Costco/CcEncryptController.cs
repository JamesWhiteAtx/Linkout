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
    public class CcEncryptController : ApiController
    {
        IAppSettingsService appSettings;
        IFileService fileSrvc;
        IGnuPGService gnuPG;

        public CcEncryptController(IAppSettingsService appSettingsService, IFileService fileService, IGnuPGService gnuPGService)
        {
            appSettings = appSettingsService;
            fileSrvc = fileService;
            gnuPG = gnuPGService;
        }

        // GET encrypt/confirm/filename.ext/
        [HttpGet]
        public HttpResponseMessage Confirm(string id)
        {
            FileInfo decryptedFile = fileSrvc.CostcoDecryptFileConfirm(id.Trim(), appSettings);

            FileInfo encryptedFile = null;
            if (decryptedFile.Exists)
            {
                encryptedFile = gnuPG.EncryptCostcoConfirmFile(decryptedFile.FullName, appSettings);
            };

            DecryptFile decryptModel = new DecryptFile(decryptedFile, encryptedFile);

            return Request.CreateResponse(HttpStatusCode.OK, decryptModel);
        }

        // GET encrypt/FA/filename.ext/
        [HttpGet]
        public HttpResponseMessage FA(string id)
        {
            FileInfo decryptedFile = fileSrvc.CostcoDecryptFileFA(id.Trim(), appSettings);

            FileInfo encryptedFile = null;
            if (decryptedFile.Exists)
            {
                encryptedFile = gnuPG.EncryptCostcoFAFile(decryptedFile.FullName, appSettings);
            };

            DecryptFile decryptModel = new DecryptFile(decryptedFile, encryptedFile);

            return Request.CreateResponse(HttpStatusCode.OK, decryptModel);
        }

    }
}
