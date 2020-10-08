using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.UIHandlers;
using Dental.UI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace Dental.UI.Pages
{
    public class ApptDialogBase : ComponentBase
    {
        [Inject] public IScheduleDataService ScheduleDataService { get; set; }
        [Inject] public IAppointmentDataService AppointmentDataService { get; set; }
        [Inject] public IDoctorDataService DoctorDataService { get; set; }
        public bool ShowDialog { get; set; }
        [Parameter]
        public string UserName { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        // selection criteria variables

        public List<Schedule> Schedules { get; set; }=new List<Schedule>();
        public string DoctorName { get; set; }
        public List<Doctor> Doctors { get; set; }=new List<Doctor>();

        public QueryParamsVM queryParamsVM { get; set; }
        public string ApptSelected { get; set; }=new string("");
        public ScheduleUI scheduleUI { get; set; }
        public DoctorUI doctorUI { get; set; }
        protected override async Task OnInitializedAsync()
        {
            scheduleUI = new ScheduleUI(ScheduleDataService, AppointmentDataService);
            doctorUI = new DoctorUI(DoctorDataService);
            queryParamsVM = scheduleUI.New();
            Doctors = await doctorUI.GetList();

            ResetDialog();
        }

        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
        }

        private void ResetDialog()
        {

        }

        public void Close()
        {
            ShowDialog = false;
        }

        public async void ProcessSelectHandler()
        {
            Schedules=await scheduleUI.ProcessQuery(queryParamsVM);
            ShowDialog = true;
            StateHasChanged();
        }
        protected async Task HandleValidSubmit()
        {

            ShowDialog = false;
            await scheduleUI.MakeAppointment(queryParamsVM, Schedules, UserName);
          
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
