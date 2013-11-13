using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Text;
using System.Net;

namespace Linkout.Controllers
{
    public class IdNameObj 
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public IdNameObj(string id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    public abstract class JsonResp
    {
        protected bool _success = false;
        public bool Success { get { return _success; } }

        public IJsonWebResponseService WebResponseService { get; set; }

        public INetSuiteConfigService NsConfigService { get; set; }

        public NetSuiteUriBase NetSuiteUriService { get; set; }

        public JsonResp()
        {
            IJsonHttpResponseService jsonHttpResponseService = new JsonHttpResponseService();
            WebResponseService = new JsonWebResponseService(jsonHttpResponseService);

            NetSuiteConfiguration nsSect = ConfigurationManager.GetSection(NetSuiteConfiguration.ConfigSectionName) as NetSuiteConfiguration;
            NsConfigService = nsSect.Uri;
        }

        public void AddQuery(string name, string value)
        {
            NetSuiteUriService.AddQuery(name, value);
        }

        protected Newtonsoft.Json.Linq.JObject _response;
        public Newtonsoft.Json.Linq.JObject Response
        {
            get
            {
                if (_response == null)
                {
                    LoadResponse();
                }

                return _response;
            }
        }

        public abstract void LoadResponse();

        public abstract string GetServerJsonResponse();

        public JObject MakeJObjectResp()
        {
            return Newtonsoft.Json.Linq.JObject.Parse(GetServerJsonResponse());
        }
    }

    public class JsonSelResp : JsonResp
    {
        private Dictionary<string, IdNameObj> _dictSelList;
        protected string _listName;
        public string ListName { get { return _listName; } }
        public INetSuiteUriSelectorService NetSuiteUriSelectorService { get; set; }

        public JsonSelResp() : base()
        {
            //IJsonHttpResponseService jsonHttpResponseService = new JsonHttpResponseService();
            //WebResponseService = new JsonWebResponseService(jsonHttpResponseService);
            //INetSuiteConfigService nsConfigService;
            //NetSuiteConfiguration nsSect = ConfigurationManager.GetSection(NetSuiteConfiguration.ConfigSectionName) as NetSuiteConfiguration;
            //nsConfigService = nsSect.Uri;

            NetSuiteUriSelectorService = new NetSuiteUriScriptSelector(NsConfigService);

            NetSuiteUriService = (NetSuiteUriBase)NetSuiteUriSelectorService;
        }

        public override void LoadResponse()
        {
            _response = MakeJObjectResp();

            JToken succProp;
            if (_response.TryGetValue("success", out succProp) == true)
            {
                _success = (bool)succProp;
            }
            else
            {
                _success = false;
            }
        }

        public override string GetServerJsonResponse()
        {
            return WebResponseService.GetServerJsonResponse(NetSuiteUriSelectorService.Uri);
        }

        public Dictionary<string, IdNameObj> DictSelList
        {
            get
            {
                if (_dictSelList == null)
                {
                    LoadResponse();

                    if (Success)
                    {
                        _dictSelList = MakeDictSelObj();
                    }
                    else
                    {
                        _dictSelList = new Dictionary<string, IdNameObj>();
                    }
                }
                return _dictSelList;
            }
        }

        public List<IdNameObj> SelObjList
        {
            get
            {
                return DictSelList.Select(i => (IdNameObj)i.Value).ToList();  
            }
        }

        virtual public Dictionary<string, IdNameObj> MakeDictSelObj()
        {
            var list = from i in Response[ListName]
                        select new { Name = (string)i["name"], ID = (string)i["id"] };
            return list.ToDictionary(i => i.ID, o => new IdNameObj(o.ID, o.Name));
        }

        public IdNameObj GetSelObjForName(string name)
        {
            var item = DictSelList.FirstOrDefault(x => x.Value.Name == name);
            return item.Value;
        }
    }

    public class MakeResp : JsonSelResp
    {
        public MakeResp() : base()
        {
            AddQuery("type", "makes");
            _listName = "makes";
        }
    }

    public class YearResp : JsonSelResp
    {
        public YearResp(string makeid)
            : base()
        {
            AddQuery("type", "years");
            AddQuery("makeid", makeid);
            _listName = "years";
        }
    }

    public class ModelResp : JsonSelResp
    {
        public ModelResp(string makeid, string year)
            : base()
        {
            AddQuery("type", "models");
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            _listName = "models";
        }
    }

    public class BodyResp : JsonSelResp
    {
        public BodyResp(string makeid, string year, string modelid)
            : base()
        {
            AddQuery("type", "bodies");
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            _listName = "bodies";
        }
    }

    public class TrimResp : JsonSelResp
    {
        public TrimResp(string makeid, string year, string modelid, string bodyid)
            : base()
        {
            AddQuery("type", "trims");
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            AddQuery("bodyid", bodyid);
            _listName = "trims";
        }
    }

    public class CarResp : JsonSelResp
    {
        public CarResp(string makeid, string year, string modelid, string bodyid, string trimid)
            : base()
        {
            AddQuery("type", "cars");
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            AddQuery("bodyid", bodyid);
            AddQuery("trimid", trimid);
            _listName = "cars";
        }
    }

    public class PtrnSelObj : IdNameObj 
    {
        public string Rows { get; set; }
        public string Airbags { get; set; }
        public string PatternCd { get; set; }
        public PtrnSelObj(string id, string name, string code, string rows, string airbags)
            : base(id, name)
        {
            PatternCd = code;
            Rows = rows;
            Airbags = airbags;
        }
    }

    public class PtrnResp : JsonSelResp
    {
        public PtrnResp(string carid)
            : base()
        {
            AddQuery("type", "carptrns");
            AddQuery("carid", carid);
            _listName = "patterns";
        }

        override public Dictionary<string, IdNameObj> MakeDictSelObj()
        {
            var list = from i in Response[ListName]
                       select new
                       {
                           ID = (string)i["id"],
                           Name = (string)i["seldescr"],
                           PatternCd = (string)i["name"],
                           Rows = (string)i["rowsname"],
                           Airbags = (string)i["airbagname"]
                       };

            return list.ToDictionary(i => i.ID, o => (IdNameObj)new PtrnSelObj(o.ID, o.Name, o.PatternCd, o.Rows, o.Airbags));
        }
    }

    public class KitSelObj : IdNameObj
    {
        public string ColorCd { get; set; }
        public string ColorDescr { get; set; }
        public KitSelObj(string id, string name, string colorCd, string colorDescr)
            : base(id, name)
        {
            ColorCd = colorCd;
            ColorDescr = colorDescr;
        }
    }

    public class KitResp : JsonSelResp
    {
        public KitResp(string ptrnid)
            : base()
        {
            AddQuery("type", "ptrncolors");
            AddQuery("ptrnid", ptrnid);
            _listName = "colors";
        }

        override public Dictionary<string, IdNameObj> MakeDictSelObj()
        {
            var list = from i in Response[ListName]
                       select new
                       {
                           ID = (string)i["id"],
                           Name = (string)i["name"],
                           ColorCd = (string)i["leacolor"],
                           ColorDescr = (string)i["leacolorname"]
                       };

            return list.ToDictionary(i => i.ID, o => (IdNameObj)new KitSelObj(o.ID, o.Name, o.ColorCd, o.ColorDescr));
        }
    }


    public class RestletResp : JsonResp
    {
        public INetSuiteUriRestService RestUriService { get; set; }
        
        public RestletResp()
        {
            RestUriService = new NetSuiteUriRest(NsConfigService);
            NetSuiteUriService = (NetSuiteUriBase)RestUriService;
        }

        public override void LoadResponse()
        {
            _response = MakeJObjectResp();
            if (_response != null)
            {
                _success = _response.HasValues;
            }
            else
            {
                _success = false;
            }
        }

        public override string GetServerJsonResponse()
        {
            return WebResponseService.GetServerJsonResponse(RestUriService.Uri, "GET", RestUriService.MakeHeaders());
        }

        public string ScriptID
        {
            get { return RestUriService.ScriptID; }
            set { RestUriService.ScriptID = value; }
        }

        private string _deployID;

        public string DeployID
        {
            get { return RestUriService.DeployID; }
            set { RestUriService.DeployID = value; }
        }

    }


    public class DownloadController : Controller
    {
        // GET: /Download/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public FileContentResult List()
        {
            RestletResp o = new RestletResp();
            o.ScriptID = "33";
            o.DeployID = "1";

            Newtonsoft.Json.Linq.JArray list = o.Response.SelectToken("obj.prop1.list") as Newtonsoft.Json.Linq.JArray;

            JObject row = list[0].Value<JObject>();

            StringBuilder lines = new StringBuilder();

            lines.AppendLine(string.Join(",", row.Properties().Select(p => "\"" + p.Name + "\"").ToArray()));

            foreach (var r in list)
            {
                row = r.Value<JObject>();
                lines.AppendLine(string.Join(",", row.Properties().Select(p => "\"" + p.Value.ToString() + "\"").ToArray()));
            }

            return File(new System.Text.UTF8Encoding().GetBytes(lines.ToString()), "text/csv", "JsonList.csv");
        }

        [Authorize]
        public FileContentResult CarParts()
        {
            //string csv = "Charlie, Chaplin, Chuckles " + Environment.NewLine + "Xharlie, Xhaplin, Xhuckles " ;

            StringBuilder lines = new StringBuilder();
            lines.AppendLine(string.Join(",", new[] { "Make", "Year", "Model", "Body", "Trim", "PartNo", "PatternDescr", "PatternCD", "Color", "Rows", "Airbags" }));

            IdNameObj make;
            MakeResp makeResp = new MakeResp();
            make = makeResp.GetSelObjForName("TOYOTA");

            YearResp yearResp = new YearResp(make.ID);
            var years = yearResp.SelObjList.Where(y => Convert.ToDouble(y.Name) > 2011);
            foreach (var year in years)
            {
                ModelResp modelResp = new ModelResp(make.ID, year.Name);
                var models = modelResp.SelObjList;
                foreach (var model in models)
                {
                    BodyResp bodyResp = new BodyResp(make.ID, year.Name, model.ID);
                    var bodies = bodyResp.SelObjList;
                    foreach (var body in bodies)
                    {
                        TrimResp trimResp = new TrimResp(make.ID, year.Name, model.ID, body.ID);
                        var trims = trimResp.SelObjList;
                        foreach (var trim in trims)
                        {
                            CarResp carResp = new CarResp(make.ID, year.Name, model.ID, body.ID, trim.ID);
                            var cars = carResp.SelObjList;
                            foreach (var car in cars)
                            {
                                PtrnResp ptrnResp = new PtrnResp(car.ID);
                                var ptrns = ptrnResp.SelObjList;
                                foreach (var p in ptrns)
                                {
                                    PtrnSelObj ptrn = (PtrnSelObj)p;
                                    KitResp kitResp = new KitResp(ptrn.ID);
                                    var kits = kitResp.SelObjList;
                                    foreach (var k in kits)
                                    {
                                        KitSelObj kit = (KitSelObj)k;

                                        //lines.AppendLine(string.Join(",", new[] { "Make", "Year", "Model", "Body", "Trim", "PartNo", "PatternDescr", "PatternCD", "Color", "Rows", "Airbags" }));

                                        lines.AppendLine(string.Join(",", new[] { make.Name, year.Name, model.Name, body.Name, trim.Name, kit.Name, ptrn.Name, ptrn.PatternCd, kit.ColorDescr, ptrn.Rows, ptrn.Airbags}));
                                    };
                                };
                            };
                        };
                    };
                };
            };

            return File(new System.Text.UTF8Encoding().GetBytes(lines.ToString()), "text/csv", "RoadwireSkus.csv");
        }
      

    }
}
