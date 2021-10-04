using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Financeiro;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invictus.Application.Queries
{
    public class FinanceiroQueries : IFinanceiroQueries
    {
        private readonly IConfiguration _config;
        public FinanceiroQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<DebitoDto>> GetDebitoAlunos(int alunoId, int turmaId)
        {
            var query = "select * from debito where debito.infofinancId = " +
                        "(select id from infofinanceira where infofinanceira.alunoId = @alunoId)";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var debitos = await connection.QueryAsync<DebitoDto>(query, new { alunoId = alunoId });

                return debitos;

            }
        }

        // public async Task<PaginatedItemsViewModel<AlunoDto>> BuscaAlunos(int itemsPerPage, int currentPage, ParametrosDTO param, int unidadeId)
        public async Task<PaginatedItemsViewModel<AlunoDto>> GetAlunoFin(int itemsPerPage, int currentPage, ParametrosDTO param, int unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT * from Aluno where ");
            if (param.nome != "") query.Append("LOWER(Aluno.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") query.Append("AND LOWER(Aluno.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") query.Append("AND LOWER(Aluno.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            query.Append("AND Aluno.UnidadeCadastrada = " + unidadeId);
            if (param.ativo == false) query.Append(" AND Aluno.Ativo = 'True' ");
            query.Append(" ORDER BY Aluno.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) from Aluno where ");
            if (param.nome != "") queryCount.Append("LOWER(Aluno.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") queryCount.Append("AND LOWER(Aluno.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") queryCount.Append("AND LOWER(Aluno.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            queryCount.Append("AND Aluno.UnidadeCadastrada = " + unidadeId);
            if (param.ativo == false) queryCount.Append(" AND Aluno.Ativo = 'True' ");



            //var query = "SELECT * from Aluno " +
            //            //"Debito.Competencia from aluno " +
            //            //"left join InfoFinanceira on aluno.id = InfoFinanceira.alunoId " +
            //            //"left join Debito on InfoFinanceira.id = Debito.InfoFinancId " +
            //            "where LOWER(aluno.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
            //            "LOWER(aluno.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
            //            "LOWER(aluno.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI " +
            //            "ORDER BY aluno.nome ";// +
            //                                   //"OFFSET(@currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY ";
            //                                   //var query = @"SELECT id, nome, email, perfil, perfilAtivo from Colaborador 
            //                                   //            where colaborador.perfil IS NOT NULL AND 
            //                                   //            Colaborador.Ativo = 'True' ORDER BY Colaborador.Nome   
            //                                   //            OFFSET(@currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY";

            //var queryCount = "select count(*) from Colaborador";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidadeId });

                var results = await connection.QueryAsync<AlunoDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                var paginatedItems = new PaginatedItemsViewModel<AlunoDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return paginatedItems;

            }
        }

        public async Task<IEnumerable<string>> GetUsuarios(QueryDto param)
        {
            //var query = "SELECT Colaborador.id, Colaborador.nome, Colaborador.email, Colaborador.perfil, Colaborador.perfilAtivo from Colaborador " +
            //           "inner join AspNetUsers on Colaborador.Email = AspNetUsers.Email " +
            //           "where LOWER(Colaborador.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
            //           "LOWER(Colaborador.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
            //           "LOWER(Colaborador.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI " +
            //           "ORDER BY Colaborador.nome " +
            //           "OFFSET(@currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY";

            var query = "SELECT aluno.nome, InfoFinanceira.alunoId, InfoFinanceira.turmaId,Debito.Status, " +
                        "Debito.Competencia from aluno " +
                        "left join InfoFinanceira on aluno.id = InfoFinanceira.alunoId " +
                        "left join Debito on InfoFinanceira.id = Debito.InfoFinancId " +
                        "where LOWER(aluno.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
                        "LOWER(aluno.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
                        "LOWER(aluno.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI " +
                        "ORDER BY aluno.nome ";// +
                                               //"OFFSET(@currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY ";
                                               //var query = @"SELECT id, nome, email, perfil, perfilAtivo from Colaborador 
                                               //            where colaborador.perfil IS NOT NULL AND 
                                               //            Colaborador.Ativo = 'True' ORDER BY Colaborador.Nome   
                                               //            OFFSET(@currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY";

            //var queryCount = "select count(*) from Colaborador";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<dynamic>(query);

                var json = JsonSerializer.Serialize(results);
                //return "";

                throw new NotImplementedException();

            }
        }

        public async Task<IEnumerable<DebitoDto>> GetBalancoCursos(string start, string end, int unidadeId)
        {

            var query = "select * from Debito where " +
            "Debito.DataPagamento >= @start AND Debito.DataPagamento <= @end AND " +
            "Debito.IdUnidadeResponsavel = @unidadeId AND " +
            "Debito.Status = 'Pago' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<DebitoDto>(query, new { start = start, end = end, unidadeId = unidadeId });

                connection.Close();

                return results;
            }

        }

        public async Task<IEnumerable<BalancoProdutosViewModel>> GetBalancoProdutos(string start, string end, int unidadeId)
        {
            var query = @"select 
                        Produto.Nome,
                        ProdutosVenda.Quantidade, 
                        ProdutosVenda.ValorUnitario, 
                        ProdutosVenda.ValorTotal,
                        VendaProduto.DataVenda,
                        VendaProduto.MeioPagamento,
                        VendaProduto.Parcelas
                        from ProdutosVenda
                        left join Produto on ProdutosVenda.ProdutoId = Produto.Id
                        left join VendaProduto on ProdutosVenda.VendaProdutoAggregateId = VendaProduto.Id
                        where VendaProduto.DataVenda >= @start AND VendaProduto.DataVenda <= @end AND
                        VendaProduto.UnidadeId = @unidadeId  ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<BalancoProdutosViewModel>(query, new { start = start, end = end, unidadeId = unidadeId });

                connection.Close();

                return results;
            }
        }
    }
}

