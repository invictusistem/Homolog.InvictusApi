using Invictus.ReportService.Queries.Interfaces;
using Invictus.ReportService.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.ReportService.Reports
{
    public class QueryResult
    {
        public string nome { get; set; }
    }
    public class ExportReport
    {
        public async Task<byte[]> Export(IDownloadReport obj, IEnumerable<Relatorio> values)
        {
            var site = obj.parametros.report;
            byte[] file = await obj.ExportFile(/*workOrders,*/ site);

            return file;
        }
    }
    public interface IDownloadReport
    {
        ReportParams parametros { get; set; }
        Task<byte[]> ExportFile(string site);
    }

    public class ReportAdm : IReportAdm
    {
        private IDownloadReport download;
        private readonly IAdmReportQueries _admqueries;
        public ReportAdm(IAdmReportQueries admqueries)
        {           
            download = null;
            _admqueries = admqueries;
        }
        public async Task<byte[]> ExcelReport(ReportParams param)
        {
            //IEnumerable<MediaPulseReturn> workOrders = new List<MediaPulseReturn>();
            //IEnumerable<Relatorio> infos = new List<Relatorio>();

            var infos = await GetInfos(param);

            download.parametros = param;
            ExportReport export = new ExportReport();
            byte[] arquivo = await export.Export(download, infos);

            return arquivo;
        }

        private async Task<IEnumerable<Relatorio>> GetInfos(ReportParams param)
        {
            IEnumerable<Relatorio> infos = new List<Relatorio>();

            switch (param.report)
            {
                case "cde":
                    //download = new ExportExcelCDE();
                    infos = await _admqueries.GetReport();
                    break;
                case "cdf":
                    //download = new ExportExcelCDF();
                    //workOrders = await _requestCDF.GetWos(parametros);
                    break;
                case "veiculos":
                    //download = new ExportExcelVeiculos();
                    //workOrders = await _requestVeiculos.GetWos(parametros);
                    break;
                case "controleestudios":
                    //download = new ExportarExcelControleEstudios();
                    //workOrders = await _requestControleEstudio.GetWos(parametros);
                    break;
                case "estudios":
                    //download = new ExportExcelEstudios();
                    //workOrders = await _requestEstudios.GetWos(parametros);
                    break;
                case "efeitos":
                    //download = new ExportExcelEfeitos();
                    //workOrders = await _requestEfeitos.GetWos(parametros);
                    break;

            }
            

            return infos;
        }
    }


}
