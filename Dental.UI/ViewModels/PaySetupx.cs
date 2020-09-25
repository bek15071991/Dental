using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace Dental.UI.ViewModels
{
    public class PaySetupx : IValidatableObject
    {
        public string CreditCardNumber { get; set; }
        public string ExpDate { get; set; }
        public string CCV { get; set; }
        public decimal PaymentAmount { get; set; }
        public static bool Mod10Check(string creditCardNumber)
        {
            //// check whether input string is null or empty
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            //// 1. Starting with the check digit double the value of every other digit
            //// 2. If doubling of a number results in a two digits number, add up
            /// the digits to get a single digit number. This will results in eight single digit numbers
            //// 3. Get the sum of the digits
            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9').Reverse().Select((e, i) => ((int)e -48) * (i % 2 == 0 ? 1 : 2))
                .Sum((e) => e / 10 + e % 10);

            //// If the final sum is divisible by 10, then the credit card number
            // is valid. If it is not divisible by 10, the number is invalid.
            return sumOfDigits % 10 == 0;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ExpDate.Length != 5)
            {
                yield return new ValidationResult(
                    "Expiration date must match MM/YY",
                    new[] { nameof(ExpDate) });
            }
            if (CCV.Length!=3 || Int32.TryParse(CCV, out int ccv)==false)
            {
                yield return new ValidationResult(
                    "CCV date must match NNN",
                    new[] { nameof(CCV) });
            }
            if (PaymentAmount <= 0)
            {
                yield return new ValidationResult(
                    "You must enter a positive payment amount",
                    new[] { nameof(PaymentAmount) });
            }

            if (Mod10Check(CreditCardNumber) == false)
            {
                yield return new ValidationResult(
                    "You must enter a valid credit card number",
                    new[] { nameof(CreditCardNumber) });
            }
        }
    }
}
