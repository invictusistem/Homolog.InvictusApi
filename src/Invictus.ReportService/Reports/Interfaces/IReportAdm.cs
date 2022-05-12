using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.ReportService.Reports.Interfaces
{
    public interface IReportAdm
    {
        Task<byte[]> ExcelReport(ReportParams param);
    }
}
