
using ClosedXML.Excel;
using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Services.Interface;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.TurmaAggregate;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/relatorios")]
    public class RelatorioController : ControllerBase
    {
        private readonly InvictusDbContext _db;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly string unidade;
        private readonly IConfiguration _config;
        private readonly IRelatorioExcel _relatorioExcel;
        public RelatorioController(InvictusDbContext db, IHttpContextAccessor userHttpContext, IConfiguration config,
            IRelatorioExcel relatorioExcel)
        {
            _db = db;
            _userHttpContext = userHttpContext;
            _config = config;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            _relatorioExcel = relatorioExcel;
        }

        #region GET PRODUTOS RELATORIOS

        [HttpGet, DisableRequestSizeLimit]
        [Route("produto")]
        public IActionResult Download()
        {
            byte[] content;

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Leads");

                var headerTitulo = worksheet.Range(1, 1, 1, 7);
                headerTitulo.Style.Font.FontColor = XLColor.Black;
                headerTitulo.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                headerTitulo.Style.Fill.BackgroundColor = XLColor.LightGreen;
                headerTitulo.Style.Font.FontSize = 14;
                worksheet.Row(1).Height = 15;

                worksheet.Column(1).Width = 25;
                worksheet.Cell(1, 1).Value = "NOME";
                worksheet.Column(2).Width = 22;
                worksheet.Cell(1, 2).Value = "EMAIL";
                worksheet.Column(3).Width = 20;
                worksheet.Cell(1, 3).Value = "DATA";
                worksheet.Column(4).Width = 20;
                worksheet.Cell(1, 4).Value = "TELEFONE";
                worksheet.Column(5).Width = 18;
                worksheet.Cell(1, 5).Value = "BAIRRO";
                worksheet.Column(6).Width = 25;
                worksheet.Cell(1, 6).Value = "CURSO PRETENDIDO";
                worksheet.Column(7).Width = 18;
                worksheet.Cell(1, 7).Value = "UNIDADE";



                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    content = stream.ToArray();
                }
            }

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(content, contentType, "Modelo LEAD.xlsx");

            //Response.ContentType = "application/vnd.ms-excel";
            ////Response.AppendHeader("content-disposition", "attachment; filename=myfile.xls");
            //var folderName = Path.Combine("Resources", "Modelo","Modelo LEAD.xlsx");
            //    var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //    var memory = new MemoryStream();
            //    using (var stream = new FileStream(path, FileMode.Open))
            //    {
            //        stream.CopyTo(memory);
            //    }
            //    memory.Position = 0;
            //return File(memory, Response.ContentType = "application/vnd.ms-excel", Path.GetFileName(path));

        }

        
        [HttpGet]
        [Route("documentacao-aluno-certconclusao/{relatorio}")]
        public IActionResult GetAlunoDocsCertificado(string relatorio)
        {
            //var html = "<h3>Lista Matriculados</h3>";
            ////var conteudo = await _context.Conteudos.Where(c => c.ContratoId == 1).Select(c => c.Content).ToListAsync();
            ////foreach (var item in conteudo)
            ////{
            ////    html += item;
            ////}
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

        [HttpGet]
        [Route("pedagrelatorio/{relatorio}")]
        public IActionResult GetRelatoriosPedag(string relatorio)
        {
            //var html = "<h3>Lista Matriculados</h3>";
            ////var conteudo = await _context.Conteudos.Where(c => c.ContratoId == 1).Select(c => c.Content).ToListAsync();
            ////foreach (var item in conteudo)
            ////{
            ////    html += item;
            ////}
            //byte[] arquivo = PdfConlusao(html).ToArray();// await ExportFile();
            //string contentType = "application/pdf";
            ////return Ok(Pdf(html));
            //return File(arquivo, contentType);
            return Ok();

        }

        [HttpGet]
        //[Route("azure")]
        public IActionResult GetTesteAzure()
        {
            var cursos = "Invictus in Azure, baby!";

            return Ok(new { message = cursos });
        }

        #endregion

        #region ATRA LIVRO MAT RELATORIOS

        [HttpGet]
        [Route("livro-mat-turma-busca")]
        public async Task<ActionResult> MatRelatorioBusca()
        {
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
            var turmas = await _db.Turmas.Where(t => t.UnidadeId == unidadeId).ToListAsync();

            return Ok(turmas);
        }

        [HttpGet]
        [Route("livro-mat-turma/{turmaId}")]
        public async Task<ActionResult> MatRelatorio(int turmaId)
        {
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();

            var query = "";
            if (turmaId != 0)
            {
                query = @"select 
                        Aluno.Nome,
                        Aluno.NumeroMatricula,
                        Aluno.NomeSocial,
                        Aluno.CPF,
                        Aluno.RG,
                        Aluno.Nascimento,
                        Aluno.Naturalidade,
                        Aluno.NaturalidadeUF,
                        Aluno.Email,
                        Aluno.Ativo,
                        Turma.Identificador as Observacoes 
                        from aluno
                        inner join Turma on Turma.Id = @turmaId   
                        where aluno.id in (
                        select AlunoId from matriculados 
                        where matriculados.TurmaId = @turmaId and Turma.UnidadeId = @unidadeId)";
            }
            else
            {
                query = @"select 
                        Aluno.Nome,
                        Aluno.NumeroMatricula,
                        Aluno.NomeSocial,
                        Aluno.CPF,
                        Aluno.RG,
                        Aluno.Nascimento,
                        Aluno.Naturalidade,
                        Aluno.NaturalidadeUF,
                        Aluno.Email,
                        Aluno.Ativo,
                        Turma.Identificador as Observacoes 
                        from aluno
                        inner join Matriculados 
                        on Aluno.Id = Matriculados.AlunoId
                        inner join Turma on Matriculados.TurmaId = Turma.Id
                        where Turma.UnidadeId = @unidadeId";
            }

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<AlunoDto>(query, new { turmaId = turmaId, unidadeId= unidadeId });

                connection.Close();
               
                    var bytes = await _relatorioExcel.ExportLivroMatricula(results);
                    
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                   
                    return File(bytes, contentType, "Matricula.xlxs");
                
            }

           //return Ok(turmas);
        }

        /*
         select 
Aluno.Nome,
Aluno.NumeroMatricula,
Aluno.NomeSocial,
Aluno.CPF,
Aluno.RG,
Aluno.Nascimento,
Aluno.Naturalidade,
Aluno.NaturalidadeUF,
Aluno.Email,
Aluno.Ativo,
Turma.Identificador as Observacoes 
from aluno
inner join Turma on Turma.Id = 2   
where aluno.id in (
select AlunoId from matriculados 
where TurmaId = 2
)

-- TODAS TURMAS

select 
Aluno.Nome,
Aluno.NumeroMatricula,
Aluno.NomeSocial,
Aluno.CPF,
Aluno.RG,
Aluno.Nascimento,
Aluno.Naturalidade,
Aluno.NaturalidadeUF,
Aluno.Email,
Aluno.Ativo,
Turma.Identificador as Observacoes 
from aluno
inner join Matriculados 
on Aluno.Id = Matriculados.AlunoId
inner join Turma on Matriculados.TurmaId = Turma.Id
         */

        #endregion


        #region CALENDARIO RELATORIOS

        [HttpGet]
        [Route("calendario-busca")]
        public async Task<ActionResult> CalendarioRelatorioBusca([FromQuery] string parametro)
        {
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();

            if(parametro == "turma")
            {
                return Ok(await _db.Turmas.Where(t => t.UnidadeId == unidadeId).ToListAsync());
            }
            else if (parametro == "sala")
            {
                return Ok(await _db.Salas.Where(t => t.UnidadeId == unidadeId).ToListAsync());
            }
            else if (parametro == "prof")
            {
                return Ok(await _db.Colaboradores.Where(t => t.UnidadeId == unidadeId & t.Cargo == "Professor").ToListAsync());
            }

            return Ok();
        }

        [HttpGet]
        [Route("calendario-relatorio")]
        public async Task<ActionResult> CalendarioRelatorio([FromQuery] string parametro, [FromQuery] int id)
        {
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
//            turma
//sala
//prof
            var query = "";
            if (parametro == "")
            {
                query = @"";
            }
            else if(parametro == "")
            {
                query = @"";
            }else if (parametro == "")
            {

            }

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<AlunoDto>(query, new { unidadeId = unidadeId });

                connection.Close();

                var bytes = await _relatorioExcel.ExportLivroMatricula(results);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(bytes, contentType, "Matricula.xlxs");

            }

            //return Ok(turmas);
        }

        /*
         select 
Aluno.Nome,
Aluno.NumeroMatricula,
Aluno.NomeSocial,
Aluno.CPF,
Aluno.RG,
Aluno.Nascimento,
Aluno.Naturalidade,
Aluno.NaturalidadeUF,
Aluno.Email,
Aluno.Ativo,
Turma.Identificador as Observacoes 
from aluno
inner join Turma on Turma.Id = 2   
where aluno.id in (
select AlunoId from matriculados 
where TurmaId = 2
)

-- TODAS TURMAS

select 
Aluno.Nome,
Aluno.NumeroMatricula,
Aluno.NomeSocial,
Aluno.CPF,
Aluno.RG,
Aluno.Nascimento,
Aluno.Naturalidade,
Aluno.NaturalidadeUF,
Aluno.Email,
Aluno.Ativo,
Turma.Identificador as Observacoes 
from aluno
inner join Matriculados 
on Aluno.Id = Matriculados.AlunoId
inner join Turma on Matriculados.TurmaId = Turma.Id
         */

        #endregion

    }

    //var query = @"select * from calendarios where DiaAula >= @inicio 
    //            AND DiaAula <= @fim 
    //            AND Turno<> 'IntegralManhaTardeNoite' 
    //            order by DiaAula asc";


}
