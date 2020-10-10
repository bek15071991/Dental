using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.ViewModels
{
    public class BillVM
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string DoctorName { get; set; }
        [Required]
        public string ProcedureCode { get; set; }
        public string UserName { get; set; }
    }
}
