using AutoMapper;
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
    public class RegisterFormBase : ComponentBase
    {
        [Inject]
        public ICredentialDataService CredentialDataService { get; set; }
        [Inject]
        public IClientDataService ClientDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        [Parameter]
        public string UserName { get; set; }
        public RegisterVM registerVM { get; set; }
        public RegisterUI registerUI { get; set; }
  
        public string Message { get; set; } = "";

        [Parameter]
        public EventCallback<string> onLogin { get; set; }
        protected override void OnInitialized()
        {
            registerUI = new RegisterUI(ClientDataService, CredentialDataService, mapper);
            registerVM = registerUI.New(UserName);
        }
        // try to login with these credentials
 
        public async void HandleSubmit()
        {
            await registerUI.Register(registerVM);
            await onLogin.InvokeAsync("");
            StateHasChanged();
        }

        public async Task HandleCancel()
        {
            await onLogin.InvokeAsync("");
            StateHasChanged();
        }
    }
}
