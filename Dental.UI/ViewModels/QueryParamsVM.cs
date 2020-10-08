using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.ViewModels
{
    public class QueryParamsVM
    {
        [Required]
        public string YearSelected { get; set; }
        [Required]
        public string MonthSelected { get; set; }
        [Required]
        public string DaySelected { get; set; }
        [Required]
        public string TODSelected { get; set; }
        [Required]
        public string ApptSelected { get; set; }
        public List<string> Years { get; set; }
        public Dictionary<string, int> Months { get; set; }
        public Dictionary<string, DayOfWeek> DayOfWeek { get; set; }
        public List<string> TimeOfDay { get; set; }
    }
}
