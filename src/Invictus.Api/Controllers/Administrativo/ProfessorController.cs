using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace Invictus.Api.Controllers
{
    [Route("api/professor")]
    [Authorize]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorApplication _profApplication;
        private readonly IProfessorQueries _profQueries;
        private readonly IUtils _utils;
        public ProfessorController(IProfessorApplication profApplication, IProfessorQueries profQueries, IUtils utils)
        {
            _profApplication = profApplication;
            _profQueries = profQueries;
            _utils = utils;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<ColaboradorDto>>> GetProfessor([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {

            var results = await _profQueries.GetProfessores(itemsPerPage, currentPage, paramsJson);

            // var cargos = await _db.Cargos.ToListAsync();
            if (results.Data.Count() == 0) return NotFound();

            return Ok(new { results = results });
        }


        [HttpGet]
        [Route("{professorId}")]
        public async Task<IActionResult> GetProfessorById(Guid professorId)
        {

            var result = await _profQueries.GetProfessorById(professorId);


            return Ok(new { result = result });
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfessor([FromBody] ProfessorDto newProfessor)
        {
            var msg = await _utils.ValidaDocumentosColaborador(newProfessor.cpf, null, newProfessor.email);
            if (msg.Count() > 0) return Conflict(new { msg = msg });
            await _profApplication.SaveProfessor(newProfessor);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> EditProfessor([FromBody] ProfessorDto editedProfessor)
        {
            await _profApplication.EditProfessor(editedProfessor);

            return NoContent();
        }
    }
}
