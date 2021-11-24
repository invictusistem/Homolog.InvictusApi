using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class AgendaTriQueries : IAgendaTriQueries
    {
        private readonly IConfiguration _config;
        //private readonly IAdmQueries _admQueries;
        //private readonly IAspNetUser _aspNetUser;
        public AgendaTriQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<AgendaTrimestreDto> GetAgenda(Guid agendaId)
        {
            var query = "SELECT * from AgendasTrimestre where AgendasTrimestre.id = @agendaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<AgendaTrimestreDto>(query, new { agendaId = agendaId });

                connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<AgendaTrimestreDto>> GetAgendas(Guid unidadeId, int ano)
        {
            var query = @"SELECT * from AgendasTrimestre where year(AgendasTrimestre.inicio) = @ano 
                        AND AgendasTrimestre.unidadeId = @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<AgendaTrimestreDto>(query, new { unidadeId = unidadeId, ano = ano });

                connection.Close();

                return result;

            }
        }
    }
}
