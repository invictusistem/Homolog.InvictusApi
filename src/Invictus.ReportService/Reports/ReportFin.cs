using Invictus.ReportService.Reports.Interfaces;
using System.Threading.Tasks;

namespace Invictus.ReportService.Reports
{
    public class ReportFin : IReportFin
    {
        public Task<byte[]> ExcelReport(ReportParams param)
        {
            throw new System.NotImplementedException();
        }
    }
}
