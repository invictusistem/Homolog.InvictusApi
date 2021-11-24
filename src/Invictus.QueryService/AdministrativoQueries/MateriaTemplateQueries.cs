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
    public class MateriaTemplateQueries : IMateriaTemplateQueries
    {
        private readonly IConfiguration _config;
        public MateriaTemplateQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<MateriaTemplateDto>> GetMateriaByTypePacoteId(Guid typePacoteId)
        {
            var query = @"SELECT * FROM MateriasTemplate WHERE MateriasTemplate.typepacoteid = @typePacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<MateriaTemplateDto>(query, new { typePacoteId = typePacoteId });

                connection.Close();

                return resultado;
            }
        }

        public async Task<IEnumerable<MateriaTemplateDto>> GetMateriasTemplateList()
        {
            var query = @"SELECT * FROM MateriasTemplate";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<MateriaTemplateDto>(query);

                connection.Close();

                return resultado;
            }
        }

        public async Task<MateriaTemplateDto> GetMateriaTemplate(Guid materiaId)
        {
            var query = @"SELECT * FROM MateriasTemplate WHERE MateriasTemplate.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QuerySingleAsync<MateriaTemplateDto>(query, new { id = materiaId });

                connection.Close();

                return resultado;
            }
        }
    }
}
