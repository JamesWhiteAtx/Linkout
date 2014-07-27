using System;
using System.Collections.Generic;

namespace ComHub
{
    public partial class ConfirmMessageBatch : MessageBatch
    {
        private List<hubConfirm> hubConfirmList;

        public ConfirmMessageBatch()
        {
            hubConfirmList = new List<hubConfirm>();
            xsdFile = "Confirmation.xsd";
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public participatingPartyName EnumPartyName { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string FileName { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string Merchant { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string OurPartnerId { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string PartyName { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public List<hubConfirm> HubConfirmList { get { return hubConfirmList; } set { hubConfirmList = value; } }

        public void CostcoSetup()
        {
            Merchant = MessageBatch.Costco;
            EnumPartyName = participatingPartyName.Costco;

            OurPartnerId = System.Configuration.ConfigurationManager.AppSettings["partnerID" + Merchant];
            PartyName = System.Configuration.ConfigurationManager.AppSettings["partyName" + Merchant];

            partnerID = new partnerID
            {
                name = "Roadwire LLC",
                roleType = partnerIDRoleType.vendor,
                roleTypeSpecified = true,
                Text = new string[] { OurPartnerId }
            };

        }

        //public hubConfirm MakeHubConfirm()
        //{
        //    return new hubConfirm(this);
        //}
        public hubConfirm AddHubConfirm()
        {
            hubConfirm newHubConfirm = new hubConfirm(this);
            return AddHubConfirm(newHubConfirm);
        }

        public hubConfirm AddHubConfirm(string partnerTrxID, string partnerTrxDt, string poNumber, string vendorsInvoiceNumber=null)
        {
            hubConfirm newHubConfirm = new hubConfirm(this)
            {
                partnerTrxID = partnerTrxID,
                partnerTrxDate = partnerTrxDt,
                poNumber = poNumber
            };

            if ((vendorsInvoiceNumber != null) && (vendorsInvoiceNumber != partnerTrxID))
            {
                newHubConfirm.trxData = new trxData { vendorsInvoiceNumber = vendorsInvoiceNumber };
            }

            return AddHubConfirm(newHubConfirm);
        }

        public hubConfirm AddHubConfirm(hubConfirm newHubConfirm)
        {
            hubConfirmList.Add(newHubConfirm);

            hubConfirm = hubConfirmList.ToArray();
            messageCount = hubConfirm.Length.ToString();

            return newHubConfirm;
        }

        public static ConfirmMessageBatch Deserialize(string path)
        {
            return MessageBatch.Deserialize<ConfirmMessageBatch>(path);
        }

    }

    public partial class hubConfirm
    {
        //private string packageDetailsPrfx = "P_";
        protected ConfirmMessageBatch confirmMessageBatch;
        private List<packageDetail> packageDetailList;
        private List<hubAction> hubActionList;

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public List<packageDetail> PackageDetailList { get { return packageDetailList; } set { packageDetailList = value; } }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public List<hubAction> HubActionList { get { return hubActionList; } set { hubActionList = value; } }

        public hubConfirm()
        {
            packageDetailList = new List<packageDetail>();
            hubActionList = new List<hubAction>();
        }

        public hubConfirm(ConfirmMessageBatch confMessageBatch)
            : this()
        {
            confirmMessageBatch = confMessageBatch;
            participatingParty = MakeParticipatingParty();
        }

        public participatingParty MakeParticipatingParty()
        {
            return new participatingParty
            {
                name = confirmMessageBatch.EnumPartyName,
                Text = new string[] { confirmMessageBatch.PartyName },
                roleType = participatingPartyRoleType.merchant,
                roleTypeSpecified = true,
                participationCode = participatingPartyParticipationCode.To,
                participationCodeSpecified = true
            };
        }

        public static hubAction MakeHubActionShip(
            string merchantLineNumber, string trxVendorSKU, string trxMerchantSKU, string trxQty,
            params string[] packageIDs)
        {
            List<packageDetailLink> packageDetailLinks = new List<packageDetailLink>();

            foreach (var id in packageIDs)
            {
                packageDetailLinks.Add(
                    new packageDetailLink { packageDetailID = id }
                );
            }

            return new hubAction
            {
                action = action.v_ship,
                merchantLineNumber = merchantLineNumber,
                trxVendorSKU = trxVendorSKU,
                trxMerchantSKU = trxMerchantSKU,
                trxQty = trxQty,
                packageDetailLink = packageDetailLinks.ToArray()
            };

        }

        public static hubAction MakeHubActionCancel(string actionCode,
            string merchantLineNumber, string trxVendorSKU, string trxMerchantSKU, string trxQty)
        {
            List<packageDetailLink> packageDetailLinks = new List<packageDetailLink>();

            return new hubAction
            {
                action = action.v_cancel,
                actionCode = actionCode,
                merchantLineNumber = merchantLineNumber,
                trxVendorSKU = trxVendorSKU,
                trxMerchantSKU = trxMerchantSKU,
                trxQty = trxQty,
                packageDetailLink = packageDetailLinks.ToArray()
            };

        }

        public hubAction AddHubActionShip(
            string merchantLineNumber, string trxVendorSKU, string trxMerchantSKU, string trxQty,
            params string[] packageIDs)
        {
            hubAction newHubAction = MakeHubActionShip(merchantLineNumber, trxVendorSKU, trxMerchantSKU, trxQty,
                packageIDs);

            hubActionList.Add(newHubAction);

            this.hubAction = hubActionList.ToArray();

            return newHubAction;
        }

        public hubAction AddHubActionCancel(string actionCode,
            string merchantLineNumber, string trxVendorSKU, string trxMerchantSKU, string trxQty)
        {
            hubAction newHubAction = MakeHubActionCancel(actionCode, merchantLineNumber, trxVendorSKU, trxMerchantSKU, trxQty);

            hubActionList.Add(newHubAction);

            this.hubAction = hubActionList.ToArray();

            return newHubAction;
        }

        public static packageDetail MakePackageDetail(
            string packageDetailID, string shipDate, string serviceLevel1, string trackingNumber, string weight)
        {
            return new packageDetail
            {
                packageDetailID = packageDetailID,
                shipDate = shipDate,
                serviceLevel1 = serviceLevel1,
                trackingNumber = trackingNumber,
                shippingWeight = new shippingWeight
                {
                    weightUnit = shippingWeightWeightUnit.LB,
                    Text = new string[] { weight }
                }
            };
        }

        //private string MakePackageDetailID(string idBase)
        //{
        //    return packageDetailsPrfx + idBase.Trim();
        //}

        public packageDetail AddPackageDetail(string packageDetailID,
            string shipDate, string serviceLevel1, string trackingNumber, string weight)
        {
            packageDetail newPackageDetail = MakePackageDetail(packageDetailID, shipDate, serviceLevel1, trackingNumber, weight);

            packageDetailList.Add(newPackageDetail);

            this.packageDetail = packageDetailList.ToArray();

            return newPackageDetail;
        }

    }

}

