using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
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
    public class TemplateQueries : ITemplateQueries
    {
        private readonly IConfiguration _config;
        public TemplateQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PaginatedItemsViewModel<PlanoPagamentoDto>> GetListPlanoPagamentoTemplate(int itemsPerPage, int currentPage)
        {
           // var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var planos = await GetPlanos(itemsPerPage, currentPage);

            var planosCount = await CountPlanos();

            var paginatedItems = new PaginatedItemsViewModel<PlanoPagamentoDto>(currentPage, itemsPerPage, planosCount, planos.ToList());

            return paginatedItems;
        }

        private async Task<IEnumerable<PlanoPagamentoDto>> GetPlanos(int itemsPerPage, int currentPage)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * from PlanoPagamentoTemplate ");
            query.Append(" ORDER BY PlanoPagamentoTemplate.descricao ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PlanoPagamentoDto>(query.ToString());

                connection.Close();

                return results;

            }
        }

        private async Task<int> CountPlanos()
        {

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) from PlanoPagamentoTemplate "); 

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString());

                connection.Close();

                return countItems;

            }
        }

        public Task<PlanoPagamentoDto> GetPagamentoTemplateById(Guid materiaTemplateId)
        {
            throw new NotImplementedException();
        }
    }
}
