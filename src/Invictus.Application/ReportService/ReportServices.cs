using DinkToPdf;
using DinkToPdf.Contracts;
using Invictus.Application.ReportService.Interfaces;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.ReportService
{
    public class ReportServices : IReportServices
    {
        private IConverter _converter;
        private readonly IContratoQueries _contratoQueries;
        private readonly IAlunoQueries _alunoQueries;
        private readonly IPDFDesigns _pdfDesign;
        public ReportServices(IConverter converter, IContratoQueries contratoQueries, IAlunoQueries alunoQueries,
            IPDFDesigns pdfDesign)
        {
            _converter = converter;
            _contratoQueries = contratoQueries;
            _alunoQueries = alunoQueries;
            _pdfDesign = pdfDesign;
        }
        public async Task<byte[]> GenerateContrato(GenerateContratoDTO info, Guid typePacoteId)
        {
            var contrato = await _contratoQueries.GetContratoCompletoByTypeId(typePacoteId);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = ContratoTemplate.Generate(info, contrato),// _template.GetContratoHTMLString(conteudos),// TemplateGenerator.GetHTMLString(),//@"<div><div style=""color: red""> PDF </div></div>", //TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
            };


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);



            return file;// File(file, "application/pdf");
        }

        public async Task<byte[]> GenerateFichaMatricula(GenerateFichaMatriculaDTO infos)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = FichaMatriculaTemplate.Generate(infos),// _template.GetContratoHTMLString(conteudos),// TemplateGenerator.GetHTMLString(),//@"<div><div style=""color: red""> PDF </div></div>", //TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
            };

            
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);



            return file;// File(file, "application/pdf");
        }

        public async Task<byte[]> GeneratePendenciaDocs(Guid matriculaId)
        {
            //var aluno = await _alunoQueries.GetAlunoByMatriculaId(matriculaId);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = _pdfDesign.GetPendenciaDocs(matriculaId),//
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
            };


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);



            return file;// File(file, "application/pdf");
        }

        public async Task<byte[]> GenerateContratoExemplo(GenerateContratoDTO info, Guid contratoId)
        {
            var contrato = await _contratoQueries.GetContratoById(contratoId);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = ContratoTemplate.GenerateExemplo(info, contrato),// _template.GetContratoHTMLString(conteudos),// TemplateGenerator.GetHTMLString(),//@"<div><div style=""color: red""> PDF </div></div>", //TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
                //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
                //FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
            };


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);



            return file;// File(file, "application/pdf");
        }
    }

    public class GenerateFichaMatriculaDTO
    {
        public string nomeCurso {get; set;}
        public DateTime dataInicio { get; set;}
        public string nomeAluno { get; set; }
        public DateTime nascimento { get; set; }
        public string naturalidade { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string complemento { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string telResiencia { get; set; }
        public string whatsapp { get; set; }
        public string email { get; set; }
        public string pai { get; set; }
        public string mae { get; set; }
        public string nomeResponsavelMatricula { get; set; }
    }

    public class GenerateContratoDTO
    {
       
        public string nome { get; set; }
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public string bairro { get; set; }
        public string complemento { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        
    }
}
