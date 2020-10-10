using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.UIHandlers
{
    public class AppointmentUI
    {
        public AppointmentUI(IAppointmentDataService appointmentDataService,
            IMapper mapper,
            string userName)
        {
            _appointmentDataService = appointmentDataService;
            _mapper = mapper;
            _userName = userName;
        }

        public IAppointmentDataService _appointmentDataService { get; }
        public IMapper _mapper { get; }
        public string _userName { get; }
        public async Task<List<Appointment>> GetList()
        {
            return (await _appointmentDataService.GetAppointments())
                .Where(a => a.UserName == _userName && a.Date >= DateTime.Now && a.Cancelled == false)
                .ToList();
        }
        public async Task<List<Appointment>> GetPage()
        {
            return (await _appointmentDataService.GetAppointments())
                 .Where(a => a.Date >= DateTime.Now && a.Cancelled == false && a.DoctorName != null)
                 .Take(10)
                 .ToList();
        }
        public async Task Cancel(int apptID)
        {
            var appointments = await GetList();
            Appointment appt = appointments.Where(a => a.Id == apptID).FirstOrDefault();
            if (appt != null)
            {
                appt.Cancelled = true;
                await _appointmentDataService.UpdateAppointment(appt);
            }
        }
    }
}
