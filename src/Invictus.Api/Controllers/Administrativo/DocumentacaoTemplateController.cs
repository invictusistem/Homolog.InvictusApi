using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/documentacao")]
    [Authorize]
    [ApiController]
    public class DocumentacaoTemplateController : ControllerBase
    {
        private readonly IDocTemplateQueries _docTemplateQueries;
        private readonly IDocTemplateApplication _docTemplateApplication;
        public DocumentacaoTemplateController(IDocTemplateQueries docTemplateQueries, IDocTemplateApplication docTemplateApplication)
        {
            _docTemplateQueries = docTemplateQueries;
            _docTemplateApplication = docTemplateApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var docs = await _docTemplateQueries.GetAll();

            if (docs.Count() == 0) return NotFound();

            return Ok(new { docs = docs });
        }

        [HttpGet]
        [Route("{documentacaoId}")]
        public async Task<IActionResult> GetById(Guid documentacaoId)
        {
            var doc = await _docTemplateQueries.GetById(documentacaoId);

            if (doc == null) return NotFound();

            return Ok(new { doc = doc });
        }

        [HttpPost]
        public async Task<IActionResult> SaveDocumentacao([FromBody] DocumentacaoTemplateDto doc)
        {
            await _docTemplateApplication.AddDoc(doc);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditDocumentacao([FromBody] DocumentacaoTemplateDto doc)
        {
            await _docTemplateApplication.EditDoc(doc);

            return Ok();
        }

        [HttpDelete]
        [Route("{documentoId}")]
        public async Task<IActionResult> RemoveDocumentacao(Guid documentoId)
        {
            await _docTemplateApplication.RemoveDoc(documentoId);

            return Ok();
        }
    }
}
