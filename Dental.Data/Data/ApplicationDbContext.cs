using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dental.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Credential> Credentiatials { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PaySetup> PaySetups { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

    }
    }
