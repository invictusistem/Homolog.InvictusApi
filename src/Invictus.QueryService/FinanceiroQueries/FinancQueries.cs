using Dapper;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries
{
    public class FinancQueries : IFinanceiroQueries
    {
        private readonly IConfiguration _config;
        public FinancQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<BoletoDto>> GetDebitoAlunos(Guid matriculaId)
        {

            var query = @"select* from Boletos where Boletos.InformacaoDebitoId = (
                        select id from InformacoesDebitos where InformacoesDebitos.MatriculaId = @matriculaId 
                        )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var debitos = await connection.QueryAsync<BoletoDto>(query, new { matriculaId = matriculaId });

                return debitos;

            }
        }

    }
}
