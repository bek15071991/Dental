using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class PaySetupDialogBasex : ComponentBase
    {
        public bool ShowDialog { get; set; }
        [Parameter]
        public string UserName { get; set; }

        [Inject] public IPaySetupDataService PaySetupDataService { get; set; }
        public List<PaySetup> PaySetups { get; set; } = null;
        public PaySetup PaySetupx { get; set; } = new PaySetup();

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
        }

        protected override void OnInitialized()
        {
            ResetDialog();
        }
        private async void ResetDialog()
        {
            PaySetups = (await PaySetupDataService.GetPaySetups())
                .Where(p => p.Username == UserName)
                .ToList();
            PaySetupx = PaySetups.FirstOrDefault();
            if (PaySetupx == null)
            {
                PaySetupx = new PaySetup
                {
                    CreditCardNumber = "",
                    ExpDate = "",
                };
            }
        }

        public void Close()
        {
            ShowDialog = false;
        }

        protected async Task HandleValidSubmit()
        {
            await PaySetupDataService.UpdatePaySetup(PaySetupx);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
