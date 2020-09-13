using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class PaymentCenterBase : ComponentBase
    {
        [Parameter] public string UserName { get; set; }
        public decimal PaymentsDue { get; set; } = 200;
    }
}
