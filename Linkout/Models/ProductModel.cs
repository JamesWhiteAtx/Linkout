﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkout.Models
{
    public class ProdDescrPriceModel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductModel : ProdDescrPriceModel
    {
        public decimal ID { get; set; }
        public string Code { get; set; }
        public decimal? LeatherRows { get; set; }
        public decimal? Heaters { get; set; }
    }
}