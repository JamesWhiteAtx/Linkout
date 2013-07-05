using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linkout.Data;

namespace Linkout
{
    public enum LinkoutPriceTypes
    {
        LeaRow1, LeaRow2, LeaRow3,
        Heater1, Heater2,
        LeaRow1Heater1, LeaRow2Heater1, LeaRow3Heater1,
        LeaRow1Heater2, LeaRow2Heater2, LeaRow3Heater2
    }

    //public

    //public static class LinkoutPriceExtensions
    //{
    //    public static decimal GetPriceType(this LinkoutPriceTypes type, byte rows = 0, byte heaters = 0)
    //    {
            
    //        //Dictionary<int, decimal> prices = new Dictionary<int, decimal>{ 
    //        //    { 1, 300M }, { 2, 600M }, { 3, 900M } 
    //        //};

    //        //if (prices.ContainsKey( rows )) {


    //        return 2m;
    //    }
    //}

    public interface IPriceObj
    {
        string Type { get; set; }
        decimal Price { get; set; }
    }

    public interface ILeatherPrice : IPriceObj
    {
        int Rows { get; set; }
    }

    public interface IHeaterPrice : IPriceObj { }

    public class LeatherPrice : ILeatherPrice
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Rows { get; set; }
    }

    public class HeaterPrice : IHeaterPrice
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
    }


    public interface IPriceService
    {
        ILeatherPrice GetLeatherPrice(int rows);
        IHeaterPrice GetHeaterPrice();
    }

    public class PriceService: IPriceService
    {
        private LinkoutEntities _linkoutEntities;

        public PriceService(LinkoutEntities linkoutEntities)
        {
            _linkoutEntities = linkoutEntities;
        }

        public ILeatherPrice GetLeatherPrice(int rows)
        {
            Dictionary<int, decimal> prices = new Dictionary<int, decimal>{ 
                { 1, 1199M }, { 2, 1299M }, { 3, 1399M } 
            };

            if (prices.ContainsKey( rows )) {
                return new LeatherPrice
                {
                    Type = "leather",
                    Rows = rows,
                    Price = prices[rows]
                };
            } else {
                throw new Exception("Invalid number of rows ("+rows.ToString()+"). Only 1,2,3 allowed.");
            }
        }

        public IHeaterPrice GetHeaterPrice()
        {
            return new HeaterPrice
            {
                Type = "heater",
                Price = 199
            };
        }
    }
}
