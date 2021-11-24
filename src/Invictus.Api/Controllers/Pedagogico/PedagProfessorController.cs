using Dapper;
using Invictus.Dtos.AdmDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{

    [Route("api/pedag/aluno")]
    [Authorize]
    [ApiController]
    public class PedagProfessorController : ControllerBase
    {
        private readonly IConfiguration _config;
        //private readonly IAdmQueries _admQueries;
        //private readonly IAspNetUser _aspNetUser;
        public PedagProfessorController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("{turmaId}")]
        public async Task<IActionResult> AdicionarProfessores(Guid turmaId)
        {
            var query = "SELECT * from Professores";
            IEnumerable<ProfessorDto> result = new List<ProfessorDto>();
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                result = await connection.QueryAsync<ProfessorDto>(query, new { turmaId = turmaId });

                connection.Close();

               // return Oresult;

            }

            if (result.Count() == 0) return NotFound();

            return Ok();
        }
    }
}
