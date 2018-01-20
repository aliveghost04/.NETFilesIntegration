﻿using System;
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
    }
}