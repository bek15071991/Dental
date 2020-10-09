using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.ViewModels
{
    public class ClientVM 
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string HomePhone { get; set; }
        [Phone]
        public string MobilePhone { get; set; }
    }
}
