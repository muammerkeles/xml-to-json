# xml-to-json
 	##Javascript Side;
	ajax.open("GET", "@Url.Action("ReadXml_giveJson")", true);
	ajax.send();
	ajax.onreadystatechange = function () {
		if (ajax.readyState == 4 && ajax.status == 200) {
		var _temp= JSON.parse(ajax.responseText).result;
		var obj = JSON.parse(_temp).CATALOG;
		var mydata= "<ul><li><b>Title  - Price </b></li>";
		Array.prototype.forEach.call(obj.CD, function (entry){
			mydata +="<li>"+ entry.TITLE + " : " + entry.PRICE + "</li>"
		});
		document.getElementById("result1").innerHTML = mydata +  "</ul>";
	}
	
		## MVC-Controller Side
		HttpGet]
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
					response.Close();
					return Json(new { result = jsonText }, JsonRequestBehavior.AllowGet);
			}
	
