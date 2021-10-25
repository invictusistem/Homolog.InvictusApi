using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Invictus.Application.Dtos;
using Invictus.Core;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/professor")]
    public class ProfessorController : ControllerBase
    {
        public readonly InvictusDbContext _db;
        public readonly IMapper _mapper;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly string unidade;
        public ProfessorController(InvictusDbContext db, IMapper mapper, IHttpContextAccessor userHttpContext)
        {
            _db = db;
            _mapper = mapper;
            _userHttpContext = userHttpContext;
            
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
        }

        [HttpPost]
        public async Task<ActionResult> Save([FromBody]ColaboradorDto professor)
        {
            var cargoId = await _db.Cargos.Where(c => c.Nome == "Professor").Select(c => c.Id).SingleOrDefaultAsync();

            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();

            professor.cargoId = cargoId;

            professor.unidadeId = unidadeId;

            var colaborador = _mapper.Map<Colaborador>(professor);

            await _db.Colaboradores.AddAsync(colaborador);

            await _db.SaveChangesAsync();



            return Ok();
        }


        [HttpGet]
        [Route("pesquisar")]
        public async Task<ActionResult<PaginatedItemsViewModel<ColaboradorDto>>> GetProfessor([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var parametros = JsonConvert.DeserializeObject<ParametrosDTO>(paramsJson);
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
            var results = await _queries.GetColaboradores(itemsPerPage, currentPage, parametros, unidadeId);

            var cargos = await _context.Cargos.ToListAsync();

            return Ok(results);
            return Ok();
        }
    }
}
