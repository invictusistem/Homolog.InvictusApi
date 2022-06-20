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

        public async Task<IEnumerable<PessoaDto>> GetAllColaboradoresAndProfessores()
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            var queryColaborador = @"SELECT Pessoas.id, Pessoas.nome 
                                    FROM Pessoas 
                                    WHERE Pessoas.ativo = 'True' 
                                    AND Pessoas.tipoPessoa = 'Colaborador' 
                                    AND Pessoas.unidadeId = @unidadeId";

            var queryProfessor = @"SELECT Pessoas.id, Pessoas.nome 
                                    FROM Pessoas 
                                    WHERE Pessoas.ativo = 'True' 
                                    AND Pessoas.tipoPessoa = 'Professor' 
                                    AND Pessoas.unidadeId = @unidadeId";

            var pessoas = new List<PessoaDto>();

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var colaboradores = await connection.QueryAsync<PessoaDto>(queryColaborador, new { unidadeId  = unidadeId });

                if (colaboradores.Any())
                {
                    foreach (var colaborador in colaboradores)
                    {
                        //colaborador.isColaborador = true;
                        //colaborador.isProfessor = false;
                    }

                    pessoas.AddRange(colaboradores);
                }

                var professores = await connection.QueryAsync<PessoaDto>(queryProfessor, new { unidadeId = unidadeId });

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

        public async Task<List<PessoaDto>> GetAllFornecedores(Guid? pessoaId)
        {
            //var pessoaId = Guid.NewGuid();

            var query = "SELECT * FROM Pessoas WHERE Pessoas.tipoPessoa = 'Fornecedor' AND Pessoas.ativo = 'True' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var fornecedores = await connection.QueryAsync<PessoaDto>(query);

                var todosFornecedores = fornecedores.ToList();

                var pessoaEstaNaLista = fornecedores.Where(f => f.id == pessoaId);

                if (pessoaId != new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    if (!pessoaEstaNaLista.Any())
                    {
                        var busca = @"SELECT * FROM Pessoas WHERE Pessoas.Id = @pessoaId AND Pessoas.tipoPessoa = 'Fornecedor' ";
                        var pessoa = await connection.QueryAsync<PessoaDto>(busca, new { pessoaId = pessoaId });
                        if (pessoa.Any())
                        {
                            if (pessoa.FirstOrDefault().tipoPessoa == "Fornecedor")
                            {
                                var pessoaAdd = pessoa.FirstOrDefault();
                                todosFornecedores.Add(pessoaAdd);
                            }
                        }
                    }
                }

                connection.Close();

                return todosFornecedores;

            }
        }

        public async Task<PessoaDto> GetFornecedor(Guid fornecedorId)
        {
            var query = @"SELECT * FROM Pessoas LEFT JOIN Enderecos ON Pessoas.id = Enderecos.PessoaId 
                        WHERE Pessoas.Id = @fornecedorId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var fornecedor = await connection.QueryAsync<PessoaDto, EnderecoDto, PessoaDto>(query,
                    map: (pessoa, endereco) =>
                    {
                        pessoa.endereco = endereco;
                        return pessoa;
                    },
                    new { fornecedorId = fornecedorId },
                    splitOn: "Id");

                connection.Close();

                return fornecedor.FirstOrDefault();

            }
        }

        public async Task<PaginatedItemsViewModel<PessoaDto>> GetFornecedores(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var professores = await GetFornecedoresByFilter(itemsPerPage, currentPage, param);

            var profCount = await CountFornecedoresByFilter(itemsPerPage, currentPage, param);

            var paginatedItems = new PaginatedItemsViewModel<PessoaDto>(currentPage, itemsPerPage, profCount, professores.ToList());

            return paginatedItems;
        }

        private async Task<int> CountFornecedoresByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) FROM Pessoas INNER JOIN Unidades on Pessoas.unidadeId = Unidades.Id WHERE Pessoas.tipoPessoa = 'Fornecedor' AND  ");
            if (param.todasUnidades == false) queryCount.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append("LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append("LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append("LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Pessoas.Ativo = 'True' "); } else { queryCount.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); }

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString());

                connection.Close();

                return countItems;
            }
        }

        private async Task<IEnumerable<PessoaDto>> GetFornecedoresByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT 
                        Pessoas.id, 
                        Pessoas.nome, 
                        Pessoas.razaosocial, 
                        Pessoas.cnpj, 
                        Pessoas.email,
                        Pessoas.ativo,
                        Unidades.Sigla as unidadeSigla
                        FROM Pessoas 
                        INNER JOIN Unidades on Pessoas.UnidadeId = Unidades.Id 
                        WHERE Pessoas.tipoPessoa = 'Fornecedor' AND ");

            if (param.todasUnidades == false) query.Append("Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Pessoas.cnpj) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            // query.Append("Professores.UnidadeId = '" + unidade.id + "'");
            if (param.ativo == false) 
            { 
                query.Append(" Pessoas.Ativo = 'True' "); 
            } else 
            { 
                query.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); 
            }
            query.Append(" ORDER BY Pessoas.razaosocial ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");



            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                //var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidade.id });

                var results = await connection.QueryAsync<PessoaDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                /// var paginatedItems = new PaginatedItemsViewModel<ProfessorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return results;

            }
        }
    }
}
