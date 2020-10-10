using AutoMapper;
using Dental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.ViewModels
{
    public class Automapping : Profile
    {
        public Automapping()
        {
            CreateMap<MessageVM, Message>();
            CreateMap<Message, MessageVM>();
            CreateMap<PaySetup, PaySetupx>();
            CreateMap<PaySetupx, PaySetup>();
            CreateMap<RegisterVM, Client>();
            CreateMap<Client, RegisterVM>();
            CreateMap<RegisterVM, Credential>();
            CreateMap<Bill, BillVM>();
            CreateMap<BillVM, Bill>();
            CreateMap<BillVM, Appointment>();
            CreateMap<Appointment, BillVM>();
        }
    }
}
