using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;

namespace Invictus.Application.Queries
{
    public class PedagModelsQueries : IPedagModelsQueries
    {
        private readonly IConfiguration _config;
        public PedagModelsQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<EstagioDto>> GetListEstagios()
        {
            var query = @"select * from estagio";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var estagios = await connection.QueryAsync<EstagioDto>(query);

                return estagios;
            }
        }
    }
}
