using System.Threading.Tasks;

namespace Invictus.ReportService.Reports.Interfaces
{
    public interface IReportFin
    {
        Task<byte[]> ExcelReport(ReportParams param);
    }
}
