using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Application.Queries
{
    public class UnidadeQueries : IUnidadeQueries
    {
        private readonly IConfiguration _config;
        public UnidadeQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<string>> GetMateriasDoCurso(int moduloId)
        {
            var query = @"select descricao from materias where moduloId = @moduloId";
            /* select * from agendaprovas where turmaId = 1070*/
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var materias = await connection.QueryAsync<string>(query, new { moduloId = moduloId });

                return materias.ToList();
            }
        }

        public async Task<List<MateriaDto>> GetMaterias(int moduloId)
        {
            var query = @"select * from materias where moduloId = @moduloId";
            /* select * from agendaprovas where turmaId = 1070*/
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var materias = await connection.QueryAsync<MateriaDto>(query, new { moduloId = moduloId });

                return materias.ToList();
            }
        }
    }
}
