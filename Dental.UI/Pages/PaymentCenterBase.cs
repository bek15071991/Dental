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
        [Inject] public IProcedureDataService ProcedureDataService { get; set; }
        public Dictionary<string, string> Procedures { get; set; }=new Dictionary<string, string>();

        protected override async Task OnInitializedAsync()
        {
            bills = (await BillDataService.GetBills())
                .Where(b => b.UserName == UserName && b.Closed == false)
                .ToList();
            PaymentsDue = bills.Sum(b => b.Balance);
            // create dictionary to map procedure codes
            var procedures = (await ProcedureDataService.GetProcedures()).ToList();
            foreach (var procedure in procedures)
            {
                Procedures.Add(procedure.Code, procedure.Description);
            }
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
