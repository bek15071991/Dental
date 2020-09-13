using System.ComponentModel.DataAnnotations;

namespace Dental.Data.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
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
