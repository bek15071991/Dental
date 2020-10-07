using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
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
    }
}
