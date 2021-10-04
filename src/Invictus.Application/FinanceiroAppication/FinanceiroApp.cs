using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Application.FinanceiroAppication.interfaces;
using Invictus.Domain.Financeiro.NewFolder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.FinanceiroAppication
{
    public class FinanceiroApp : IFinanceiroApp
    {
        private readonly IConfiguration _config;
        public FinanceiroApp(IConfiguration config)
        {
            _config = config;
        }
        public void addProduct(Produto newProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FornecedorDto>> SearchFornecedor(QueryDto queryDto)
        {
            var query = "SELECT * from Fornecedores where " + 
                        "LOWER(Fornecedores.RazaoSocial) like LOWER('%" + queryDto.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
                        "LOWER(Fornecedores.email) like LOWER('%" + queryDto.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
                        "LOWER(Fornecedores.cnpj_cpf) like LOWER('%" + queryDto.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI " +
                        "ORDER BY Fornecedores.RazaoSocial ";// +
                                               //"OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY";


            // var query = "select * from aluno where aluno.nome like '%"+nome+"%' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<FornecedorDto>(query);

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return results;

            }
        }

        public async Task<IEnumerable<ProdutoDto>> SearchProduct(string nome, int unidadeId)
        {
            var query = "SELECT * from Produto where LOWER(Produto.nome) like LOWER('%" + nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND "+
                        "Produto.UnidadeId = @unidadeId " +
                        "ORDER BY Produto.Nome ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var products = await connection.QueryAsync<ProdutoDto>(query, new { unidadeId = unidadeId });

                return products;
                
            }
        }
    }
}
