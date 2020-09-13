using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental.Data.Models
{
    public class Credential
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
