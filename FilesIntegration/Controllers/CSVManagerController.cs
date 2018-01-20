using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FilesIntegration.Models;

namespace FilesIntegration.Controllers
{
    public class CSVManagerController : Controller
    {
        // GET: CSVManager
        FilesIntegrationDb _db = new FilesIntegrationDb();


        public ActionResult PullCsv()
        {
            return View();
        }

        public ActionResult PushCsvIndex()
        {
            return View();
        }

        public void PushCsv()
        {
            var stringBuilder = new StringBuilder();
            var employeeData = _db.CsvEmployeeData.ToList();

            stringBuilder.AppendFormat("{0},{1},{3},{4},{5},{6},{7},{8}",
                "Name","LastName","Dominican Identification","Intitutional Employee Code","From Date","To Date","Institution Code","Salary","Discounts",Environment.NewLine);
            stringBuilder.AppendLine();

            foreach (var employee in employeeData)
            {
                stringBuilder.AppendFormat("{0},{1},{3},{4},{5},{6},{7},{8}", employee.Name,employee.LastName,employee.DominicanIdentification,employee.InstitutionalEmployeeCode,employee.FromDate,employee.ToDate,employee.InstitutionCode,employee.Salary,employee.Discounts, Environment.NewLine);
                stringBuilder.AppendLine();
            }

            var response = System.Web.HttpContext.Current.Response;
            response.BufferOutput = true;
            response.Clear();
            response.ClearHeaders();
            response.ContentEncoding = Encoding.Unicode;
            response.AddHeader("content-disposition", "attachment;filename=Employee.csv");
            response.ContentType = "text/plain";
            response.Write(stringBuilder.ToString());
            response.End();

        }
    }
}