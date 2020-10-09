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

        public class Register
        {
            public Credential credential { get; set; }
            public Client client { get; set; }
        }

        public Register register { get; set; } = new Register
        {
            credential = new Credential(),
            client = new Client()

        };
            public string Message { get; set; } = "";

        public bool LoginRequired { get; set; } = true;
        public bool RegisterPage { get; set; } = false;

        protected override void OnInitialized()
        {
            cred = new Credential
            {
                UserName = "",
                Password = ""
            };
            // sample data
            register.credential.UserName = "jdoe";
            register.credential.Password = "test";
        }
        // try to login with these credentials
        public async void HandleSubmit()
        {
           var credFound = (await CredentialDataService.GetCredentials())
                .FirstOrDefault(c => c.UserName == register.credential.UserName && c.Password == register.credential.Password);
           if (credFound == null)
           {
               Message = "Could not login, Try again or Register a new client";
               RegisterPage = false;
               StateHasChanged();
           } 
           else
           {
               Message = "Logged in";
                LoginRequired = false;
                RegisterPage = false;
                StateHasChanged();
           }
        }

        public void DashboardLogout()
        {
            LoginRequired = true;
            StateHasChanged();
        }
        public async void HandleRegisterSubmit()
        {
            client = register.client;
            cred = register.credential;
            client.UserName = cred.UserName;
            await CredentialDataService.AddCredential(cred);
            await ClientDataService.AddClient(client);
            LoginRequired = true;
            RegisterPage = false;
            StateHasChanged();
        }

        public void HandleRegisterCancel()
        {
            LoginRequired = true;
            RegisterPage = false;
            var register = new Register
            {
                credential = cred,
                client = new Client()
            };
            StateHasChanged();
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
            StateHasChanged();
        }
    }
}
