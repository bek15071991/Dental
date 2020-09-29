using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.ViewModels
{
    public class RegisterVM : IValidatableObject
    {
        [Required]
        public string UserName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        [PasswordPropertyText]
        public string Password2 { get; set; }
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

        public string Insurance { get; set; }
        public string PolicyNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
    
            if (Password.Length < 10)
            {
                yield return new ValidationResult(
                    "Password must be at least 10 characters",
                    new[] { nameof(Password) });
            }

            if (Password != Password2)
            {
                yield return new ValidationResult(
                    "Passwords must agree",
                    new[] { nameof(Password), nameof(Password2) });
            }
        }
    }
}
