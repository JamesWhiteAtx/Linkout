using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Linkout.Data;
using Linkout.Models;

namespace Linkout
{
    public interface IProductService
    {
        IEnumerable<ProductModel> Listing();
    }

    public class ProductService : IProductService
    {
        private LinkoutEntities _linkoutEntities;

        public ProductService(LinkoutEntities linkoutEntities)
        {
            _linkoutEntities = linkoutEntities;
        }

        public IEnumerable<ProductModel> Listing()
        {
            var prods = from p in _linkoutEntities.CostcoProducts
                        select new ProductModel
                        {
                            ID = p.ID,
                            Code = p.Code,
                            Description = p.Description,
                            Price = p.Price,
                            LeatherRows = p.LeatherRows,
                            Heaters = p.SeatHeaters
                        };

            return prods; 
        }

    }
}