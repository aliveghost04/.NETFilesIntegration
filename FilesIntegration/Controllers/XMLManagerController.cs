using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using FilesIntegration.Models;
using System.Xml;
using System.Xml.Linq;

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

            if (attachedFile != null) document.Load(attachedFile.InputStream);
            //document.Load(Server.MapPath("~/Files/Xml/Asiento.xml"));


            //Loop through the selected Nodes.
            foreach (XmlNode node in document.SelectNodes("/AccountingSeat/Seat"))
            {
                var parsedSeatDate = DateTime.ParseExact(node["SeatDate"].InnerText, "dd/MM/yyyy", null);

                var accountingSeat = new XmlAccountingSeat
                {
                    AccountSeatNumber = Convert.ToInt32(node["AccountSeatNumber"].InnerText),
                    SeatDescription = node["SeatDescription"].InnerText,
                    SeatDate = parsedSeatDate,
                    AccountingAccount = Convert.ToInt32(node["AccountingAccount"].InnerText),
                    MovementType = Convert.ToInt32(node["MovementType"].InnerText),
                    MovementAmount = Convert.ToDecimal(node["MovementAmount"].InnerText)
                };
                //Fetch the Node values and assign it to Model.
                _db.AccountingSeat.Add(accountingSeat);

            }
            _db.SaveChanges();

            return View("PullXmlIndex");
        }

        public ActionResult PushXmlIndex()
        {
            return View();
        }

        public void PushXml()
        {

            var accountinSeats = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                new XElement("AccountingSeats",
                from seat in _db.AccountingSeat.ToList()
                select new XElement("Seats",
                    new XElement("AccountSeatNumber", seat.AccountSeatNumber),
                    new XElement("SeatDescription", seat.SeatDescription),
                    new XElement("SeatDate", seat.SeatDate),
                    new XElement("AccountingAccount", seat.AccountingAccount),
                    new XElement("MovementType", seat.MovementType),
                    new XElement("MovementAmount", seat.MovementAmount))));
            //accountinSeats.Save(Server.MapPath(@"~/Files/Xml")
            var response = System.Web.HttpContext.Current.Response;
            response.AddHeader("content-disposition", "attachment;filename=Asiento.xml");
            response.ContentType = "text/plain";
            response.Write(accountinSeats.ToString());
            response.End();
            //return View("PushXmlIndex", accountinSeats);
        }
    }
}