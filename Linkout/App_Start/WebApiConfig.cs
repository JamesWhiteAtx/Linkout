using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Linkout
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "MakesApi",
                routeTemplate: "selector/makes",
                defaults: new { controller = "Makes", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "YearsApi",
                routeTemplate: "selector/make/{makeid}/years",
                defaults: new { controller = "Years", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "ModelsApi",
                routeTemplate: "selector/make/{makeid}/year/{year}/models",
                defaults: new { controller = "Models", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "BodiesApi",
                routeTemplate: "selector/make/{makeid}/year/{year}/model/{modelid}/bodies",
                defaults: new { controller = "Bodies", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "TrimsApi",
                routeTemplate: "selector/make/{makeid}/year/{year}/model/{modelid}/body/{bodyid}/trims",
                defaults: new { controller = "Trims", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "CarsApi",
                routeTemplate: "selector/make/{makeid}/year/{year}/model/{modelid}/body/{bodyid}/trim/{trimid}/cars",
                defaults: new { controller = "Cars", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "PtrnsApi",
                routeTemplate: "selector/car/{carid}/ptrns",
                defaults: new { controller = "Ptrns", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "IntColsApi",
                routeTemplate: "selector/car/{carid}/ptrn/{ptrnid}/intcols",
                defaults: new { controller = "IntCols", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "RecColsApi",
                routeTemplate: "selector/car/{carid}/ptrn/{ptrnid}/intcol/{intcolid}/reccols",
                defaults: new { controller = "RecCols", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "AllColsApi",
                routeTemplate: "selector/ptrn/{ptrnid}/allcols",
                defaults: new { controller = "AllCols", action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "ScheuleApi",
                routeTemplate: "schedule/installers/{zipcode}",
                defaults: new { controller = "Installers", action = "Get", zipcode = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ProductListingApi",
                routeTemplate: "product/listing",
                defaults: new { controller = "Product", action = "Listing" }
            );

            config.Routes.MapHttpRoute(
                name: "LeatherPriceApi",
                routeTemplate: "price/leather/{rows}",
                defaults: new { controller = "Price", action = "Leather" }
            );
            config.Routes.MapHttpRoute(
                name: "HeaterPriceApi",
                routeTemplate: "price/heater",
                defaults: new { controller = "Price", action = "Heater" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
