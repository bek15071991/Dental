using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data.Models
{
    public class PaySetups
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpDate { get; set; }

    }
}
