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
    public class AutorizacaoQueries : IAutorizacaoQueries
    {
        private readonly IConfiguration _config;
        
        public AutorizacaoQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<AutorizacaoDto>> GetUnidadesAutorizadas(Guid colaboradorId)
        {
            string query = @"SELECT * FROM Autorizacoes WHERE Autorizacoes.usuarioId = @colaboradorId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var count = await connection.QueryAsync<AutorizacaoDto>(query, new { colaboradorId = colaboradorId });

                connection.Close();

                return count;
            }
        }
    }
}
