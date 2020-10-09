using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Procedure { get; set; }
        public decimal Charge { get; set; }
        public decimal Insurance { get; set; }
        public decimal Balance { get; set; }
        public bool Closed { get; set; }
        public string DoctorName { get; set; }
    }
}
