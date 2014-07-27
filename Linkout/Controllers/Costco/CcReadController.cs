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
    public class CcReadController : ApiController
    {
        IAppSettingsService appSettings;
        IFileService fileSrvc;
        IComHubModelService comHubSrvc;

        public CcReadController(IAppSettingsService appSettingsService, IFileService fileService, IComHubModelService comHubService)
        {
            appSettings = appSettingsService;
            fileSrvc = fileService;
            comHubSrvc = comHubService;
        }

        // GET read/orders
        [HttpGet]
        public HttpResponseMessage Orders()
        {
            IEnumerable<IDecryptFile> files = fileSrvc.CostcoReadFilesOrder(appSettings);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        // GET read/orders/filename.ext/
        [HttpGet]
        public HttpResponseMessage Orders(string id)
        {
            try
            {
                HubOrdBatch batch = comHubSrvc.CostcoHubOrdBatch(id.Trim(), appSettings, fileSrvc);
                return Request.CreateResponse(HttpStatusCode.OK, batch);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                var message = string.Format("Order with id {0} was not found. {1}", id, ex.Message);
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

        // GET read/confirms
        [HttpGet]
        public HttpResponseMessage Confirms()
        {
            IEnumerable<IDecryptFile> files = fileSrvc.CostcoReadFilesConfirm(appSettings);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        // GET read/confirms/filename.ext/
        [HttpGet]
        public HttpResponseMessage Confirms(string id)
        {
            try
            {
                HubConfBatch batch = comHubSrvc.CostcoHubConfBatch(id.Trim(), appSettings, fileSrvc);
                return Request.CreateResponse(HttpStatusCode.OK, batch);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                var message = string.Format("Confirm with id {0} was not found. {1}", id, ex.Message);
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }


        // GET read/fas
        [HttpGet]
        public HttpResponseMessage FAs()
        {
            IEnumerable<IDecryptFile> files = fileSrvc.CostcoReadFilesFA(appSettings);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        // GET read/fas/filename.ext/
        [HttpGet]
        public HttpResponseMessage FAs(string id)
        {
            try
            {
                HubFABatch batch = comHubSrvc.CostcoHubFABatch(id.Trim(), appSettings, fileSrvc);
                return Request.CreateResponse(HttpStatusCode.OK, batch);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                var message = string.Format("FA with id {0} was not found. {1}", id, ex.Message);
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

    }
}
