using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Dental.UI.Services;
using Microsoft.AspNetCore.Components;

namespace Dental.UI.Pages
{
    public class ApptDialogBase : ComponentBase
    {
        [Inject] public IScheduleDataService ScheduleDataService { get; set; }
        public bool ShowDialog { get; set; }
        [Parameter]
        public string UserName { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        // selection criteria variables
        public List<string> Years { get; set; } = new List<string>();

        public Schedule Schedule { get; set; } = new Schedule();
        public List<Schedule> Schedules { get; set; }=new List<Schedule>();

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

        protected override void OnInitialized()
        {
            int currYear = DateTime.Now.Year;
            Years.Add(currYear.ToString());
            Years.Add((currYear+1).ToString());

            TimeOfDay.Add("Morning");
            TimeOfDay.Add("Noon");
            TimeOfDay.Add("Afternoon");
            TimeOfDay.Add("No Preference");

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
                .Where(s => s.Date.Year == Year && s.Date.Month == month && s.Date.DayOfWeek == dow && s.Open == true)
                .ToList();
            StateHasChanged();
        }
        protected async Task HandleValidSubmit()
        {

            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
