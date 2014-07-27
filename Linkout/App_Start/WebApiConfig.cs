using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Headers;

namespace Linkout
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "CcDownloadApi",
                routeTemplate: "ccdownload/{action}",
                defaults: new { controller = "CcDownload" }
            );
            config.Routes.MapHttpRoute(
                name: "CcUploadApi",
                routeTemplate: "ccupload/{action}/{id}",
                defaults: new { controller = "CcUpload", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CcDecryptApi",
                routeTemplate: "ccdecrypt/{action}/{id}",
                defaults: new { controller = "CcDecrypt", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CcEncryptApi",
                routeTemplate: "ccencrypt/{action}/{id}",
                defaults: new { controller = "CcEncrypt", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CcReadApiId",
                routeTemplate: "ccread/{action}/{id}",
                defaults: new { controller = "CcRead" }
            );

            config.Routes.MapHttpRoute(
                name: "CcReadApi",
                routeTemplate: "ccread/{action}",
                defaults: new { controller = "CcRead" }
            );

            config.Routes.MapHttpRoute(
                name: "CcSaveApi",
                routeTemplate: "ccsave/{action}/{id}",
                defaults: new { controller = "CcSave", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SlctrMakesApi",
                routeTemplate: "selector/makes",
                defaults: new { controller = "Makes", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgMakesApi",
                routeTemplate: "selector/{ctlg}/makes",
                defaults: new { controller = "Makes", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );

            config.Routes.MapHttpRoute(
                name: "SlctrYearsApi",
                routeTemplate: "selector/make/{makeid}/years",
                defaults: new { controller = "Years", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgYearsApi",
                routeTemplate: "selector/{ctlg}/make/{makeid}/years",
                defaults: new { controller = "Years", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );
            
            config.Routes.MapHttpRoute(
                name: "SlctrModelsApi",
                routeTemplate: "selector/make/{makeid}/year/{year}/models",
                defaults: new { controller = "Models", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgModelsApi",
                routeTemplate: "selector/{ctlg}/make/{makeid}/year/{year}/models",
                defaults: new { controller = "Models", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );
            
            config.Routes.MapHttpRoute(
                name: "SlctrBodiesApi",
                routeTemplate: "selector/make/{makeid}/year/{year}/model/{modelid}/bodies",
                defaults: new { controller = "Bodies", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgBodiesApi",
                routeTemplate: "selector/{ctlg}/make/{makeid}/year/{year}/model/{modelid}/bodies",
                defaults: new { controller = "Bodies", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );
            
            config.Routes.MapHttpRoute(
                name: "SlctrTrimsApi",
                routeTemplate: "selector/make/{makeid}/year/{year}/model/{modelid}/body/{bodyid}/trims",
                defaults: new { controller = "Trims", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgTrimsApi",
                routeTemplate: "selector/{ctlg}/make/{makeid}/year/{year}/model/{modelid}/body/{bodyid}/trims",
                defaults: new { controller = "Trims", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );
            
            config.Routes.MapHttpRoute(
                name: "SlctrCarsApi",
                routeTemplate: "selector/make/{makeid}/year/{year}/model/{modelid}/body/{bodyid}/trim/{trimid}/cars",
                defaults: new { controller = "Cars", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgCarsApi",
                routeTemplate: "selector/{ctlg}/make/{makeid}/year/{year}/model/{modelid}/body/{bodyid}/trim/{trimid}/cars",
                defaults: new { controller = "Cars", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );
            
            config.Routes.MapHttpRoute(
                name: "SlctrPtrnsApi",
                routeTemplate: "selector/car/{carid}/ptrns",
                defaults: new { controller = "Ptrns", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgPtrnsApi",
                routeTemplate: "selector/{ctlg}/car/{carid}/ptrns",
                defaults: new { controller = "Ptrns", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );
            
            config.Routes.MapHttpRoute(
                name: "SlctrIntColsApi",
                routeTemplate: "selector/car/{carid}/ptrn/{ptrnid}/intcols",
                defaults: new { controller = "IntCols", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgIntColsApi",
                routeTemplate: "selector/{ctlg}/car/{carid}/ptrn/{ptrnid}/intcols",
                defaults: new { controller = "IntCols", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );
            
            config.Routes.MapHttpRoute(
                name: "SlctrRecColsApi",
                routeTemplate: "selector/car/{carid}/ptrn/{ptrnid}/intcol/{intcolid}/reccols",
                defaults: new { controller = "RecCols", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgRecColsApi",
                routeTemplate: "selector/{ctlg}/car/{carid}/ptrn/{ptrnid}/intcol/{intcolid}/reccols",
                defaults: new { controller = "RecCols", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );
            
            config.Routes.MapHttpRoute(
                name: "SlctrAllColsApi",
                routeTemplate: "selector/ptrn/{ptrnid}/allcols",
                defaults: new { controller = "AllCols", action = "Get" }
            );
            config.Routes.MapHttpRoute(
                name: "SlctrCtlgAllColsApi",
                routeTemplate: "selector/{ctlg}/ptrn/{ptrnid}/allcols",
                defaults: new { controller = "AllCols", action = "Get" },
                constraints: new { ctlg = @"^[1-9]+$" }
            );

            
            config.Routes.MapHttpRoute(
                name: "ScheduleApi",
                routeTemplate: "schedule/installers/{zipcode}",
                defaults: new { controller = "Installers", action = "Get", zipcode = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ProductListingApi",
                routeTemplate: "product/listing",
                defaults: new { controller = "Product", action = "Listing" }
            );
            config.Routes.MapHttpRoute(
                name: "ProductApi",
                routeTemplate: "product/{id}",
                defaults: new { controller = "Product", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "NetSuiteApi",
                routeTemplate: "netsuite/{type}",
                defaults: new { controller = "NetSuite", type = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "NetSuiteFile",
                routeTemplate: "netsuitefile/{id}",
                defaults: new { controller = "NetSuiteFile", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
