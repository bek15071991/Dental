using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public bool Cancelled { get; set; }
        public string DoctorName { get; set; }

    }
}
