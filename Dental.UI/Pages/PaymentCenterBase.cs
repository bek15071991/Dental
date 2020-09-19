using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class PaymentCenterBase : ComponentBase
    {
        [Parameter] public string UserName { get; set; }
        public decimal PaymentsDue { get; set; } = 0;
        public List<Bill> bills { get; set; } = null;
        public bool DisplayBills { get; set; } = false;
        protected PaySetupDialogx paySetupDialogx { get; set; }
        [Inject] public IBillDataService BillDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bills = (await BillDataService.GetBills())
                .Where(b => b.UserName == UserName && b.Closed == false)
                .ToList();
            PaymentsDue = bills.Sum(b => b.Balance);
        }

        public void DisplayReportHandler()
        {
            DisplayBills = true;
        }

        public async void PaySetupDialogx_OnDialogClose()
        {
            bills = (await BillDataService.GetBills())
                .Where(b => b.UserName == UserName && b.Closed == false)
                .ToList();
            PaymentsDue = bills.Sum(b => b.Balance);
            StateHasChanged();
        }

        protected void QuickAddPayment()
        {
            paySetupDialogx.Show();
        }
    }
}
