using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Antlr.Runtime;
using FilesIntegration.Models;
using FilesIntegration.Utils;

namespace FilesIntegration.Controllers
{
    public class CSVManagerController : Controller
    {
        // GET: CSVManager
        FilesIntegrationDb _db = new FilesIntegrationDb();

        public ActionResult PullCsvIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PullCsv()
        {
            var attachedFile = Request.Files["FileUpload"];
            var values = new List<string>();
            if (attachedFile != null)
            {
                values = CsvReader.GetListFromStream(attachedFile.InputStream, 1);
            }
            var operationMessage = string.Empty;

            foreach (var value in values)
            {
                if (!value.Contains(","))
                {
                    operationMessage = "There's a comma inside of your data, remember this is a CSV fila and it should have commas inside of the data my dear friend :)";
                    continue;
                }

                var currentRowValues = value.Split(',');
                var employeeName = currentRowValues[0];
                var employeeLastName = currentRowValues[1];
                var employeeInstitutionalCode = currentRowValues[2];
                var fromDate = currentRowValues[3];
                var toDate = currentRowValues[4];
                var institutionCode = currentRowValues[5];
                var salary = currentRowValues[6];
                var discounts = currentRowValues[7];

                if (!string.IsNullOrWhiteSpace(employeeName) && !string.IsNullOrWhiteSpace(employeeLastName)
                    && !string.IsNullOrWhiteSpace(employeeInstitutionalCode) && !string.IsNullOrWhiteSpace(fromDate)
                    && !string.IsNullOrWhiteSpace(toDate) && !string.IsNullOrWhiteSpace(institutionCode)
                    && !string.IsNullOrWhiteSpace(salary) && !string.IsNullOrWhiteSpace(discounts))
                {
                    var parsedFromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                    var parsedToDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                    var parsedSalary = Convert.ToDecimal(salary);
                    var parsedDiscounts = Convert.ToDecimal(discounts);

                    var employee = new EmployeeAttachment
                    {
                        Name = employeeName,
                        LastName = employeeLastName,
                        InstitutionalEmployeeCode = employeeInstitutionalCode,
                        FromDate =  parsedFromDate,
                        ToDate = parsedToDate,
                        InstitutionCode = institutionCode,
                        Salary = parsedSalary,
                        Discounts = parsedDiscounts
                    };
                    _db.EmployeeAttachment.Add(employee);
                    operationMessage = "Everything was great :D!!!";
                }
                else
                {
                    operationMessage = "There are null or empty rows in your .csv file my dear friend :)";
                }
            }
            _db.SaveChanges();
            ViewBag.Message = operationMessage;

            return View("PullCsvIndex");
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