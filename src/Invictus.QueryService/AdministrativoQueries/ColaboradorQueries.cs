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

        public async Task<PessoaDto> GetColaboradoresByEmail(string email)
        {
            var query = "SELECT * FROM Pessoas WHERE LOWER(Pessoas.email) like LOWER('" + email + "') " +
                        "collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PessoaDto>(query);

                connection.Close();

                return results.FirstOrDefault();

            }
        }

        public async Task<PessoaDto> GetColaboradoresById(Guid colaboradorId)
        {
            var query = @"SELECT * FROM Pessoas INNER JOIN Enderecos ON Pessoas.id = Enderecos.PessoaId WHERE Pessoas.id = @colaboradorId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PessoaDto>(query, new { colaboradorId = colaboradorId });

                connection.Close();

                return results.FirstOrDefault();

            }

        }

        public async Task<PaginatedItemsViewModel<PessoaDto>> GetColaboradoresByUnidadeId(int itemsPerPage, int currentPage, string paramsJson)
        {
            var parametros = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            //return await GetColaboradores(itemsPerPage, currentPage, parametros, unidade.id);
            return await GetColaboradoresV2(itemsPerPage, currentPage, parametros, unidade.id);

        }

        public async Task<string> GetEmailFromColaboradorById(Guid colaboradorId)
        {
            var query = "SELECT Pessoas.Email FROM Pessoas WHERE Pessoas.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<string>(query, new { id = colaboradorId });

                connection.Close();

                return results.FirstOrDefault();

            }
        }

        private async Task<PaginatedItemsViewModel<PessoaDto>> GetColaboradoresV2(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {
            //var ativos = param.ativo;
            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT 
                        Pessoas.id, 
                        Pessoas.nome, 
                        Pessoas.email,
                        Pessoas.ativo,
                        Unidades.Sigla as unidadeSigla
                        FROM Pessoas 
                        INNER JOIN Unidades on Pessoas.UnidadeId = Unidades.Id 
                        WHERE Pessoas.TipoPessoa = 'Colaborador' AND ");
            if (param.todasUnidades == false) query.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Pessoas.Ativo = 'True' "); } else { query.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); }
            query.Append(" ORDER BY Pessoas.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) FROM Pessoas INNER JOIN Unidades on Pessoas.UnidadeId = Unidades.Id WHERE Pessoas.TipoPessoa = 'Colaborador' AND ");
            if (param.todasUnidades == false) queryCount.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Pessoas.Ativo = 'True' "); } else { queryCount.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); }


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidadeId });

                var results = await connection.QueryAsync<PessoaDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                connection.Close();

                var paginatedItems = new PaginatedItemsViewModel<PessoaDto>(currentPage, itemsPerPage, countItems, results.ToList());

                return paginatedItems;

            }
        }

        private async Task<PaginatedItemsViewModel<PessoaDto>> GetColaboradores(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {
            //var ativos = param.ativo;
            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT 
                        Colaboradores.id, 
                        Colaboradores.nome, 
                        Colaboradores.email,
                        Colaboradores.ativo,
                        Unidades.Sigla as unidadeSigla
                        FROM Colaboradores 
                        INNER JOIN Unidades on Colaboradores.UnidadeId = Unidades.Id 
                        WHERE ");
            if (param.todasUnidades == false) query.Append(" Colaboradores.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Colaboradores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Colaboradores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Colaboradores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Colaboradores.Ativo = 'True' "); } else { query.Append(" Colaboradores.Ativo = 'True' OR Colaboradores.Ativo = 'False' "); }
            query.Append(" ORDER BY Colaboradores.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) FROM Colaboradores INNER JOIN Unidades on Colaboradores.UnidadeId = Unidades.Id WHERE ");
            if (param.todasUnidades == false) queryCount.Append(" Colaboradores.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Colaboradores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Colaboradores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Colaboradores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Colaboradores.Ativo = 'True' "); } else { queryCount.Append(" Colaboradores.Ativo = 'True' OR Colaboradores.Ativo = 'False' "); }


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidadeId });

                var results = await connection.QueryAsync<PessoaDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                connection.Close();

                var paginatedItems = new PaginatedItemsViewModel<PessoaDto>(currentPage, itemsPerPage, countItems, results.ToList());

                return paginatedItems;

            }
        }

        public async Task<PessoaDto> GetColaboradoresByIdV2(Guid colaboradorId)
        {
            var query = @"SELECT * FROM Pessoas INNER JOIN Enderecos ON Pessoas.id = Enderecos.PessoaId WHERE Pessoas.id = @colaboradorId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                try
                {
                    var results = await connection.QueryAsync<PessoaDto, EnderecoDto, PessoaDto>(query,
                         map: (pessoa, endereco) =>
                         {
                             pessoa.endereco = endereco;
                             return pessoa;
                         },
                         new { colaboradorId = colaboradorId }, splitOn: "Id");

                    connection.Close();

                    return results.FirstOrDefault();

                }
                catch (Exception ex)
                {

                }

                return new PessoaDto();
            }


            //return conexao.Query<Cliente, Passaporte, Cliente>(
            //  "SELECT * " +
            //  "FROM Clientes C " +
            //  "INNER JOIN Passaportes P ON C.ClienteId = P.ClienteId " +
            //  "ORDER BY C.Nome",
            //  map: (cliente, passaporte) =>
            //  {
            //      cliente.DadosPassaporte = passaporte;
            //      return cliente;
            //  },
            // splitOn: "ClienteId,PassaporteId");
        }

        public async Task<IEnumerable<PessoaDto>> GetColaboradoresProfessoresAtivos(Guid pessoaId)
        {
            var query = @"SELECT * FROM Pessoas WHERE Pessoas.ativo = 'True' AND
                        Pessoas.tipoPessoa = 'Colaborador' OR
                        Pessoas.tipoPessoa = 'Professor' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PessoaDto>(query);

                var todosColaboradores = results.ToList();

                var pessoaEstaNaLista = results.Where(f => f.id == pessoaId);
                

                if (!pessoaEstaNaLista.Any())
                {
                    var busca = @"SELECT * FROM Pessoas WHERE Pessoas.Id = @pessoaId AND 
                                Pessoas.tipoPessoa = 'Colaborador' OR
                                Pessoas.tipoPessoa = 'Professor'";

                    var pessoa = await connection.QueryAsync<PessoaDto>(busca, new { pessoaId = pessoaId });

                    if (pessoa.Any())
                    {   
                            var pessoaAdd = pessoa.FirstOrDefault();
                        todosColaboradores.Add(pessoaAdd);
                    }
                }


                connection.Close();

                return todosColaboradores.OrderBy(c => c.nome);

            }            
        }
    }
}
