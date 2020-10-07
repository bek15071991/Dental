using AutoMapper;
using Dental.UI.Services;
using Dental.UI.UIHandlers;
using Dental.UI.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.Pages
{
    public class ContainerBase : ComponentBase
    {
        [Inject]
        public ICredentialDataService CredentialDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        public string UserName { get; set; } = new string("");
        public bool LoginRequired { get; set; }
        public bool RegisterPage { get; set; }
        public LoginVM loginVM { get; set; }
        public LoginUI loginUI { get; set; }

        protected override void OnInitialized()
        {
            loginUI = new LoginUI(CredentialDataService, mapper);
            LoginRequired = true;
            RegisterPage = false;
        }
        public async Task DashboardLogout()
        {
            await loginUI.Logout();
            LoginRequired = true;
            StateHasChanged();
        }
        public void LoginHandler(string x)
        {
     
                LoginRequired = true;
                RegisterPage = false;
        }
        public void RegisterHandler(string x)
        {
                LoginRequired = false;
                RegisterPage = true;
        }
        public void DashboardHandler(string x)
        {
            LoginRequired = false;
            RegisterPage = false;
        }
        public void UserNameHandler(string userName)
        {
            UserName = userName;
        }
    }
}
