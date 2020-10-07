using Dental.Data.Models;
using Dental.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.UIHandlers
{
    public class DoctorUI
    {
        public DoctorUI(IDoctorDataService doctorDataService)
        {
            _doctorDataService = doctorDataService;
        }

        public IDoctorDataService _doctorDataService { get; }
        public async Task<List<Doctor>> GetList()
        {
            return (await _doctorDataService.GetDoctors()).ToList();
        }
    }
}
