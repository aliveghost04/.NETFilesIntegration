using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FilesIntegration.Models
{
    public class FilesIntegrationDb : DbContext
    {
        public FilesIntegrationDb(): base("DefaultConnection")
        {
            
        }

        public DbSet<CSVEmployeeData> CsvEmployeeData { get; set; }
        public DbSet<EmployeeAttachment> EmployeeAttachment { get; set; }
        public DbSet<XmlAccountingSeat> AccountingSeat { get; set; }
    }
}