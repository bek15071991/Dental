using Dental.Data.Models;
using Dental.UI.Services;
using Dental.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.UIHandlers
{
    public class ScheduleUI
    {
        public ScheduleUI(IScheduleDataService scheduleDataService,
            IAppointmentDataService appointmentDataService)
        {
            _scheduleDataService = scheduleDataService;
            _appointmentDataService = appointmentDataService;

            int currYear = DateTime.Now.Year;
            Years.Add(currYear.ToString());
            Years.Add((currYear + 1).ToString());

            TimeOfDay.Add("Morning");
            TimeOfDay.Add("Noon");
            TimeOfDay.Add("Afternoon");
            TimeOfDay.Add("No Preference");
        }

        public IScheduleDataService _scheduleDataService { get; }
        public IAppointmentDataService _appointmentDataService { get; }
        public List<string> Years { get; set; } = new List<string>();
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
        public async Task<List<Schedule>> ProcessQuery(QueryParamsVM queryParamsVM)
        {
            int Year;
            Int32.TryParse(queryParamsVM.YearSelected, out Year);
            int month = Months[queryParamsVM.MonthSelected];
            System.DayOfWeek dow = DayOfWeek[queryParamsVM.DaySelected];
            var Schedules = (await _scheduleDataService.GetSchedules())
                .Where(s => s.Date.Year == Year && s.Date.Month == month && s.Date.DayOfWeek == dow && s.Date >= DateTime.Now && s.Open == true)
                .ToList();
            int lowerHour = 8;
            int upperHour = 16;
            if (queryParamsVM.TODSelected == "Morning")
            {
                lowerHour = 8;
                upperHour = 11;
            }
            else if (queryParamsVM.TODSelected == "Noon")

            {
                lowerHour = 11;
                upperHour = 13;
            }
            else if (queryParamsVM.TODSelected == "Afternoon")
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
            return Schedules;
        }
        public async Task MakeAppointment(QueryParamsVM queryParamsVM, List<Schedule> schedules, string userName)
        {
            // get date selected
            int apptId = Int32.Parse(queryParamsVM.ApptSelected);
            Schedule schedule = schedules.Where(s => s.Id == apptId).FirstOrDefault();
            DateTime date;
            if (schedule == null)
            {
                date = DateTime.Now;
            }
            else
            {
                date = schedule.Date;
            }
            // add apointment to customers database
            Appointment appointment = new Appointment
            {
                UserName = userName,
                Date = date,
                Duration = 30,
                Cancelled = false
            };
            await _appointmentDataService.AddAppointment(appointment);
            var appt = (await _appointmentDataService.GetAppointments())
                .Where(a => a.UserName == userName && a.Date == date)
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

                await _scheduleDataService.UpdateSchedule(schedule);
            }

        }
        public QueryParamsVM New()
        {
            var queryParamsVM = new QueryParamsVM();

            queryParamsVM.Years = Years;
            queryParamsVM.Months = Months;
            queryParamsVM.TimeOfDay = TimeOfDay;
            queryParamsVM.DayOfWeek = DayOfWeek;
            queryParamsVM.ApptSelected = "";
            queryParamsVM.DaySelected = "";
            queryParamsVM.MonthSelected = "";
            queryParamsVM.TODSelected = "";
            queryParamsVM.YearSelected = "";

            return queryParamsVM;
        }
    }
}
