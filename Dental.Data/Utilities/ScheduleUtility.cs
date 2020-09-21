using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Data;
using Dental.Data.Models;

namespace Dental.Data.Utilities
{
    public class ScheduleUtility
    {
        private readonly ApplicationDbContext _context;

        public ScheduleUtility(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateSchedule(DateTime D1, DateTime D2)
        {
            for (DateTime D = D1; D <= D2; D = D.AddDays((1)))
            {
                if (D.DayOfWeek == DayOfWeek.Sunday) continue;
                if (D.DayOfWeek == DayOfWeek.Friday) continue;
                if (D.DayOfWeek == DayOfWeek.Saturday) continue;

                DateTime T = D.Date; // set time to midnight

                T=T.AddHours(8.0); // set time to 8am
                for (int ii = 0; ii < 16; ii++)
                {
                    Schedule S = new Schedule
                    {
                        Date = T,
                        ApptId = 0,
                        Open = true
                    };
                    _context.Schedules.Add(S);
                    _context.SaveChanges();
                    T = T.AddMinutes(30.0);
                }

            }
        }

        public void InitializeSchedule()
        {
            int currentYear = DateTime.Now.Year;
            DateTime D1=new DateTime(currentYear, 1, 1);
            DateTime D2=new DateTime(currentYear, 12, 31);
            int count = _context.Schedules.Where(s => s.Date >= D1).Count();
            if (count == 0)
            {
                CreateSchedule(D1, D2);
            }
            int nextYear = currentYear + 1;
            // are there any schedule entries for the next year?
            DateTime SOY=new DateTime(nextYear, 1, 1);
            DateTime EOY=new DateTime(nextYear, 12, 31);
            count = _context.Schedules.Where(s => s.Date >= SOY).Count();
            if (count == 0)
            {
                CreateSchedule(SOY, EOY);
            }
        }
    }
}
