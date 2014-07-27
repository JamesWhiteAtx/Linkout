using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComHub
{
    public partial class FAMessageBatch : MessageBatch
    {
        private List<FAMessageBatchHubFA> hubFAList;

        public FAMessageBatch()
        {
            hubFAList = new List<FAMessageBatchHubFA>();
            xsdFile = "FA.xsd";
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string Merchant { get; set; }

        public void CostcoSetup()
        {
            Merchant = MessageBatch.Costco;
            partnerID = System.Configuration.ConfigurationManager.AppSettings["partnerID" + Merchant];
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string FileName { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public List<FAMessageBatchHubFA> HubFAList { get { return hubFAList; } set { hubFAList = value; } }

        public FAMessageBatchHubFA AddHubFA()
        {
            FAMessageBatchHubFA newHubFA = new FAMessageBatchHubFA(this);
            return AddHubFA(newHubFA);
        }

        public FAMessageBatchHubFA AddHubFA(FAMessageBatchHubFA newHubFA)
        {
            hubFAList.Add(newHubFA);

            hubFA = hubFAList.ToArray();
            messageCount = hubFA.Length.ToString();

            return newHubFA;
        }

        public static FAMessageBatch Deserialize(string path)
        {
            return MessageBatch.Deserialize<FAMessageBatch>(path);
        }
    }

    public partial class FAMessageBatchHubFA
    {
        protected FAMessageBatch faMessageBatch;
        protected List<FAMessageBatchHubFAMessageAck> messageAckList;

        public FAMessageBatchHubFA()
        {
            messageBatchLink = new FAMessageBatchHubFAMessageBatchLink();
            messageBatchDisposition = new FAMessageBatchHubFAMessageBatchDisposition();
            
            messageAckList = new List<FAMessageBatchHubFAMessageAck>();
            AssignMessageAcks();
        }

        public FAMessageBatchHubFA(FAMessageBatch faMsgBatch)
            : this()
        {
            faMessageBatch = faMsgBatch;
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public List<FAMessageBatchHubFAMessageAck> MessageAckList { get { return messageAckList; } }        

        public void AssignMessageAcks()
        {
            messageAck = messageAckList.ToArray();
            int acks = messageAck.Length;
            int acpts = messageAck.Count(ack => ack.messageDisposition.status == FAMessageBatchHubFAMessageAckMessageDispositionStatus.A);

            messageBatchDisposition.trxReceivedCount = acks.ToString();
            messageBatchDisposition.trxAcceptedCount = acpts.ToString();

            if (acks > 0)
            {
                if (acks == acpts)
                {
                    messageBatchDisposition.status = FAMessageBatchHubFAMessageBatchDispositionStatus.A;        
                }
                else if (acpts == 0)
                {
                    messageBatchDisposition.status = FAMessageBatchHubFAMessageBatchDispositionStatus.R;
                }
                else 
                {
                    messageBatchDisposition.status = FAMessageBatchHubFAMessageBatchDispositionStatus.P;
                }
            }
            else
            {
                messageBatchDisposition.status = FAMessageBatchHubFAMessageBatchDispositionStatus.P;
            }
        }

        public FAMessageBatchHubFAMessageAck AddAckOrdAccept(string orderId)
        {
            FAMessageBatchHubFAMessageAck newAck = new FAMessageBatchHubFAMessageAck(FAMessageBatchHubFAMessageAckType.order); // Message

            newAck.trxID = orderId;
            newAck.messageDisposition.status = FAMessageBatchHubFAMessageAckMessageDispositionStatus.A;// Accept

            return AddMessageAck(newAck);
        }

        public FAMessageBatchHubFAMessageAck AddAckOrdReject(string orderId, string detailID, string detailException)
        {
            FAMessageBatchHubFAMessageAck newAck = new FAMessageBatchHubFAMessageAck(FAMessageBatchHubFAMessageAckType.order); // Message

            newAck.trxID = orderId;
            newAck.messageDisposition.status = FAMessageBatchHubFAMessageAckMessageDispositionStatus.R; //reject

            newAck.detailException = new FAMessageBatchHubFAMessageAckDetailException[1]
            {
                new FAMessageBatchHubFAMessageAckDetailException(detailID, detailException)
            };

            return AddMessageAck(newAck);
        }

        public FAMessageBatchHubFAMessageAck AddAckMsgAccept(string trxID)
        {
            FAMessageBatchHubFAMessageAck newAck = new FAMessageBatchHubFAMessageAck(FAMessageBatchHubFAMessageAckType.msg); // Message

            newAck.trxID = trxID;
            newAck.messageDisposition.status = FAMessageBatchHubFAMessageAckMessageDispositionStatus.A;// Accept

            return AddMessageAck(newAck);
        }

        public FAMessageBatchHubFAMessageAck AddMessageAck(FAMessageBatchHubFAMessageAckType type)
        {
            FAMessageBatchHubFAMessageAck newAck = new FAMessageBatchHubFAMessageAck(type);
            return AddMessageAck(newAck);
        }

        public FAMessageBatchHubFAMessageAck AddMessageAck(FAMessageBatchHubFAMessageAck newAck)
        {
            messageAckList.Add(newAck);
            AssignMessageAcks();
            return newAck;
        }
    }

    public partial class FAMessageBatchHubFAMessageAck
    {
        public FAMessageBatchHubFAMessageAck()
        {
            messageDispositionField = new FAMessageBatchHubFAMessageAckMessageDisposition();
        }

        public FAMessageBatchHubFAMessageAck(FAMessageBatchHubFAMessageAckType type) : this()
        {
            this.type = type;
        }
    }

    public partial class FAMessageBatchHubFAMessageAckMessageDisposition
    {
        public FAMessageBatchHubFAMessageAckMessageDisposition()
        {
            status = FAMessageBatchHubFAMessageAckMessageDispositionStatus.A;
            statusSpecified = true;
        }
    }


    public partial class FAMessageBatchHubFAMessageBatchDisposition
    {
        public FAMessageBatchHubFAMessageBatchDisposition()
        {
            status = FAMessageBatchHubFAMessageBatchDispositionStatus.P;
            statusSpecified = true;
        }
    }

    public partial class FAMessageBatchHubFAMessageAckDetailException
    {
        public FAMessageBatchHubFAMessageAckDetailException()
        {
            exceptionDesc = new string[0];
        }

        public FAMessageBatchHubFAMessageAckDetailException(string detailID, string detailException): this()
        {
            this.detailID = detailID;
            this.exceptionDesc = new string[1] { detailException };
        }
    }
}
