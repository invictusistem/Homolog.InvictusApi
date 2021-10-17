using Invictus.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;
using System.Collections;
using Invictus.Application.Queries.Interfaces;
using Invictus.Domain.Administrativo.Models;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/document")]
    public class DocumentController : ControllerBase
    {
       // private IConverter _converter;
        private InvictusDbContext _context;
        private readonly IAlunoQueries _alunoQueries;

        public DocumentController(/*IConverter converter,*/ InvictusDbContext context, IAlunoQueries alunoQueries)
        {
            //_converter = converter;
            _context = context;
            _alunoQueries = alunoQueries;
        }

        //public static MemoryStream Pdf(string html)
        //{
        //    StringReader sr = new StringReader(html);

        //    Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

        //    MemoryStream stream = new MemoryStream();

        //    PdfWriter.GetInstance(document, stream);

        //    document.Open();

        //    StyleSheet styles = new StyleSheet();

        //    var unicodeFontProvider = FontFactoryImp.Instance;
        //    unicodeFontProvider.DefaultEmbedding = BaseFont.EMBEDDED;
        //    unicodeFontProvider.DefaultEncoding = BaseFont.IDENTITY_H;

        //    var props = new Hashtable
        //    {

        //        { "font_factory", unicodeFontProvider }
        //    };
        //    var objects = HtmlWorker.ParseToList(sr, styles);
        //    foreach (IElement element in objects)
        //    {
        //        document.Add(element);
        //    }

        //    document.Close();
        //    return stream;
        //}

        [HttpGet]
        [Route("pdf")]
        public async Task<IActionResult> DownloadPDFReport()
        {
            //var html = "";
            //var conteudo = await _context.Conteudos.Where(c => c.ContratoId == 1).Select(c => c.Content).ToListAsync();
            //foreach (var item in conteudo)
            //{
            //    html += item;
            //}
            //byte[] arquivo;// = Pdf(html).ToArray();// await ExportFile();
            //string contentType = "application/pdf";
            ////return Ok(Pdf(html));
            return Ok();// File(arquivo, contentType);


        }

        [HttpGet]
        [Route("pdf-image")]
        public IActionResult DownloadPDFImageReport()
        {
            var html = @"&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;Empresa&gt;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<span style=""background - color: transparent; font - size: 1rem; "">&lt;Empresa&gt;</span></div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;Empresa&gt;<span style=""background - color: transparent; font - size: 1rem; ""><br></span></div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;Empresa&gt;<br></div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;Empresa&gt;<br></div><div>&nbsp;<b><font size=""4""><i>Boletim</i></font></b></div><div><b style=""background - color: transparent; font - size: 1rem; ""><font size=""4"">___________________________________________________________________________</font></b></div><div><b style=""background - color: transparent; font - size: 1rem; ""><font size=""4""><br></font></b></div>";
            //var conteudo = await _context.Conteudos.Where(c => c.ContratoId == 1).Select(c => c.Content).ToListAsync();
            //foreach (var item in conteudo)
            //{
            //    html += item;
            //}
            //byte[] arquivo = PdfImage(html).ToArray();// await ExportFile();
            //string contentType = "application/pdf";
            //return Ok(Pdf(html));
            return Ok();// File(arquivo, contentType);
        }

        //public static MemoryStream PdfImage(string html)
        //{
        //    StringReader sr = new StringReader(html);

        //    Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

            

        //    MemoryStream stream = new MemoryStream();

        //    PdfWriter.GetInstance(document, stream);

        //    document.Open();

        //    StyleSheet styles = new StyleSheet();

        //    var unicodeFontProvider = FontFactoryImp.Instance;
        //    unicodeFontProvider.DefaultEmbedding = BaseFont.EMBEDDED;
        //    unicodeFontProvider.DefaultEncoding = BaseFont.IDENTITY_H;

        //    var props = new Hashtable
        //    {

        //        { "font_factory", unicodeFontProvider }
        //    };
        //    var objects = HtmlWorker.ParseToList(sr, styles);
        //    foreach (IElement element in objects)
        //    {
        //        document.Add(element);
        //    }

        //    document.Close();
        //    return stream;
        //}

        [HttpGet]
        [Route("aluno-docs")]
        public async Task<ActionResult> GetFile([FromQuery] int alunoid, [FromQuery] int docid)
        {

            var doc = new DocumentoAluno();

            await Task.Run(() =>
            {
                //doc = _db.DocumentosEStagio.Where(doc => doc.AlunoId == alunoid & doc.Id == docid).SingleOrDefault();
            });

            doc = await _context.DocumentosAlunos.Where(d => d.Id == docid).SingleOrDefaultAsync();
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl);
            //if (!System.IO.File.Exists((filePath))
            //    return NotFound();
            var memory = new MemoryStream(doc.DataFile);


            //await using (var stream = new FileStream(filePath, FileMode.Open))
            //{
            //    await stream.CopyToAsync(memory);
            //}
            //memory.Position = 0;
            //return new FileStreamResult(stream, result.FileType);
            return File(memory, doc.ContentArquivo, doc.Nome);
        }

        [HttpGet]
        [Route("documentacao-aluno/{alunoId}")]
        public async Task<IActionResult> GetAlunoDocs(int alunoId)
        {
            var docs = await _alunoQueries.GetDocsAluno(alunoId);
            
            var matriculas = await _context.Matriculados.Where(m => m.AlunoId == alunoId).ToListAsync();

            var matriculado = false;

            if (matriculas.Count() > 0) matriculado = true;

            return Ok(new { matriculado = matriculado, docs = docs });
        }


        [HttpGet]
        [Route("relatorios-pedagogico")]
        public IActionResult GetAlunoDocsCertificado()
        {
            var html = "<h3>Certificado Conclusão de Curso</h3>";
            //var conteudo = await _context.Conteudos.Where(c => c.ContratoId == 1).Select(c => c.Content).ToListAsync();
            //foreach (var item in conteudo)
            //{
            //    html += item;
            //}
            //byte[] arquivo = PdfConlusao(html).ToArray();// await ExportFile();
            //string contentType = "application/pdf";
            ////return Ok(Pdf(html));
            //return File(arquivo, contentType);
            return Ok();


        }


        //public static MemoryStream PdfConlusao(string html)
        //{
        //    StringReader sr = new StringReader(html);

        //    Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

        //    MemoryStream stream = new MemoryStream();

        //    PdfWriter.GetInstance(document, stream);

        //    document.Open();

        //    StyleSheet styles = new StyleSheet();

        //    var unicodeFontProvider = FontFactoryImp.Instance;
        //    unicodeFontProvider.DefaultEmbedding = BaseFont.EMBEDDED;
        //    unicodeFontProvider.DefaultEncoding = BaseFont.IDENTITY_H;

        //    var props = new Hashtable
        //    {

        //        { "font_factory", unicodeFontProvider }
        //    };
        //    var objects = HtmlWorker.ParseToList(sr, styles);
        //    foreach (IElement element in objects)
        //    {
        //        document.Add(element);
        //    }

        //    document.Close();
        //    return stream;
        //}

        //public Task<byte[]> ExportFile()
        //{
        //    var globalSettings = new GlobalSettings
        //    {
        //        ColorMode = ColorMode.Color,
        //        Orientation = Orientation.Landscape,
        //        PaperSize = PaperKind.A4,
        //        Margins = new MarginSettings { Top = 10 },
        //        DocumentTitle = ""
        //    };

        //    var objectSettings = new ObjectSettings
        //    {
        //        PagesCount = true,
        //        HtmlContent = Conteudo(),
        //        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
        //        HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
        //        FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "" }
        //    };

        //    var pdf = new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = globalSettings,
        //        Objects = { objectSettings }
        //    };

        //    var filePdf = _converter.Convert(pdf);





        //    return Task.FromResult(filePdf);
        //}

        public string Conteudo()
        {

            var stringTemplate = new StringBuilder();
            stringTemplate.Append(@"<html><head></head><body><div style='color: red' >Hello from invictus!!</div></body></html>");

            return stringTemplate.ToString();
        }
    }
}
