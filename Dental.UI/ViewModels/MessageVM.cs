using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.ViewModels
{
    public class MessageVM : IValidatableObject
    {
        public string Direction { get; set; }
        public string MessageText { get; set; }
        public string DoctorName { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MessageText.Length <20)
            {
                yield return new ValidationResult(
                    "Missing message text",
                    new[] {nameof(MessageText)});
            }
            if (Direction!="In" && Direction!="Out")
            {
                yield return new ValidationResult(
                    "Direction must be In or Out",
                    new[] { nameof(Direction) });
            }
        }
    }
}
