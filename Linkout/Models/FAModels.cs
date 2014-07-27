using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ComHub
{
    public class HubAckMsgException
    {
        public string ID { get; set; }
        public List<string> Descriptions { get; set; }

        public HubAckMsgException()
        {
            Descriptions = new List<string>();
        }
    }

    public class HubAckMsg
    {
        public bool Accept { get; set; }
        public bool Order { get; set; }
        public string TrxID { get; set; }
        public List<HubAckMsgException> Exceptions { get; set; }

        public HubAckMsg()
        {
            Exceptions = new List<HubAckMsgException>();
        }
    }

    public class HubFA
    {
        public string TrxSetID { get; set; }
        public List<HubAckMsg> AckMsgs { get; set; }
    }

    public class HubFABatch
    {
        public HubFABatch() {}
        
        public HubFABatch(string fileName, FAMessageBatch faBatch)
            : this()
        {
            FileName = Path.GetFileName(fileName);
            FAs = (from fa in faBatch.hubFA
                   select new HubFA
                   {
                       TrxSetID = fa.messageBatchLink.trxSetID
                       ,
                       AckMsgs = (fa.messageAck != null)
                            ? (from m in fa.messageAck
                                select new HubAckMsg
                                {
                                    TrxID = m.trxID,
                                    Accept = (m.messageDisposition.status == FAMessageBatchHubFAMessageAckMessageDispositionStatus.A),
                                    Order = (m.type == FAMessageBatchHubFAMessageAckType.order),
                                    Exceptions = (m.detailException != null)
                                        ? (from e in m.detailException
                                            select new HubAckMsgException {
                                                ID = e.detailID,
                                                Descriptions = (e.exceptionDesc != null)
                                                    ? (from d in e.exceptionDesc select d).ToList()
                                                    : new List<string>()
                                            }).ToList()
                                        : new List<HubAckMsgException>()
                                }).ToList()
                                : new List<HubAckMsg>()
                   }).ToList();

        }

        public string FileName { get; set; }

        public List<HubFA> FAs { get; set; }

        public static FAMessageBatch FABatchFromJson(string json)
        {
            JObject jObjBatch = JObject.Parse(json);

            string fileName = (string)jObjBatch["FileName"];
            if (fileName == null)
            {
                fileName = (string)jObjBatch["NewFileName"];
            }
            fileName = Path.ChangeExtension(fileName, "xml");

            return HubFABatch.FABatchFromJson(fileName, jObjBatch);
        }

        public static FAMessageBatch FABatchFromJson(string fileName, JObject joBatch)
        {
            FAMessageBatch faBatch = new FAMessageBatch();
            faBatch.CostcoSetup();
            faBatch.FileName = fileName;

            var fas = (from f in joBatch["FAs"]
                       select f).ToList();

            FAMessageBatchHubFA newFA;
            foreach (var fa in fas)
            {
                newFA = faBatch.AddHubFA();
                newFA.messageBatchLink.trxSetID = (string)fa["TrxSetID"];

                var msgs = from m in fa["AckMsgs"]
                           select m;
                
                string trxID;
                bool order;
                bool accept;
                foreach (var msg in msgs)
                {
                    trxID = (string)msg["TrxID"];
                    order = (bool)msg["Order"];
                    accept = (bool)msg["Accept"];

                    if (order)
                    {
                        if (accept)
                        {
                            newFA.AddAckOrdAccept(trxID);
                        }
                        //else
                        //{
                        //    newFA.AddAckOrdReject(trxID);
                        //}
                    }
                    else
                    {
                        if (accept)
                        {
                            newFA.AddAckMsgAccept(trxID);
                        }
                    }
                    
                }
            }

            //string orderId = "106170111";
            //FAMessageBatchHubFAMessageAck newAck = newFA.AddAckOrdAccept(orderId);

            return faBatch;
        }
    }
}