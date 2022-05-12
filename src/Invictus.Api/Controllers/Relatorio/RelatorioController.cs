using Invictus.ReportService.Reports;
using Invictus.ReportService.Reports.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Relatorio
{
    [ApiController]
    [Authorize]
    [Route("api/relatorio")]
    public class RelatorioController : ControllerBase
    {
        private readonly IReportAdm _reportAdm;
        private readonly IReportFin _reportFin;
        private readonly IReportPedag _reportPedag;
        public RelatorioController(IReportAdm reportAdm, IReportFin reportFin, IReportPedag reportPedag)
        {
            _reportAdm = reportAdm;
            _reportFin = reportFin;
            _reportPedag = reportPedag;
        }

        [HttpGet]
        [Route("administrativo")]
        public async Task<IActionResult> GerarRelatorioAdm([FromQuery] string parameters)
        {
            var param = JsonSerializer.Deserialize<ReportParams>(parameters);

            byte[] arquivo = await _reportAdm.ExcelReport(param);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(arquivo, contentType);
        }

        [HttpGet]
        [Route("pedagogico")]
        public async Task<IActionResult> GerarRelatorioPedag([FromQuery] string parameters)
        {
            var param = JsonSerializer.Deserialize<ReportParams>(parameters);

            byte[] arquivo = await _reportPedag.ExcelReport(param);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(arquivo, contentType);
        }

        [HttpGet]
        [Route("financeiro")]
        public async Task<IActionResult> GerarRelatorioFinanceiro([FromQuery] string parameters)
        {
            var param = JsonSerializer.Deserialize<ReportParams>(parameters);

            byte[] arquivo = await _reportFin.ExcelReport(param);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(arquivo, contentType);
        }
    }
}
