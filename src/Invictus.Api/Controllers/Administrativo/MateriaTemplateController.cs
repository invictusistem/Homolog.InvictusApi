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
    [Route("api/materia-template")]
    [Authorize]
    [ApiController]
    public class MateriaTemplateController : ControllerBase
    {
        private readonly IMateriaTemplateQueries _materiaQueries;
        private readonly IMateriaTemplateApplication _materiaApplication;
        private readonly ITypePacoteQueries _typeQueries;
        public MateriaTemplateController(IMateriaTemplateQueries materiaQueries, IMateriaTemplateApplication materiaApplication, ITypePacoteQueries typeQueries)
        {
            _materiaQueries = materiaQueries;
            _materiaApplication = materiaApplication;
            _typeQueries = typeQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetMateriasPaginated([FromQuery] int itemsPerPage, [FromQuery] int currentPage)
        {
            var results = await _materiaQueries.GetMateriasTemplateList(itemsPerPage, currentPage);

            if (results.Data.Count() == 0) return NotFound();

            return Ok(new { results = results });
        }

        [HttpGet]
        [Route("materias")]
        public async Task<IActionResult> GetMaterias()
        {
            var results = await _materiaQueries.GetAllMaterias();

            if (results.Count() == 0) return NotFound();

            return Ok(new { results = results });
        }

        [HttpGet]
        [Route("{materiaId}")]
        public async Task<IActionResult> GetMateriaById(Guid materiaId)
        {

            var results = await _materiaQueries.GetMateriaTemplate(materiaId);//.GetProfessores(itemsPerPage, currentPage, paramsJson);

            if (results == null) return NotFound();

            var typePacotes = await _typeQueries.GetTypePacotes();

            if (typePacotes.Count() == 0) return NotFound();

            return Ok(new { results = results, types = typePacotes });
        }

        [HttpGet]
        [Route("filtro/{typePacoteId}")]
        public async Task<IActionResult> GetMateriaByTypePacoteId(Guid typePacoteId)
        {

            var materias = await _materiaQueries.GetMateriaByTypePacoteId(typePacoteId);

            // var cargos = await _db.Cargos.ToListAsync();
            if (materias == null) return NotFound();

            return Ok(new { materias });
        }

        [HttpGet]
        [Route("materia-liberada/{typePacoteId}/{professorId}")]
        public async Task<IActionResult> GetMateriaLiberadas(Guid typePacoteId, Guid professorId)
        {

            var materias = await _materiaQueries.GetMateriasByTypePacoteLiberadoParaOProfessor(typePacoteId, professorId);

            // var cargos = await _db.Cargos.ToListAsync();
            if (materias == null) return NotFound();

            return Ok(new { materias = materias });
        }

        [HttpPost]
        public async Task<IActionResult> SaveMateria([FromBody] MateriaTemplateDto materia)
        {

            // validar se já nao existe o mesmo nome salvo
            await _materiaApplication.AddMateria(materia);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> EditMateria([FromBody] MateriaTemplateDto materia)
        {
            await _materiaApplication.EditMateria(materia);

            return NoContent();
        }

        [HttpDelete]
        [Route("{materiaId}")]
        public async Task<IActionResult> DeleteMateria(Guid materiaId)
        {
            await _materiaApplication.DeleteMateria(materiaId);

            return NoContent();
        }
    }
}
