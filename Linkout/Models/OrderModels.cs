using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ComHub
{
    public class HubOrdPersPlace
    {
        public string ID { get; set; }
    }

    public class HubOrdLine
    {
        public string ID { get; set; }
        public string OrderLineNumber { get; set; }
        public string MerchantLineNumber { get; set; }
        public string ShippingCode { get; set; }
        public string MerchantSKU { get; set; }
        public string VendorSKU { get; set; }
        public string QtyOrdered { get; set; }
        public string Weight { get; set; }
    }

    public class HubOrder
    {
        public string PoNumber { get; set; }
        public string OrderID { get; set; }
        public List<HubOrdLine> Lines { get; set; }
        public List<HubOrdPersPlace> PersPlaces { get; set; }
    }

    public class HubOrdBatch
    {
        public string FileName { get; set; }
        public string BatchNumber { get; set; }
        public string PartnerID { get; set; }
        public List<HubOrder> Orders { get; set; }

        public HubOrdBatch()
        {

        }

        public HubOrdBatch(string fileName, OrderMessageBatch orderBatch)
            : this()
        {
            FileName = fileName;
            PartnerID = "roadwirellc";
            BatchNumber = orderBatch.batchNumber;
            Orders = (from ord in orderBatch.hubOrder
                        select new HubOrder
                        {
                            PoNumber = ord.poNumber
                            ,
                            OrderID = ord.orderId
                            ,
                            Lines = (from ln in ord.lineItem
                                    select new HubOrdLine
                                    {
                                        ID = ln.lineItemId,
                                        OrderLineNumber = ln.orderLineNumber,
                                        MerchantLineNumber = ln.merchantLineNumber,
                                        ShippingCode = ln.shippingCode,
                                        MerchantSKU = ln.merchantSKU,
                                        VendorSKU = ln.vendorSKU,
                                        QtyOrdered = ln.qtyOrdered,
                                        Weight = ln.poLineData.unitShippingWeight.Text.FirstOrDefault()
                                    }).ToList()

                            ,
                            PersPlaces = (from pp in ord.personPlace
                                        select new HubOrdPersPlace
                                        {
                                            ID = pp.personPlaceID
                                        }).ToList()

                        }).ToList();

        }
    }

}