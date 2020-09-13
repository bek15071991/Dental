using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class AppointmentCenterBase : ComponentBase
    {
        [Parameter] public string UserName { get; set; }
        public int ApptCount { get; set; } = 1;
    }
}
