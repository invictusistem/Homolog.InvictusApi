using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class ProdutoQueries : IProdutoQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        public ProdutoQueries(IConfiguration config, IAspNetUser aspNetUser)
        {
            _config = config;
            _aspNetUser = aspNetUser;
        }
        public async Task<ProdutoDto> GetProdutobyId(Guid produtoId)
        {
            var query = "SELECT * from Produtos where Produtos.Id = @produtoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<ProdutoDto>(query, new { produtoId = produtoId });

                connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<ProdutoDto>> GetProdutos()
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            var query = @"SELECT 
                        produtos.Id, 
                        produtos.Nome,
                        produtos.preco,
                        produtos.quantidade,
                        produtos.NivelMinimo,
                        unidades.sigla as Observacoes 
                        FROM Produtos 
                        INNER JOIN unidades ON 
                        Produtos.UnidadeId = Unidades.Id 
                        WHERE Produtos.unidadeId = @unidadeId";
            
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var produtos = await connection.QueryAsync<ProdutoDto>(query, new { unidadeId = unidadeId });

                return produtos;
            }
        }

        public async Task<int> ProdutosCount()
        {
            string query = @"SELECT Count(*) FROM Produtos";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var count = await connection.QuerySingleAsync<int>(query);

                connection.Close();

                return count;
            }
        }

        public async Task<IEnumerable<string>> SearchProductByName(string nome)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            string query = @"SELECT Produtos.nome FROM Produtos WHERE 
                            LOWER(Produtos.nome) like LOWER('%" + nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
                            "Produtos.unidadeId = @unidadeId";            

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var names = await connection.QueryAsync<string>(query, new { unidadeId  = unidadeId });

                connection.Close();

                return names;
            }
        }
    }
}
