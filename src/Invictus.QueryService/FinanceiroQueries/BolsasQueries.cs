using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries
{
    public class BolsasQueries : IBolsasQueries
    {
        private readonly IConfiguration _config;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspNetUser;
        public BolsasQueries(IConfiguration config, IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries)
        {
            _config = config;
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
        }

        public async Task<BolsaDto> GetBolsa(string senha)
        {
            var query = @"SELECT * FROM Bolsas WHERE Bolsas.Senha = @senha ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<BolsaDto>(query, new { senha = senha });

                connection.Close();

                //if(result.Any())
                //{
                //    result = new List<BolsaDto>();
                //}


                return result;//.FirstOrDefault();

            }
        }

        public async Task<BolsaDto> GetBolsa(Guid bolsaId)
        {
            var query = @"SELECT * FROM Bolsas WHERE Bolsas.id = @bolsaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<BolsaDto>(query, new { bolsaId = bolsaId });

                connection.Close();

                //if(result.Any())
                //{
                //    result = new List<BolsaDto>();
                //}


                return result;//.FirstOrDefault();

            }
        }


        public async Task<IEnumerable<BolsaDto>> GetBolsas(Guid typePacoteId)
        {
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            var query = @"SELECT    
                            Bolsas.Id,
                            Bolsas.Nome,
                            Bolsas.PercentualDesconto,
                            Bolsas.Colaborador,
                            Bolsas.UnidadeId,
                            Bolsas.TypePacoteId,
                            Bolsas.DataCriacao,
                            Bolsas.DataExpiracao
                         FROM Bolsas WHERE Bolsas.TypePacoteId = @typePacoteId 
                         AND Bolsas.UnidadeId = @unidadeId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<BolsaDto>(query, new { typePacoteId = typePacoteId, unidadeId = unidade.id });

                connection.Close();

                return results;

            }
        }

        public async Task<string> GetSenha(Guid bolsaId)
        {
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            var query = @"SELECT    
                            Bolsas.Senha
                         FROM Bolsas WHERE Bolsas.Id = @bolsaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<string>(query, new { bolsaId = bolsaId });

                connection.Close();

                return result;

            }
        }
    }
}
