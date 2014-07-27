using System;
using System.Collections.Generic;

namespace ComHub
{
    public partial class OrderMessageBatch : MessageBatch
    {
        public OrderMessageBatch()
        {
            xsdFile = "Order.xsd";
        }

        public static OrderMessageBatch Deserialize(string path)
        {
            return MessageBatch.Deserialize<OrderMessageBatch>(path);
        }

    }
}
