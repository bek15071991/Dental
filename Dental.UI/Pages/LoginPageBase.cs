using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class LoginPageBase : ComponentBase
    {
        [Inject]
        public ICredentialDataService CredentialDataService { get; set; }
        [Inject]
        public IClientDataService ClientDataService { get; set; }
        protected Credential cred { get; set; }
        protected Client client { get; set; }
        public string Message { get; set; } = "";

        public bool LoginRequired { get; set; } = true;
        public bool RegisterPage { get; set; } = false;

        protected override void OnInitialized()
        {
            cred = new Credential
            {
                UserName = "jdoe",
                Password = ""
            };
        }
        // try to login with these credentials
        public async void HandleSubmit()
        {
           var credFound = (await CredentialDataService.GetCredentials())
                .FirstOrDefault(c => c.UserName == cred.UserName && c.Password == cred.Password);
           if (credFound == null)
           {
               Message = "Could not login, Try again or Register a new client";
               RegisterPage = false;
           } 
           else
           {
               Message = "Logged in";
                LoginRequired = false;
                RegisterPage = false;
           }
        }

        public void DashboardLogout()
        {
            LoginRequired = true;
        }
        public async void HandleRegisterSubmit()
        {
            await CredentialDataService.AddCredential(cred);
            await ClientDataService.AddClient(client);
            LoginRequired = true;
            RegisterPage = false;
        }

        public void HandleRegisterCancel()
        {
            LoginRequired = true;
            RegisterPage = false;
        }
        public void RegisterPageHandler()
        {
            RegisterPage = true;
            LoginRequired = false;
            client=new Client
            {
                UserName=cred.UserName,
                FirstName = ""
            };
        }
    }
}
