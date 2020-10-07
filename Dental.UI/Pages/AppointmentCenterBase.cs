using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.UIHandlers;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class AppointmentCenterBase : ComponentBase
    {
        [Parameter] public string UserName { get; set; }
        public int ApptCount { get; set; } = 0;
        public bool DisplayAppointments { get; set; } = false;
        [Inject] public IAppointmentDataService AppointmentDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        public List<Appointment> appointments { get; set; } = null;
        protected ApptDialog apptDialog { get; set; }
        public AppointmentUI appointmentUI { get; set; }

        protected override async Task OnInitializedAsync()
        {
            appointmentUI = new AppointmentUI(AppointmentDataService, mapper, UserName);
            appointments = await appointmentUI.GetList();
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
            await appointmentUI.Cancel(apptID);
                appointments = await appointmentUI.GetList();
                ApptCount = appointments.Count();
                if (ApptCount == 0)
                {
                    DisplayAppointments = false;
                }
                StateHasChanged();

        }
        public async void ApptDialog_OnDialogClose()
        {
            appointments = await appointmentUI.GetList();
            ApptCount = appointments.Count();
            StateHasChanged();
        }

        protected void QuickAddAppt()
        {
            apptDialog.Show();
        }

    }
}
