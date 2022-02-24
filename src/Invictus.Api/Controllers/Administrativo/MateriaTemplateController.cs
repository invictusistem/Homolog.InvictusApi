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
        public MateriaTemplateController(IMateriaTemplateQueries materiaQueries, IMateriaTemplateApplication materiaApplication)
        {
            _materiaQueries = materiaQueries;
            _materiaApplication = materiaApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterias([FromQuery] int itemsPerPage, [FromQuery] int currentPage)
        {
            var results = await _materiaQueries.GetMateriasTemplateList(itemsPerPage, currentPage);

            if (results.Data.Count() == 0) return NotFound();

            return Ok(new { results = results });
        }

        [HttpGet]
        [Route("{materiaId}")]
        public async Task<IActionResult> GetMateriaById(Guid materiaId)
        {

            var results = await _materiaQueries.GetMateriaTemplate(materiaId);//.GetProfessores(itemsPerPage, currentPage, paramsJson);

            // var cargos = await _db.Cargos.ToListAsync();
            if (results == null) return NotFound();

            return Ok(new { results = results });
        }

        [HttpGet]
        [Route("filtro/{typePacoteId}")]
        public async Task<IActionResult> GetMateriaByTypePacoteId(Guid typePacoteId)
        {

            var materias = await _materiaQueries.GetMateriaByTypePacoteId(typePacoteId);

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
    }
}
