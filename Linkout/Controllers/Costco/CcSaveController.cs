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
    public class CcSaveController : ApiController
    {
        IAppSettingsService appSettings; 
        IFileService fileSrvc;
        IGnuPGService gnuPG;

        public CcSaveController(IAppSettingsService appSettingsService, IFileService fileService, IGnuPGService gnuPGService)
        {
            appSettings = appSettingsService;
            fileSrvc = fileService;
            gnuPG = gnuPGService;
        }

        // POST save/Confirm
        [HttpPost]
        public HttpResponseMessage Confirm(WebApiJson model)  //[FromBody]string value)
        {
            ConfirmMessageBatch confirmBatch = HubConfBatch.ConfBatchFromJson(model.Json);

            FileInfo savedFile = fileSrvc.SaveCostcoConfirm(confirmBatch, appSettings);

            FileInfo encryptedFile = null;
            if (savedFile.Exists)
            {
                encryptedFile = gnuPG.EncryptCostcoConfirmFile(savedFile.FullName, appSettings);
            };

            confirmBatch = fileSrvc.CostcoMessageBatchConfirm(savedFile.Name, appSettings);

            HubConfBatch batch = new HubConfBatch(savedFile.Name, confirmBatch);

            return Request.CreateResponse(HttpStatusCode.OK, batch);
        }

        // POST save/FA
        [HttpPost]
        public HttpResponseMessage FA(WebApiJson model)  //[FromBody]string value)
        {
            FAMessageBatch faBatch = HubFABatch.FABatchFromJson(model.Json);

            FileInfo savedFile = fileSrvc.SaveCostcoFA(faBatch, appSettings);

            FileInfo encryptedFile = null;
            if (savedFile.Exists)
            {
                encryptedFile = gnuPG.EncryptCostcoConfirmFile(savedFile.FullName, appSettings);
            };

            faBatch = fileSrvc.CostcoMessageBatchFA(savedFile.Name, appSettings);

            HubFABatch batch = new HubFABatch(savedFile.Name, faBatch);

            return Request.CreateResponse(HttpStatusCode.OK, batch);
        }
    }
}
