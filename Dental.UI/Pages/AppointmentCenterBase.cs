using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class AppointmentCenterBase : ComponentBase
    {
        [Parameter] public string UserName { get; set; }
        public int ApptCount { get; set; } = 0;
        public bool DisplayAppointments { get; set; } = false;
        [Inject] public IAppointmentDataService AppointmentDataService { get; set; }
        public List<Appointment> appointments { get; set; } = null;
        

        protected override async Task OnInitializedAsync()
        {
            appointments = (await AppointmentDataService.GetAppointments())
                .Where(a => a.UserName == UserName)
                .ToList();
            ApptCount = appointments.Count();
        }

        public void DisplayReportHandler()
        {
            DisplayAppointments = true;
        }

        public void CloseAppointmentsHandler()
        {
            DisplayAppointments = false;
        }
    
}
}
