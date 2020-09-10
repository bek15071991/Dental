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
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<Clients> Clients { get; set; }

        public DbSet<Credentials> Credentiatials { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<PaySetups> PaySetups { get; set; }
        public DbSet<Procedures> Procedures { get; set; }
        public DbSet<Providers> Providers { get; set; }

    }
    }
