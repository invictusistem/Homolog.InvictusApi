using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries
{
    public class FornecedorQueries : IFornecedorQueries
    {
        private readonly IConfiguration _config;
        
        private readonly IAspNetUser _aspNetUser;
        public FornecedorQueries(IConfiguration config, IAspNetUser aspNetUser)
        {
            _config = config;
           
            _aspNetUser = aspNetUser;
        }

        public async Task<IEnumerable<ColaboradorDto>> GetAllColaboradoresAndProfessores()
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            var queryColaborador = @"SELECT Colaboradores.id, Colaboradores.nome 
                                    FROM Colaboradores 
                                    WHERE Colaboradores.ativo = 'True' 
                                    AND Colaboradores.unidadeId = @unidadeId";

            var queryProfessor = @"SELECT Professores.id, Professores.nome 
                                    FROM Professores 
                                    WHERE Professores.ativo = 'True' 
                                    AND Professores.unidadeId = @unidadeId";

            var pessoas = new List<ColaboradorDto>();

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var colaboradores = await connection.QueryAsync<ColaboradorDto>(queryColaborador, new { unidadeId  = unidadeId });

                if (colaboradores.Any())
                {
                    foreach (var colaborador in colaboradores)
                    {
                        //colaborador.isColaborador = true;
                        //colaborador.isProfessor = false;
                    }

                    pessoas.AddRange(colaboradores);
                }

                var professores = await connection.QueryAsync<ColaboradorDto>(queryProfessor, new { unidadeId = unidadeId });

                if (professores.Any())
                {
                    foreach (var professor in professores)
                    {
                        //professor.isColaborador = false;
                        //professor.isProfessor = true;
                    }

                    pessoas.AddRange(professores);
                }

                connection.Close();

                return pessoas;

            }
        }

        public async Task<IEnumerable<FornecedorDto>> GetAllFornecedores()
        {
            var query = "SELECT * FROM Fornecedores WHERE Fornecedores.ativo = 'True' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var fornecedores = await connection.QueryAsync<FornecedorDto>(query);

                connection.Close();

                return fornecedores;

            }
        }

        public async Task<FornecedorDto> GetFornecedor(Guid fornecedorId)
        {
            var query = "SELECT * from Fornecedores where Fornecedores.Id = @fornecedorId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var fornecedor = await connection.QuerySingleAsync<FornecedorDto>(query, new { fornecedorId  = fornecedorId });

                connection.Close();

                return fornecedor;

            }
        }

        public async Task<PaginatedItemsViewModel<FornecedorDto>> GetFornecedores(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var professores = await GetFornecedoresByFilter(itemsPerPage, currentPage, param);

            var profCount = await CountFornecedoresByFilter(itemsPerPage, currentPage, param);

            var paginatedItems = new PaginatedItemsViewModel<FornecedorDto>(currentPage, itemsPerPage, profCount, professores.ToList());

            return paginatedItems;
        }

        private async Task<int> CountFornecedoresByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) FROM Fornecedores INNER JOIN Unidades on Fornecedores.unidadeId = Unidades.Id WHERE   ");
            if (param.todasUnidades == false) queryCount.Append(" Fornecedores.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append("LOWER(Fornecedores.razaosocial) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append("LOWER(Fornecedores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append("LOWER(Fornecedores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Fornecedores.Ativo = 'True' "); } else { queryCount.Append(" Fornecedores.Ativo = 'True' OR Fornecedores.Ativo = 'False' "); }

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString());

                connection.Close();

                return countItems;
            }
        }

        private async Task<IEnumerable<FornecedorDto>> GetFornecedoresByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT 
                        Fornecedores.id, 
                        Fornecedores.razaosocial, 
                        Fornecedores.cnpj_cpf, 
                        Fornecedores.email,
                        Fornecedores.ativo,
                        Unidades.Sigla as unidadeSigla
                        FROM Fornecedores 
                        INNER JOIN Unidades on Fornecedores.UnidadeId = Unidades.Id 
                        WHERE ");

            if (param.todasUnidades == false) query.Append("Fornecedores.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Fornecedores.razaosocial) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Fornecedores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Fornecedores.cnpj_cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            // query.Append("Professores.UnidadeId = '" + unidade.id + "'");
            if (param.ativo == false) 
            { 
                query.Append(" Fornecedores.Ativo = 'True' "); 
            } else 
            { 
                query.Append(" Fornecedores.Ativo = 'True' OR Fornecedores.Ativo = 'False' "); 
            }
            query.Append(" ORDER BY Fornecedores.razaosocial ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");



            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                //var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidade.id });

                var results = await connection.QueryAsync<FornecedorDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                /// var paginatedItems = new PaginatedItemsViewModel<ProfessorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return results;

            }
        }
    }
}
