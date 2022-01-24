
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.ReportService.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{
    [Route("api/pedag/doc")]
    [Authorize]
    [ApiController]
    public class PedagDocController : ControllerBase
    {
        private readonly IPedagDocApp _pedagDocApp;
        private readonly IPedagDocsQueries _pedagDocQueries;
        private readonly IReportServices _reportService;
        public PedagDocController(IPedagDocApp pedagDocApp, IPedagDocsQueries pedagDocQueries, IReportServices reportService)
        {
            _pedagDocApp = pedagDocApp;
            _pedagDocQueries = pedagDocQueries;
            _reportService = reportService;
        }

        [HttpGet]
        [Route("{alunoDocumentId}")]
        public async Task<ActionResult> GetFile(Guid alunoDocumentId)
        {

            var doc = await _pedagDocQueries.GetDocumentById(alunoDocumentId);

            var memory = new MemoryStream(doc.dataFile);

            return File(memory, doc.contentArquivo, doc.nome);
        }

        [HttpGet]
        [Route("getcontrato/{matriculaId}")]
        public async Task<ActionResult> GetContrato(Guid matriculaId)
        {

            var doc = await _pedagDocQueries.GetContrato(matriculaId);

            var memory = new MemoryStream(doc.dataFile);

            return File(memory, doc.contentArquivo, doc.nome);
        }

        [HttpGet]
        [Route("getficha/{matriculaId}")]
        public async Task<ActionResult> GetFicha(Guid matriculaId)
        {

            var doc = await _pedagDocQueries.GetFicha(matriculaId);

            var memory = new MemoryStream(doc.dataFile);

            return File(memory, doc.contentArquivo, doc.nome);
        }

        [HttpGet]
        [Route("getpendencia/{matriculaId}")]
        public async Task<ActionResult> GetPendencia(Guid matriculaId)
        {
            var doc = await _reportService.GeneratePendenciaDocs(matriculaId);

            var memory = new MemoryStream(doc);

            return File(memory, "application/pdf", "lista_pendencia.pdf");
        }

        [HttpGet]
        [Route("lista/{matriculaId}")]
        public async Task<ActionResult> GetMatriculaDocs(Guid matriculaId)
        {

            var docs = await _pedagDocQueries.GetDocsMatriculaViewModel(matriculaId);

            return Ok(new { docs = docs });
        }

        [HttpPut]
        [Route("{alunoDocumentId}")]
        public async Task<IActionResult> SaveDocAluno(Guid alunoDocumentId, IFormFile file)
        {

            await _pedagDocApp.ADdFileToDocument(alunoDocumentId, file);

            return Ok();
        }

        [HttpPut]
        [Route("excluir/{documentId}")]
        public async Task<IActionResult> ExcluirDocAluno(Guid documentId)
        {

            await _pedagDocApp.ExcluirDoc(documentId);

            return Ok();
        }
    }
}
