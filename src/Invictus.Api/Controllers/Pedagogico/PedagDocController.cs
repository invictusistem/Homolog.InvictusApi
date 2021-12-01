
using Invictus.Application.AdmApplication.Interfaces;
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
        public PedagDocController(IPedagDocApp pedagDocApp, IPedagDocsQueries pedagDocQueries)
        {
            _pedagDocApp = pedagDocApp;
            _pedagDocQueries = pedagDocQueries;
        }

        [HttpGet]
        [Route("{alunoDocumentId}")]
        public async Task<ActionResult> GetFile(Guid alunoDocumentId)
        {

            var doc = await _pedagDocQueries.GetDocumentById(alunoDocumentId);

            var memory = new MemoryStream(doc.dataFile);

            return File(memory, doc.contentArquivo, doc.nome);
        }

        [HttpPut]
        [Route("{alunoDocumentId}")]
        public async Task<IActionResult> SaveDocAluno(Guid alunoDocumentId, IFormFile file)
        {

            await _pedagDocApp.ADdFileToDocument(alunoDocumentId, file);

            return Ok();
        }
    }
}
