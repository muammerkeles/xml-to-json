using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MvcXmlJson.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ReadXml_giveJson()
        {
            HttpWebRequest request = HttpWebRequest.Create("https://www.w3schools.com/xml/cd_catalog.xml") as HttpWebRequest;
             
            request.ContentType = "text/xml";
            string results = string.Empty;
            WebResponse response = request.GetResponse();
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
 
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(results);
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            // To convert JSON text contained in string json into an XML node
            //XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
            response.Close();
            return Json(new { result = jsonText }, JsonRequestBehavior.AllowGet);
        }
    }
}