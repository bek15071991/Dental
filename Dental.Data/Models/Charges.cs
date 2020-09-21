using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data.Models
{
    public class Charge
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
