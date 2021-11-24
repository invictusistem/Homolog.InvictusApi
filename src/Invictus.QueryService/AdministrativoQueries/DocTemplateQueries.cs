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
    public class DocTemplateQueries : IDocTemplateQueries
    {
        private readonly IConfiguration _config;

        public DocTemplateQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<DocumentacaoTemplateDto>> GetAll()
        {
            var query = "SELECT * from DocumentacaoTemplate";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<DocumentacaoTemplateDto>(query);

                connection.Close();

                return results;

            }
        }

        public async Task<DocumentacaoTemplateDto> GetById(Guid documentacaoId)
        {
            var query = "SELECT * from DocumentoesTemplate WHERE DocumentoesTemplate.id = @documentacaoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<DocumentacaoTemplateDto>(query, new { documentacaoId = documentacaoId });

                connection.Close();

                return results;

            }
        }
    }
}
