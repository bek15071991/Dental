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
        protected ApptDialog apptDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            appointments = (await AppointmentDataService.GetAppointments())
                .Where(a => a.UserName == UserName && a.Date>=DateTime.Now && a.Cancelled == false)
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

        public async void cancelApptHandler(int apptID)
        {
            Appointment appt = appointments.Where(a => a.Id == apptID).FirstOrDefault();
            if (appt != null)
            {
                appt.Cancelled = true;
                await AppointmentDataService.UpdateAppointment(appt);
                appointments = (await AppointmentDataService.GetAppointments())
                    .Where(a => a.UserName == UserName && a.Date >= DateTime.Now && a.Cancelled==false)
                    .ToList();
                ApptCount = appointments.Count();
                if (ApptCount == 0)
                {
                    DisplayAppointments = false;
                }
                StateHasChanged();
            }
        }
        public async void ApptDialog_OnDialogClose()
        {
            appointments = (await AppointmentDataService.GetAppointments())
                .Where(a => a.UserName == UserName && a.Date>=DateTime.Now && a.Cancelled == false)
                .ToList();
            ApptCount = appointments.Count();
            StateHasChanged();
        }

        protected void QuickAddAppt()
        {
            apptDialog.Show();
        }

    }
}
