using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.UIHandlers
{
    public class PaymentUI
    {
        public PaymentUI(IPaySetupDataService paySetupDataService,
            IChargeDataService chargeDataService,
            IBillDataService billDataService,
            IMapper mapper,
            string userName)
        {
            _paySetupDataService = paySetupDataService;
            _chargeDataService = chargeDataService;
            _billDataService = billDataService;
            _mapper = mapper;
            _userName = userName;
        }

        public IPaySetupDataService _paySetupDataService { get; }
        public IChargeDataService _chargeDataService { get; }
        public IBillDataService _billDataService { get; }
        public IMapper _mapper { get; }
        public string _userName { get; }
        public PaySetup PaySetup { get; set; }
        public async Task<ViewModels.PaySetupx> GetPaymentModel()
        {
            var PaySetups = (await _paySetupDataService.GetPaySetups())
               .Where(p => p.Username == _userName)
               .ToList();
            PaySetup = PaySetups.FirstOrDefault();
            if (PaySetup == null)
            {
                PaySetup = new PaySetup
                {
                    Id = 0,
                    Username = _userName,
                    CreditCardNumber = "",
                    ExpDate = ""
                };
            }
            var PaySetupx=_mapper.Map<PaySetupx>(PaySetup);
            List<Bill> bills = (await _billDataService.GetBills())
               .Where(b => b.UserName == _userName && b.Balance > 0)
               .ToList();
            PaySetupx.PaymentAmount = bills.Sum(b => b.Balance);
            return PaySetupx;
        }
        public async Task UpdatePaymentInfo(PaySetupx paySetupx)
        {
            PaySetup.CreditCardNumber = paySetupx.CreditCardNumber;
            PaySetup.ExpDate = paySetupx.ExpDate;


            await _paySetupDataService.UpdatePaySetup(PaySetup);
        }
        public async Task AddCharge(PaySetupx paySetupx)
        {
            var Charge = new Charge
            {
                UserName = _userName,
                Date = DateTime.Now,
                CreditCardNumber = paySetupx.CreditCardNumber,
                ExpDate = paySetupx.ExpDate,
                PaymentAmount = paySetupx.PaymentAmount
            };
            await _chargeDataService.AddCharge(Charge);
        }
        public async Task ApplyPaymentToBills(PaySetupx paySetupx)
        {
            List<Bill> bills = (await _billDataService.GetBills())
               .Where(b => b.UserName == _userName && b.Balance > 0)
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

                await _billDataService.UpdateBill(Bill);
            }
        }
        public async Task ApplyPayment(PaySetupx paySetupx)
        {
            // update credit card info on file
            await UpdatePaymentInfo(paySetupx);

            // create charge record
            await AddCharge(paySetupx);
            // update open balance on bills
            await ApplyPaymentToBills(paySetupx);
        }
    }
}

