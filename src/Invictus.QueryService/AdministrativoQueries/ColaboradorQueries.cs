using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class ColaboradorQueries : IColaboradorQueries
    {
        private readonly IConfiguration _config;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspNetUser;
        public ColaboradorQueries(IConfiguration config, IUnidadeQueries unidadeQueries, IAspNetUser aspNetUser)
        {
            _config = config;
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
        }

        public async Task<ColaboradorDto> GetColaboradoresByEmail(string email)
        {
            var query = "SELECT * from Colaboradores where LOWER(colaboradores.email) like LOWER('"+ email + "') "+
                        "collate SQL_Latin1_General_CP1_CI_AI "; 

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ColaboradorDto>(query);

                connection.Close();

                return results.FirstOrDefault();

            }
        }

        public async Task<ColaboradorDto> GetColaboradoresById(Guid colaboradorId)
        {
            var query = "SELECT * from Colaboradores where Colaboradores.id = @colaboradorId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ColaboradorDto>(query, new { colaboradorId  = colaboradorId } );

                connection.Close();

                return results.FirstOrDefault();

            }
        }

        public async Task<PaginatedItemsViewModel<ColaboradorDto>> GetColaboradoresByUnidadeId(int itemsPerPage, int currentPage, string paramsJson)
        {
            var parametros = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            return await GetColaboradores(itemsPerPage, currentPage, parametros, unidade.id);

        }

        private async Task<PaginatedItemsViewModel<ColaboradorDto>> GetColaboradores(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {
            //var ativos = param.ativo;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * from Colaboradores where ");
            if (param.nome != "") query.Append("LOWER(Colaboradores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append("LOWER(Colaboradores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append("LOWER(Colaboradores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            query.Append(" Colaboradores.UnidadeId = '" + unidadeId+"' ");
            if (param.ativo == false) query.Append(" AND Colaboradores.Ativo = 'True' ");
            query.Append(" ORDER BY Colaboradores.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) from Colaboradores where ");
            if (param.nome != "") queryCount.Append("LOWER(Colaboradores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append("LOWER(Colaboradores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append("LOWER(Colaboradores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            queryCount.Append(" Colaboradores.UnidadeId = '" + unidadeId+"'");
            if (param.ativo == false) queryCount.Append(" AND Colaboradores.Ativo = 'True' ");


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidadeId });

                var results = await connection.QueryAsync<ColaboradorDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                connection.Close();

                var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results.ToList());               

                return paginatedItems;

            }
        }
        
    }
}
