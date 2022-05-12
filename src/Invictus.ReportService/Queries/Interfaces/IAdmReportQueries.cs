using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.ReportService.Queries.Interfaces
{
    public interface IAdmReportQueries
    {
        Task<IEnumerable<Relatorio>> GetReport();
    }

    public class SampleDto : Relatorio
    {

    }

    public abstract class Relatorio
    {
    }

}
