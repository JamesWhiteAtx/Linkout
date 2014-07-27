using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComHub
{
    public interface IComHubModelService
    {
        HubOrdBatch CostcoHubOrdBatch(string id, IAppSettingsService appSettings, IFileService fileSrvc);
        HubConfBatch CostcoHubConfBatch(string p, IAppSettingsService appSettings, IFileService fileSrvc);
        HubFABatch CostcoHubFABatch(string p, IAppSettingsService appSettings, IFileService fileSrvc);
    }

    public class ComHubModelService : IComHubModelService
    {
        public HubOrdBatch CostcoHubOrdBatch(string fileName, IAppSettingsService appSettings, IFileService fileSrvc)
        {
            OrderMessageBatch orderBatch = fileSrvc.CostcoMessageBatchOrder(fileName, appSettings);
            HubOrdBatch batch = new HubOrdBatch(fileName, orderBatch);
            return batch;
        }

        public HubConfBatch CostcoHubConfBatch(string fileName, IAppSettingsService appSettings, IFileService fileSrvc)
        {
            ConfirmMessageBatch confirmBatch = fileSrvc.CostcoMessageBatchConfirm(fileName, appSettings);
            HubConfBatch batch = new HubConfBatch(fileName, confirmBatch);
            return batch;
        }

        public HubFABatch CostcoHubFABatch(string fileName, IAppSettingsService appSettings, IFileService fileSrvc)
        {
            FAMessageBatch faBatch = fileSrvc.CostcoMessageBatchFA(fileName, appSettings);
            HubFABatch batch = new HubFABatch(fileName, faBatch);
            return batch;
        }
    }
}