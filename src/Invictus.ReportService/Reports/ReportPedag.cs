using Invictus.ReportService.Reports.Interfaces;
using System.Threading.Tasks;

namespace Invictus.ReportService.Reports
{
    public class ReportPedag : IReportPedag
    {
        public Task<byte[]> ExcelReport(ReportParams param)
        {
            throw new System.NotImplementedException();
        }
    }
}
