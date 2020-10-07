using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.UIHandlers;
using Dental.UI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class PaySetupDialogBasex : ComponentBase
    {
        public bool ShowDialog { get; set; }
        [Parameter]
        public string UserName { get; set; }
        [Parameter]
        public decimal PayAmount { get; set; }

        [Inject] public IPaySetupDataService PaySetupDataService { get; set; }

        [Inject]
        public IChargeDataService ChargeDataService { get; set; }
        [Inject]
        public IBillDataService BillDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }

        public PaySetupx paySetupx { get; set; } = new PaySetupx();
        public PaymentUI paymentUI { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
        }

        protected override void OnInitialized()
        {
            paymentUI = new PaymentUI(PaySetupDataService, ChargeDataService, BillDataService,
                mapper, UserName);
            ResetDialog();
        }
        private async void ResetDialog()
        {
            paySetupx = await paymentUI.GetPaymentModel();
        }

        public void Close()
        {
            ShowDialog = false;
        }

        protected async Task HandleValidSubmit()

        {
            await paymentUI.ApplyPayment(paySetupx);
          
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
