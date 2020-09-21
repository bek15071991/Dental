using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dental.Data.Data;
using Dental.Data.Models;
using Dental.Data.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DentalDataTests
{
    [TestClass]
    public class ScheduleTests
    {
        private ApplicationDbContext context;
        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

            context = new ApplicationDbContext(options);
        }

        [TestMethod]
        public void TestCreateSimpleScheduleCreate()

        {
            TestInitialize();
            DateTime D1=new DateTime(2020, 1, 1);
            DateTime D2=new DateTime(2020, 1, 1);
            DateTime D3=new DateTime(2020,1,1,8, 30, 0);

            var cs=new ScheduleUtility(context);
            cs.CreateSchedule(D1, D2);
            int count = context.Schedules.Count();
            Schedule S = context.Schedules.Where(s => s.Date == D3).FirstOrDefault();

            Assert.AreEqual(16, count);
            Assert.IsNotNull(S);
        }
    }
}
