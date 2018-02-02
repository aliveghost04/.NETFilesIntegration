using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilesIntegration.Models;
using System.Xml;

namespace FilesIntegration.Controllers
{
    public class XMLManagerController : Controller
    {
        FilesIntegrationDb _db = new FilesIntegrationDb();

        public ActionResult PullXmlIndex()
        {
            return View();
        }

        public ActionResult PullXml()
        {
            var attachedFile = Request.Files["FileUpload"];
            var document = new XmlDocument();
            List<AccountingSeat> accountingSeat = new List<AccountingSeat>();

            if (attachedFile != null) document.Load(attachedFile.InputStream);
            //document.Load(Server.MapPath("~/Files/Xml/Asiento.xml"));


            //Loop through the selected Nodes.
            foreach (XmlNode node in document.SelectNodes("/Asiento/Asiento"))
            {
                //Fetch the Node values and assign it to Model.
                _db.AccountingSeat.Add(new AccountingSeat
                {
                    AccountSeatNumber = Convert.ToInt16(node["AccountSeatNumber"].InnerText),
                    SeatDescription = node["SeatDescription"].InnerText,
                    SeatDate = Convert.ToDateTime(node["SeatDate"].InnerText),
                    AccountingAccount = Convert.ToDateTime(node["SeatDate"].InnerText),
                    MovementType = Convert.ToInt16(node["SeatDate"].InnerText),
                    MovementAmount = Convert.ToDecimal(node["SeatDate"].InnerText)
                });
                
            }
            _db.SaveChanges();

            return View("PullXmlIndex");
        }

        public ActionResult PushXmlIndex()
        {
            return View();
        }

        public ActionResult PushXml()
        {
            return View("PushXmlIndex");
        }
    }
}