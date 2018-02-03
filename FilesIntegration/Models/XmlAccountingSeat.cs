using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilesIntegration.Models
{
    public class XmlAccountingSeat
    {
        public int Id { get; set; }
        public int AccountSeatNumber { get; set; }
        public string SeatDescription { get; set; }
        public DateTime SeatDate { get; set; }
        public int AccountingAccount { get; set; }
        public int MovementType { get; set; }
        public decimal MovementAmount { get; set; }
    }
}