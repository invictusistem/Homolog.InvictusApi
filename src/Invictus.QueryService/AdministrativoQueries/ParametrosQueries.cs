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
    public class ParametrosQueries : IParametrosQueries
    {
        private readonly IConfiguration _config;
        
        public ParametrosQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<ParametrosKeyDto> GetParamKey(string key)
        {
            var query = @"SELECT * FROM ParametrosKey WHERE ParametrosKey.[Key] = @key";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<ParametrosKeyDto>(query, new { key = key });

                connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<ParametroValueDto>> GetParamValue(string key)
        {
            var query = @"SELECT * FROM ParametrosValue WHERE ParametrosValue.ParametrosKeyId = (
                        SELECT ParametrosKey.id from ParametrosKey WHERE ParametrosKey.[Key] = @key
                        )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ParametroValueDto>(query, new { key = key });

                connection.Close();

                return result;

            }
        }
    }
}
