﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data.Models
{
    public class Bills
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public Procedures ProcedureId { get; set; }
        public decimal Charge { get; set; }
        public decimal Insurance { get; set; }
        public decimal Balance { get; set; }
        public bool Closed { get; set; }
    }
}