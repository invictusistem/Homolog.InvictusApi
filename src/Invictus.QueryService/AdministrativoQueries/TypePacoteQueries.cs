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
    public class TypePacoteQueries : ITypePacoteQueries
    {
        private readonly IConfiguration _config;
        public TypePacoteQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<TypePacoteDto> GetTypePacote(Guid typePacoteId)
        {
            var query = "SELECT * FROM TypePacote WHERE TypePacote.id = @typePacoteId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QuerySingleAsync<TypePacoteDto>(query, new { typePacoteId  = typePacoteId });

                connection.Close();

                return resultado;
            }
        }

        public async Task<IEnumerable<TypePacoteDto>> GetTypePacotes()
        {
            var query = "SELECT * FROM TypePacote";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<TypePacoteDto>(query);

                connection.Close();

                return resultado;
            }
        }
    }
}
