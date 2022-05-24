using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/pacote")]
    [ApiController]
    [Authorize]
    public class PacoteController : ControllerBase
    {
        private readonly IPacoteQueries _pacoteQueries;
        private readonly IPacoteApplication _pacoteApplication;
        private readonly ITypePacoteQueries _typeQueries;
        private readonly IDocTemplateQueries _docQueries;
        private readonly IMateriaTemplateQueries _materiaQueries;
        public PacoteController(IPacoteQueries pacoteQueries, IPacoteApplication pacoteApplication,
            ITypePacoteQueries typeQueries, IDocTemplateQueries docQueries, IMateriaTemplateQueries materiaQueries)
        {
            _pacoteQueries = pacoteQueries;
            _pacoteApplication = pacoteApplication;
            _typeQueries = typeQueries;
            _docQueries = docQueries;
            _materiaQueries = materiaQueries;
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> GetCreatePacoteViewModel()
        {
            var typePacotes = await _typeQueries.GetTypePacotes();

            if (typePacotes.Count() == 0) return NotFound();

            var documentos = await _docQueries.GetAll();

            if (documentos.Count() == 0) return NotFound();

            return Ok(new { typePacotes = typePacotes, documentos = documentos });
        }

        [HttpGet]
        [Route("{typePacoteId}/{unidadeId}")]
        public async Task<IActionResult> GetPacotes(Guid typePacoteId, Guid unidadeId)
        {
            var pacotes = await _pacoteQueries.GetPacotes(typePacoteId, unidadeId);

            if (pacotes.Count() == 0) return NotFound();

            return Ok(new { pacotes = pacotes });
        }

        [HttpGet]
        [Route("lista/{typePacoteId}")]
        public async Task<IActionResult> GetPacotesByUserUnidade(Guid typePacoteId)
        {
            var pacotes = await _pacoteQueries.GetPacotesByUserUnidade(typePacoteId);

            if (pacotes.Count() == 0) return NotFound();

            return Ok(new { pacotes = pacotes });
        }

        [HttpGet]
        [Route("{pacoteId}")]
        public async Task<IActionResult> GetPacote(Guid pacoteId)
        {
            var pacote = await _pacoteQueries.GetPacoteById(pacoteId);

            return Ok(new { pacote = pacote });
        }

        [HttpGet]
        [Route("edit/{pacoteId}")]
        public async Task<IActionResult> GetEditPacoteView(Guid pacoteId)
        {
            var pacote = await _pacoteQueries.GetPacoteByIdTeste(pacoteId);

            var materias = await _materiaQueries.GetMateriaByTypePacoteId(pacote.typePacoteId);

            var docs = await _docQueries.GetAll();

            var typePacote = await _typeQueries.GetTypePacote(pacote.typePacoteId);

            return Ok(new { pacote = pacote, materias = materias, docs = docs, typePacote = typePacote });
        }

        [HttpPost]
        public async Task<IActionResult> SavePacote([FromBody] PacoteDto newPacote)
        {

            var validationsError = await _pacoteApplication.SavePacote(newPacote);

            if (validationsError.Any()) return Conflict(new { erros = validationsError });

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePacote([FromBody] PacoteDto editedPacote)
        {
            await _pacoteApplication.EditPacote(editedPacote);

            return Ok();

        }

    }
}
