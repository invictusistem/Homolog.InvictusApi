using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class PlanoPagamentoQueries : IPlanoPagamentoQueries
    {
        private readonly IConfiguration _config;
        public PlanoPagamentoQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PlanoPagamentoDto> GetPlanoById(Guid planoId)
        {
            var query = "SELECT * FROM PlanoPagamentoTemplate WHERE PlanoPagamentoTemplate.id = @planoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QuerySingleAsync<PlanoPagamentoDto>(query, new { planoId = planoId });

                connection.Close();

                return resultado;
            }
        }

        public async Task<IEnumerable<PlanoPagamentoDto>> GetPlanosByTypePacote(Guid typePacoteId)
        {
            var query = "SELECT * FROM PlanoPagamentoTemplate WHERE PlanoPagamentoTemplate.typepacoteid = @typePacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<PlanoPagamentoDto>(query, new { typePacoteId = typePacoteId });

                connection.Close();

                return resultado;
            }
        }
    }
}
