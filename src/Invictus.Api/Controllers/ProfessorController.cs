using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/professor")]
    public class ProfessorController : ControllerBase
    {
        public readonly InvictusDbContext _db;
        public readonly IMapper _mapper;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly IUnidadeQueries _unidadeQuery;
        private readonly string unidade;
        public ProfessorController(InvictusDbContext db, IMapper mapper, IHttpContextAccessor userHttpContext,
            IUnidadeQueries unidadeQuery)
        {
            _db = db;
            _mapper = mapper;
            _userHttpContext = userHttpContext;
            _unidadeQuery = unidadeQuery;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
        }

        [HttpGet]
        [Route("pesquisar")]
        public async Task<ActionResult<PaginatedItemsViewModel<ColaboradorDto>>> GetProfessor([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var parametros = JsonConvert.DeserializeObject<ParametrosDTO>(paramsJson);
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
            var results = await _unidadeQuery.GetProfessores(itemsPerPage, currentPage, parametros, unidadeId);

            var cargos = await _db.Cargos.ToListAsync();

            return Ok(results);
            //return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Save([FromBody] ProfessorDto professorDto)
        {
            var cargoId = await _db.Cargos.Where(c => c.Nome == "Professor").Select(c => c.Id).SingleOrDefaultAsync();

            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();

            //professor.cargoId = cargoId;

            professorDto.unidadeId = unidadeId;

            var professor = _mapper.Map<Professor>(professorDto);

            await _db.Professores.AddAsync(professor);

            await _db.SaveChangesAsync();

            return Ok();
        }


        


        [HttpPut]
        public ActionResult UpdateColaborador([FromBody] ProfessorDto professorDto)
        {   
            var professor = _mapper.Map<Professor>(professorDto);
         
            _db.Professores.Update(professor);

            _db.SaveChanges();

            return NoContent();
            
        }
    }
}
