using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilesIntegration.Models
{
    public class EmployeeAttachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DominicanIdentification { get; set; }
        public string InstitutionalEmployeeCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string InstitutionCode { get; set; }
        public decimal Salary { get; set; }
        public decimal Discounts { get; set; }

    }
}