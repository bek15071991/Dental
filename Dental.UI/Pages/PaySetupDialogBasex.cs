using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
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
        public List<PaySetup> PaySetups { get; set; } = null;
        public PaySetup PaySetup { get; set; } = new PaySetup();

        //public class PaySetupx
        //{
        //    public string CreditCardNumber { get; set; }
        //    public string ExpDate { get; set; }
        //    public decimal PaymentAmount { get; set; }
        //    public string CCV { get; set; }
        //}

        public PaySetupx paySetupx { get; set; } = new PaySetupx();

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
            PaySetup = PaySetups.FirstOrDefault();
            if (PaySetup == null)
            {
                PaySetup = new PaySetup
                {
                    Id=0,
                    Username = UserName,
                    CreditCardNumber = "",
                    ExpDate = ""
                };
            }

            paySetupx = new PaySetupx
                {
                    CreditCardNumber = PaySetup.CreditCardNumber,
                    ExpDate = PaySetup.ExpDate,
                    CCV="",
                    PaymentAmount = PayAmount,
                };
            }

        public void Close()
        {
            ShowDialog = false;
        }

        protected async Task HandleValidSubmit()

        { 
            // update credit card infoon file

            PaySetup.CreditCardNumber = paySetupx.CreditCardNumber;
            PaySetup.ExpDate = paySetupx.ExpDate;
            
            
            await PaySetupDataService.UpdatePaySetup(PaySetup);
            // create charge record
            var Charge = new Charge
            {
                UserName = UserName,
                Date = DateTime.Now,
                CreditCardNumber = paySetupx.CreditCardNumber,
                ExpDate = paySetupx.ExpDate,
                PaymentAmount = paySetupx.PaymentAmount
            };
            await ChargeDataService.AddCharge(Charge);
            // update open balance on bills
            List<Bill> bills = (await BillDataService.GetBills())
                .Where(b => b.UserName == UserName && b.Balance > 0)
                .ToList();
            decimal amount = paySetupx.PaymentAmount;
            foreach (var bill in bills)
            {
                decimal balance = bill.Balance;
                Bill Bill = new Bill();
                Bill = bill;
                if (amount >= balance)
                {
                    amount -= balance;
                    Bill.Balance = 0;
                    Bill.Closed = true;
                }
                else
                {
                    Bill.Balance -= amount;
                    amount = 0;
                }

                await BillDataService.UpdateBill(Bill);
            }
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
