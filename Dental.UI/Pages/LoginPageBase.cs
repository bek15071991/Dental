using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dental.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class LoginPageBase : ComponentBase
    {
        public Credential cred { get; set; }
        public string user { get; set; } = "";

        protected override void OnInitialized()
        {
            cred = new Credential
            {
                UserName = "cfloryiv",
                Password = ""
            };
        }
    }
}
