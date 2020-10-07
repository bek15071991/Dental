using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.UIHandlers;
using Dental.UI.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.Pages
{
    public class LoginFormBase : ComponentBase
    {
       [Parameter]
       public EventCallback<string> onRegister { get; set; }
        [Parameter]
        public EventCallback<string> onDashboard { get; set; }
        [Parameter]
        public EventCallback<string> onUserName { get; set; }
        [Inject]
        public ICredentialDataService CredentialDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        public LoginVM loginVM { get; set; }
        public LoginUI loginUI { get; set; }
      
        public string Message { get; set; } = "";


        protected override void OnInitialized()
        {
            loginUI = new LoginUI(CredentialDataService, mapper);
            loginVM = loginUI.New();
        }
        // try to login with these credentials
        public async void HandleSubmit()
        {
            bool loginOK = await loginUI.Login(loginVM);
            if (loginOK==false)
            {
                Message = "Could not login, Try again or Register a new client";
                StateHasChanged();
            }
            else
            {
                Message = "Logged in";
                await onUserName.InvokeAsync(loginVM.UserName);
                await onDashboard.InvokeAsync("");
                StateHasChanged();
            }
        }
        public async Task RegisterPageHandler()
        {
            await onRegister.InvokeAsync("");
            StateHasChanged();
        }
      
    }
}
