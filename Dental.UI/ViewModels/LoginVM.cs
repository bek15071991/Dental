using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.ViewModels
{
    public class LoginVM : IValidatableObject
    {
        public string UserName { get; set; }
        [PasswordPropertyText] public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Length < 4)
            {
                yield return new ValidationResult(
                    "User name must be at least 10 characters",
                    new[] {nameof(UserName)});
            }

            if (Password.Length < 4)
            {
                yield return new ValidationResult(
                    "Password must be at least 10 characters",
                    new[] {nameof(Password)});
            }
        }
    }
}
