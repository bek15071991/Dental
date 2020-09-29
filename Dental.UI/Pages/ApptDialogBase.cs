using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
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
        public List<string> Years { get; set; } = new List<string>();

        public Schedule Schedule { get; set; } = new Schedule();
        public List<Schedule> Schedules { get; set; }=new List<Schedule>();
        public string DoctorName { get; set; }
        public List<Doctor> Doctors { get; set; }=new List<Doctor>();

        public Dictionary<string, int> Months { get; set; } = new Dictionary<string, int>
        {
            {"January", 1},
            {"February", 2},
            {"March", 3},
            {"April", 4},
            {"May", 5},
            {"June", 6},
            {"July", 7},
            {"August", 8},
            {"September", 9},
            {"October", 10},
            {"November", 11},
            {"December", 12}
        };

        public Dictionary<string, DayOfWeek> DayOfWeek { get; set; } = new Dictionary<string, DayOfWeek>
        {
            {"Monday", System.DayOfWeek.Monday},
            {"Tuesday", System.DayOfWeek.Tuesday},
            {"Wednesday", System.DayOfWeek.Wednesday},
            {"Thursday", System.DayOfWeek.Thursday}
        };

        public List<string> TimeOfDay { get; set; } = new List<string>();

        public string MonthSelected { get; set; } = new string("");

        public string DaySelected { get; set; }=new string("");
        public string YearSelected { get; set; } = new string("");
        public string TODSelected { get; set; }=new String("");
        public string ApptSelected { get; set; }=new string("");

        protected override async Task OnInitializedAsync()
        {
            int currYear = DateTime.Now.Year;
            Years.Add(currYear.ToString());
            Years.Add((currYear+1).ToString());

            TimeOfDay.Add("Morning");
            TimeOfDay.Add("Noon");
            TimeOfDay.Add("Afternoon");
            TimeOfDay.Add("No Preference");

            Doctors = (await DoctorDataService.GetDoctors()).ToList();

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
            int Year;
            Int32.TryParse(YearSelected, out Year);
            int month = Months[MonthSelected];
            System.DayOfWeek dow = DayOfWeek[DaySelected];
            Schedules = (await ScheduleDataService.GetSchedules())
                .Where(s => s.Date.Year == Year && s.Date.Month == month && s.Date.DayOfWeek == dow && s.Date>=DateTime.Now && s.Open == true)
                .ToList();
            int lowerHour = 8;
            int upperHour = 16;
            if (TODSelected == "Morning")
            {
                lowerHour = 8;
                upperHour = 11;
            } else if (TODSelected=="Noon")

            {
                lowerHour = 11;
                upperHour = 13;
            } else if (TODSelected == "Afternoon")
            {
                lowerHour = 13;
                upperHour = 16;
            }
            else
            {
                lowerHour = 8;
                upperHour = 16;
            }

            Schedules = Schedules.Where(s => s.Date.Hour >= lowerHour && s.Date.Hour <= upperHour).ToList();
            ShowDialog = true;
            StateHasChanged();
        }
        protected async Task HandleValidSubmit()
        {

            ShowDialog = false;
            // get date selected
            int apptId=Int32.Parse(ApptSelected);
            Schedule schedule = Schedules.Where(s => s.Id == apptId).FirstOrDefault();
            DateTime date;
            if (schedule == null)
            {
                date=DateTime.Now;
            }
            else
            {
                date = schedule.Date;
            }
            // add apointment to customers database
            Appointment appointment = new Appointment
            {
                UserName = UserName,
                Date = date,
                Duration = 30,
                Cancelled = false
            };
            await AppointmentDataService.AddAppointment(appointment);
            var appt = (await AppointmentDataService.GetAppointments())
                .Where(a => a.UserName == UserName && a.Date == date)
                .FirstOrDefault();
            if (appt == null)
            {
                apptId = 0;
            }
            else
            {
                apptId = appt.Id;
            }

            if (schedule != null)
            {
                schedule.Open = false;
                schedule.ApptId = apptId;

                await ScheduleDataService.UpdateSchedule(schedule);
            }

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
