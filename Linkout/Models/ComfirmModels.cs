using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ComHub
{
    public class HubConfPackage
    {
        public string ID { get; set; }
        public string ShipDate { get; set; }
        public string ServiceLevel1 { get; set; }
        public string TrackingNumber { get; set; }
        public string Weight { get; set; }
    }

    public class HubConfAction
    {
        public bool Shipment { get; set; }
        public string ActionCode { get; set; }
        public string MerchantLineNumber { get; set; }
        public string TrxVendorSKU { get; set; }
        public string TrxMerchantSKU { get; set; }
        public string TrxQty { get; set; }
        public string[] PackageIDs { get; set; }
    }

    public class HubConfirm
    {
        public string PartnerTrxId { get; set; }
        public string PartnerTrxDt { get; set; }
        public string PoNumber { get; set; }
        public string VendorsInvoiceNumber { get; set; }

        public List<HubConfPackage> Packages { get; set; }
        public List<HubConfAction> Actions { get; set; }
    }

    public class HubConfBatch
    {
        public HubConfBatch() {}

        public HubConfBatch(string fileName, ConfirmMessageBatch confirmBatch) : this()
        {
            FileName = Path.GetFileName(fileName);
            Confirms = (from c in confirmBatch.hubConfirm
                        select new HubConfirm
                        {
                            PartnerTrxId = c.partnerTrxID,
                            PartnerTrxDt = c.partnerTrxDate,
                            PoNumber = c.poNumber,
                            VendorsInvoiceNumber = (c.trxData != null) ? c.trxData.vendorsInvoiceNumber : null,

                            Packages = (c.packageDetail != null)
                                        ? (from p in c.packageDetail
                                           select new HubConfPackage
                                           {
                                               ID = p.packageDetailID,
                                               ShipDate = p.shipDate,
                                               ServiceLevel1 = p.serviceLevel1,
                                               TrackingNumber = p.trackingNumber,
                                               Weight = (p.shippingWeight != null) ? p.shippingWeight.Text.FirstOrDefault() : null
                                           }).ToList()
                                            : null,

                            Actions = (c.hubAction != null)
                                        ? (from a in c.hubAction
                                           select new HubConfAction
                                           {
                                               Shipment = (a.action == action.v_ship),
                                               ActionCode = a.actionCode,
                                               MerchantLineNumber = a.merchantLineNumber,
                                               TrxVendorSKU = a.trxVendorSKU,
                                               TrxMerchantSKU = a.trxMerchantSKU,
                                               TrxQty = a.trxQty,
                                               PackageIDs = (a.packageDetailLink != null) ? (from l in a.packageDetailLink select l.packageDetailID).ToArray() : null
                                           }).ToList()
                                        : null
                        }).ToList();
        }

        public string FileName { get; set; }

        public List<HubConfirm> Confirms { get; set; }

        public static ConfirmMessageBatch ConfBatchFromJson(string json)
        {
            JObject jObjBatch = JObject.Parse(json);

            string fileName = (string)jObjBatch["FileName"];
            if (fileName == null)
            {
                fileName = (string)jObjBatch["NewFileName"];
            }
            fileName = Path.ChangeExtension(fileName, "xml");

            return HubConfBatch.ConfBatchFromJObj(fileName, jObjBatch);
        }

        public static ConfirmMessageBatch ConfBatchFromJObj(string fileName, JObject jsonBatch)
        {
            string poNumber;
            string vendorsInvoiceNumber;
            string partnerTrxId;
            string partnerTrxDt;

            hubConfirm newHubConfirm;

            string packageID;
            string shipDate;
            string serviceLevel1;
            string trackingNumber;
            string weight;

            packageDetail newackageDetail;

            bool shipment;
            string merchantLineNumber;
            string actionCode;
            string trxVendorSKU;
            string trxMerchantSKU;
            string trxQty;
            string[] pkgIDs;

            hubAction newHubAction;

            ConfirmMessageBatch confirmBatch = new ConfirmMessageBatch();
            confirmBatch.CostcoSetup();
            confirmBatch.FileName = fileName;

            var confirms = from c in jsonBatch["Confirms"]
                           select c;
            foreach (var c in confirms)
            {
                poNumber = (string)c["PoNumber"];
                partnerTrxId = (string)c["PartnerTrxId"];
                partnerTrxDt = (string)c["PartnerTrxDt"];
                vendorsInvoiceNumber = (string)c["VendorsInvoiceNumber"];

                newHubConfirm = confirmBatch.AddHubConfirm(partnerTrxId, partnerTrxDt, poNumber, vendorsInvoiceNumber);


                var packages = from p in c["Packages"]
                               select p;
                foreach (var p in packages)
                {
                    packageID = (string)p["ID"];
                    shipDate = (string)p["ShipDate"];
                    serviceLevel1 = (string)p["ServiceLevel1"];
                    trackingNumber = (string)p["TrackingNumber"];
                    weight = (string)p["Weight"];

                    newackageDetail = newHubConfirm.AddPackageDetail(packageID, shipDate, serviceLevel1, trackingNumber, weight);
                }

                var actions = from a in c["Actions"]
                              select a;
                foreach (var a in actions)
                {
                    shipment = (bool)a["Shipment"];
                    merchantLineNumber = (string)a["MerchantLineNumber"];
                    actionCode = (string)a["ActionCode"];
                    trxVendorSKU = (string)a["TrxVendorSKU"];
                    trxMerchantSKU = (string)a["TrxMerchantSKU"];
                    trxQty = (string)a["TrxQty"];

                    pkgIDs = (from i in a["PackageIDs"]
                              select (string)i).ToArray();

                    if (shipment)
                    {
                        newHubAction = newHubConfirm.AddHubActionShip(merchantLineNumber, trxVendorSKU, trxMerchantSKU, trxQty,
                            pkgIDs);
                    }
                    else
                    {
                        newHubAction = newHubConfirm.AddHubActionCancel(actionCode, merchantLineNumber, trxVendorSKU, trxMerchantSKU, trxQty);
                    }
                }

            }

            return confirmBatch;
        }
    }

}