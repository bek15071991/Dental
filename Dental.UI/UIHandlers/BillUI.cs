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
    public class BillUI
    {
        public BillUI(IBillDataService billDataService, IMapper mapper, string userName)
        {
            _billDataService = billDataService;
            _mapper = mapper;
            _userName = userName;
        }

        public IBillDataService _billDataService { get; }
        public IMapper _mapper { get; }
        public string _userName { get; }
        public async Task<List<Bill>> GetList()
        {
            return (await _billDataService.GetBills())
         .Where(b => b.UserName == _userName && b.Closed == false)
         .ToList();
        }
        public async Task<List<BillVM>> GetListVM(List<Appointment> appts, RegisterUI registerUI)
        {
            var billVMs = new List<BillVM>();
            foreach (var appt in appts)
            {
                // get client name
                var UserName = appt.UserName;
                var reqisterVM = await registerUI.GetClient(UserName);
                // create billvm 
                var billVM = _mapper.Map<BillVM>(appt);
                // construct final vm
                billVM.Name = reqisterVM.FirstName + " " + reqisterVM.LastName;
                billVM.ProcedureCode = "";
                // add to list
                billVMs.Add(billVM);
            }
            // return List of view models
            return billVMs;
        }
        public async Task PostProcedures(List<BillVM> billVms, ProcedureUI procedureUI)
        {
            foreach (var billVM in billVms)
            {
                var bill = _mapper.Map<Bill>(billVM);
                Procedure procedure = await procedureUI.GetFromCode(billVM.ProcedureCode);
                if (procedure==null)
                {
                    throw new InvalidOperationException("Illegal Procedure Lookup");
                }
                bill.Procedure = billVM.ProcedureCode;
                bill.Insurance = procedure.Insurance;
                bill.Charge = procedure.Cost;
                bill.Balance = bill.Charge - bill.Insurance;
                bill.Closed = false;
                bill.Date = DateTime.Now;

                await Add(bill);
            }
          
        }
        public async Task Add(Bill bill)
        {
            await _billDataService.AddBill(bill);
        }
    }
}
