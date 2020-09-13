using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class MessageCenterBase : ComponentBase
    {
        [Parameter] public string UserName { get; set; }
        public int MessageCount { get; set; } = 5;
    }
}
