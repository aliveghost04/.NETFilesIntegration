using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilesIntegration.Models
{
    public class AccountingSeat
    {
        public int Id { get; set; }
        public int AccountSeatNumber { get; set; }
        public string SeatDescription { get; set; }
        public DateTime SeatDate { get; set; }
        public DateTime AccountingAccount { get; set; }
        public int MovementType { get; set; }
        public decimal MovementAmount { get; set; }
    }
}