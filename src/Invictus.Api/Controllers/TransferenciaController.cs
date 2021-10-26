using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using Invictus.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/transferencia")]
    public class TransferenciaController : ControllerBase
    {
        private readonly InvictusDbContext _context;
        private readonly IMatriculaQueries _matriculaQueries;
        public TransferenciaController(InvictusDbContext context, IMatriculaQueries matriculaQueries)
        {
            _context = context;
            _matriculaQueries = matriculaQueries;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarCadastro([FromQuery] string query)
        {
            var param = JsonConvert.DeserializeObject<QueryDto>(query);
            var pessoas = await _context.Alunos.Where(a => a.CPF == param.cpf).ToListAsync();

            if (pessoas.Count() > 0) return Conflict(new { message = "Foi localizado um aluno matriculado/cadastrado com este CPF." });

            return Ok();
        }

        //[Authorize(Roles = "MasterAdm,SuperAdm")]
        //[HttpPost]
        //public async Task<IActionResult> TransExterna([FromBody] TransferenciaForm query)
        //{
        //    //var param = JsonConvert.DeserializeObject<QueryDto>(query);

        //    return Ok();
        //}
    }

    public class TransferenciaForm
    {
        public AlunoDto alunoDto { get; set; }
        public RespFinancDto respAlunoDto { get; set; }
        public RespMenorDto respMenorDto { get; set; }
        public MatriculaForm turma { get; set; }
    }

    public class MatriculaForm {
        public string cienciaCurso {get;set;}
        public string turmaId { get; set; }
    }
}
