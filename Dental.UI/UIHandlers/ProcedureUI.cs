using AutoMapper;
using Dental.Data.Models;
using Dental.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.UI.UIHandlers
{
    public class ProcedureUI
    {
        public ProcedureUI(IProcedureDataService procedureDataService, IMapper mapper)
        {
            _procedureDataService = procedureDataService;
            _mapper = mapper;
        }

        public IProcedureDataService _procedureDataService { get; }
        public IMapper _mapper { get; }
        public async Task<Procedure> GetFromCode(string procedureCode)
        {
            return (await _procedureDataService.GetProcedures())
                .Where(p => p.Code == procedureCode)
                .FirstOrDefault();
        }
    }
}
