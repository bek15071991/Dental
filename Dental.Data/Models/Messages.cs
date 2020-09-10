using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Direction { get; set; }
        public string Message { get; set; }
        public bool Read { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
