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
        void Update(int id, string description, decimal price);
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

        public void Update(int id, string description, decimal price)
        {
            var prod = (from p in _linkoutEntities.CostcoProducts
                        where p.ID == id
                        select p).FirstOrDefault();
            
            if (prod != null) { 
                prod.Description = description;
                prod.Price = price;

                _linkoutEntities.SaveChanges();
            }
        }

    }
}