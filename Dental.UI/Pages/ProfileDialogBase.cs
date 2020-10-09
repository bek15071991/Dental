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
    public class ProfileDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }
        [Parameter]
        public string UserName { get; set; }

      
        [Inject]
        public IMapper mapper { get; set; }
        [Inject]
        public IClientDataService clientDataService { get; set; }
        [Inject]
        public ICredentialDataService credentialDataService { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public RegisterVM registerVM { get; set; }
        public RegisterUI registerUI { get; set; }
        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
        }
        private async void ResetDialog()
        {

            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            registerVM = new RegisterVM();
            registerUI = new RegisterUI(clientDataService, credentialDataService, mapper);
            registerVM = await registerUI.GetClient(UserName);
            ResetDialog();
        }

        public void Close()
        {
            ShowDialog = false;
        }

        protected async Task HandleValidSubmit()
        {
            await registerUI.UpdateClient(registerVM, UserName);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
