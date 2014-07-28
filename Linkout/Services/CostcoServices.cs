using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace Linkout
{
    public interface IContentService
    {
        IEnumerable<FileInfo> ColorFiles();
        IEnumerable<ColorModel> ColorModels();
    }

    public class ContentService : IContentService
    {
        const string colorPath = "/images/costco/colors";

        public IEnumerable<FileInfo> ColorFiles()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(colorPath);
            DirectoryInfo folder = new DirectoryInfo(path);
            return folder.GetFiles();
        }

        public IEnumerable<ColorModel> ColorModels()
        {
            var colors = from file in ColorFiles()
                         select new ColorModel
                         {
                             Code = Path.GetFileNameWithoutExtension(file.Name),
                             Url = colorPath + "/" + file.Name
                         };

            return colors.ToList();
        }

    }


    public interface ICcOrderService
    {
        //IEnumerable<OrderModel> Listing();
        //OrderModel Query(int id);
        JsonModel Insert(JsonModel model);
    }

    public class CcOrderService : ICcOrderService
    {
        private INetSuiteCcOrderUriService _nsUriService;

        public CcOrderService(INetSuiteCcOrderUriService nsRestService)
        {
            _nsUriService = nsRestService;
        }

        //public IEnumerable<OrderModel> Listing() 
        //{
        //    return new List<OrderModel>()
        //    {
        //        new OrderModel {},
        //        new OrderModel {}
        //    };
        //}

        //public OrderModel Query(int id) 
        //{
        //    return new OrderModel();
        //}

        public JsonModel Insert(JsonModel model)
        {
            string responseFromServer = String.Empty;

            string uri = _nsUriService.Uri.AbsoluteUri;

            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            _nsUriService.LoadHeaders(request.Headers);

            //string payload = JsonConvert.SerializeObject(model.Json);
            string payload = model.Json;

            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(payload);
            request.ContentLength = bytes.Length;
            System.IO.Stream os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
            }

            return new JsonModel { Json = responseFromServer };
        }
    }

    public interface ICcProductService
    {
        IEnumerable<CcProductModel> Listing();
        void Update(CcProductModel prod);
        void Update(int id, string description, decimal price, string pageUrl);
    }

    public class CcProductService : ICcProductService
    {
        //private CostcoEntities _costcoEntities;
        //public CcProductService(CostcoEntities costcoEntities)
        //{
        //    _costcoEntities = costcoEntities;
        //}

        public IEnumerable<CcProductModel> Listing()
        {
            //var prods = from p in _costcoEntities.CostcoProducts
            //            where p.ActiveFlag == "Y"
            //            select new CcProductModel
            //            {
            //                ID = p.ID,
            //                Code = p.Code,
            //                Description = p.Description,
            //                Price = p.Price,
            //                LeatherRows = p.LeatherRows,
            //                Heaters = p.SeatHeaters,
            //                PageUrl = p.PageUrl
            //            };

            List<CcProductModel> prods = new List<CcProductModel>();
            return prods;
        }

        public void Update(CcProductModel prod)
        {
            int id = Convert.ToInt32(prod.ID);
            Update(id, prod.Description, prod.Price, prod.PageUrl);
        }

        public void Update(int id, string description, decimal price, string pageUrl)
        {
            //var prod = (from p in _costcoEntities.CostcoProducts
            //            where p.ID == id
            //            select p).FirstOrDefault();

            //if (prod != null)
            //{
            //    prod.Description = description;
            //    prod.Price = price;
            //    prod.PageUrl = pageUrl;

            //    _costcoEntities.SaveChanges();
            //}
        }
    }
}