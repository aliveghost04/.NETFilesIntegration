using EasyHttp.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UploadXMLFile.Libs;


namespace UploadXMLFile.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            var fileUpload = Request.Files["FileUpload"];
            var http = new HttpClient();

            byte[] data = new byte[fileUpload.ContentLength];
            fileUpload.InputStream.Read(data, 0, fileUpload.ContentLength);

            var client = new RestClient();
            client.BaseUrl = new Uri("http://localhost:49232");

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "/XMLManager/Pull";
            request.AddFileBytes("FileUpload", data, "FileUpload.xml");

            var result = client.Execute(request);
            if (result.IsSuccessful)
            {
                ViewBag.Success = true;
            } else
            {
                ViewBag.Error = "Ha ocurrido un error, intente nuevamente o contacte con el administrador"; 
            }

            return View("Index");
        }
    }
}