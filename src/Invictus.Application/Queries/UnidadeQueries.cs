using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Queries
{
    public class UnidadeQueries : IUnidadeQueries
    {
        private readonly IConfiguration _config;
        public UnidadeQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<string>> GetMateriasDoCurso(int moduloId)
        {
            var query = @"select descricao from materias where moduloId = @moduloId";
            /* select * from agendaprovas where turmaId = 1070*/
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var materias = await connection.QueryAsync<string>(query, new { moduloId = moduloId });

                return materias.ToList();
            }
        }

        public async Task<List<MateriaDto>> GetMaterias(int moduloId)
        {
            var query = @"select * from materias where moduloId = @moduloId";
            /* select * from agendaprovas where turmaId = 1070*/
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var materias = await connection.QueryAsync<MateriaDto>(query, new { moduloId = moduloId });

                return materias.ToList();
            }
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetProdutosViewModel()
        {
            var query = @"select 
                        produto.Id, 
                        produto.Nome,
                        produto.preco,
                        produto.quantidade,
                        produto.NivelMinimo,
                        unidade.sigla as unidade 
                        from Produto 
                        inner join unidade on 
                        Produto.UnidadeId = Unidade.Id";
            /* select * from agendaprovas where turmaId = 1070*/
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var produtos = await connection.QueryAsync<ProdutoViewModel>(query);

                return produtos;
            }
        }

        public async Task<PaginatedItemsViewModel<ProfessorDto>> GetProfessores(int itemsPerPage, int currentPage, ParametrosDTO param, int unidadeId)
        {
            //var ativos = param.ativo;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * from Professores where ");
            if (param.nome != "") query.Append("LOWER(Professores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") query.Append("AND LOWER(Professores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") query.Append("AND LOWER(Professores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            query.Append("AND Professores.UnidadeId = " + unidadeId);
            if (param.ativo == false) query.Append(" AND Professores.Ativo = 'True' ");
            query.Append(" ORDER BY Professores.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) from Professores where ");
            if (param.nome != "") queryCount.Append("LOWER(Professores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") queryCount.Append("AND LOWER(Professores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") queryCount.Append("AND LOWER(Professores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            queryCount.Append("AND Professores.UnidadeId = " + unidadeId);
            if (param.ativo == false) queryCount.Append(" AND Professores.Ativo = 'True' ");


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidadeId });

                var results = await connection.QueryAsync<ProfessorDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                var paginatedItems = new PaginatedItemsViewModel<ProfessorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return paginatedItems;

            }
        }
    }
}
