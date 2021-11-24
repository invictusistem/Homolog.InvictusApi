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
        public PacoteController(IPacoteQueries pacoteQueries, IPacoteApplication pacoteApplication,
            ITypePacoteQueries typeQueries, IDocTemplateQueries docQueries)
        {
            _pacoteQueries = pacoteQueries;
            _pacoteApplication = pacoteApplication;
            _typeQueries = typeQueries;
            _docQueries = docQueries;
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
        [Route("{typePacoteId}")]
        public async Task<IActionResult> GetPacote(Guid typePacoteId)
        {
            var pacote = await _pacoteQueries.GetPacoteById(typePacoteId);

            return Ok(new { pacote = pacote });
        }

        [HttpPost]
       // [Route("modulo")]
        public async Task<IActionResult> SavePacote([FromBody] PacoteDto newPacote)
        {

            await _pacoteApplication.SavePacote(newPacote);

            return Ok();

            //var modulo = _mapper.Map<Pacote>(newModulo);

            //modulo.SetCreateDate();
            //modulo.SetTotalHours(modulo.Materias);
            //var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            //modulo.SetUnidadeId(unidadeId);

            //_context.Modulos.Add(modulo);

            //_context.SaveChanges();

            //var listDocumentacaoExigida = new List<DocumentacaoExigencia>();


            //foreach (var item in newModulo.documentos)
            //{
            //    var doc = new DocumentacaoExigencia(item.descricao, item.comentario, modulo.Id);
            //    doc.SetTitular(item.titular);
            //    listDocumentacaoExigida.Add(doc);
            //}
            //if (newModulo.documentos.Count > 0)
            //{
            //    _context.DocsExigencias.AddRange(listDocumentacaoExigida);

            //    _context.SaveChanges();
            //}

            //return Ok();
        }

        [HttpPut]
       // [Route("pacote-editar")]
        public async Task<IActionResult> UpdatePacote([FromBody] PacoteDto editedPacote)
        {
            await _pacoteApplication.EditPacote(editedPacote);

            return Ok();

            //var pacote = _mapper.Map<Pacote>(editedPacote);

            //_context.Modulos.Update(pacote);
            //_context.SaveChanges();

            //return Ok();
        }

    }
}
