using System.Threading.Tasks;

namespace Invictus.ReportService.Reports.Interfaces
{
    public interface IReportPedag
    {
        Task<byte[]> ExcelReport(ReportParams param);
    }
}
