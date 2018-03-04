using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using FilesIntegration.Models;
using System.Xml;
using System.Xml.Linq;
using System.Net;

namespace FilesIntegration.Controllers
{
    public class XMLManagerController : Controller
    {
        FilesIntegrationDb _db = new FilesIntegrationDb();

        public ActionResult Index() {
            return RedirectToAction("Pull");
        }

        [HttpGet]
        [ActionName("Pull")]
        public ActionResult PullGet()
        {
            return View("PullXml");
        }

        [HttpPost]
        [ActionName("Pull")]
        public ActionResult PullPost()
        {
            var attachedFile = Request.Files["FileUpload"];
            var document = new XmlDocument();
            
            if (attachedFile != null)
            {
                document.Load(attachedFile.InputStream);

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

                try
                {
                    _db.SaveChanges();
                    ViewBag.Success = true;
                    Response.StatusCode = (int) HttpStatusCode.OK;
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                }
            } else
            {
                ViewBag.Error = "No se ha enviado el archivo";
            }

            return View("PullXml");
        }

        [HttpGet]
        [ActionName("Push")]
        public ActionResult PushXmlGet()
        {
            return View("PushXml");
        }

        [HttpPost]
        [ActionName("Push")]
        public void PushXmlPost()
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
                    new XElement("MovementAmount", seat.MovementAmount))
                )
            );
            
            var response = System.Web.HttpContext.Current.Response;

            response.AddHeader("content-disposition", "attachment;filename=Asientos.xml");
            response.ContentType = "application/xml";
            response.Write(accountinSeats.ToString());
            response.End();
        }
    }
}