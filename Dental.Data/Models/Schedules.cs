using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ApptId { get; set; }
        public bool Open { get; set; }
    }
}
