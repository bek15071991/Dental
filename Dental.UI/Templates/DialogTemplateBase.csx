using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class DialogTemplateBase : ComponentBase
    {
        public bool ShowDialog { get; set; }
        [Parameter]
        public string UserName { get; set; }
        
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
        }

        private void ResetDialog()
        {
  
        }

        public void Close()
        {
            ShowDialog = false;
        }

        protected async Task HandleValidSubmit()
        {
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
