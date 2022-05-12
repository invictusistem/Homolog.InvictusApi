using Dapper;
using Invictus.Dtos.Financeiro.Configuracoes;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries
{
    public class FinConfigQueries : IFinConfigQueries
    {
        private readonly IConfiguration _config;
        
        public FinConfigQueries(IConfiguration config)
        {
            _config = config;
        }

        public Task<BancoDto> GetAllBancoById(System.Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BancoDto>> GetAllBancos()
        {
            var query = @"";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<BancoDto>(query);

                return bancos;

            }
        }

        public async Task<IEnumerable<CentroCustoDto>> GetAllCentroCusto()
        {
            var query = @"";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<CentroCustoDto>(query);

                return bancos;

            }
        }

        public Task<CentroCustoDto> GetAllCentroCustoById(System.Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MeioPagamentoDto>> GetAllMeiosPagamento()
        {
            var query = @"";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<MeioPagamentoDto>(query);

                return bancos;

            }
        }

        public Task<MeioPagamentoDto> GetAllMeiosPagamentoById(System.Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PlanoContaDto>> GetAllPlanos()
        {
            var query = @"";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<PlanoContaDto>(query);

                return bancos;

            }
        }

        public Task<PlanoContaDto> GetAllPlanosById(System.Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<SubContaDto>> GetAllSubContas()
        {
            var query = @"";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<SubContaDto>(query);

                return bancos;

            }
        }

        public Task<SubContaDto> GetAllSubContasById(System.Guid id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SubContaDto>> GetAllSubContasByPlanoId(System.Guid planoId)
        {
            throw new System.NotImplementedException();
        }
    }
}
