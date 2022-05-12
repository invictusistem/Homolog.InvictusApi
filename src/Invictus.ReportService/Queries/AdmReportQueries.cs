using Invictus.ReportService.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.ReportService.Queries
{
    public class AdmReportQueries : IAdmReportQueries
    {
        public Task<IEnumerable<Relatorio>> GetReport()
        {
            throw new NotImplementedException();
        }
    }
}
