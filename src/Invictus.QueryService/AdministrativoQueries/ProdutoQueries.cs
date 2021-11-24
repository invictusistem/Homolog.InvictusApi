using Dapper;
using Invictus.Dtos.AdmDtos;
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
    public class ProdutoQueries : IProdutoQueries
    {
        private readonly IConfiguration _config;
        
        public ProdutoQueries(IConfiguration config)
        {
            _config = config;
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
            var query = @"select 
                        produtos.Id, 
                        produtos.Nome,
                        produtos.preco,
                        produtos.quantidade,
                        produtos.NivelMinimo,
                        unidades.sigla as Observacoes 
                        from Produtos 
                        inner join unidades on 
                        Produtos.UnidadeId = Unidades.Id";
            /* select * from agendaprovas where turmaId = 1070*/
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var produtos = await connection.QueryAsync<ProdutoDto>(query);

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
    }
}
