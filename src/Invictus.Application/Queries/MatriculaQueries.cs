using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using Invictus.Core;
using System.Text;

namespace Invictus.Application.Queries
{
    public class MatriculaQueries : IMatriculaQueries
    {
        private readonly IConfiguration _config;
        public MatriculaQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<string>> SearchEmail(string email)
        {
            var query = "select email from aluno " +
                        "where LOWER(aluno.email) like LOWER('%" + email + "%') collate SQL_Latin1_General_CP1_CI_AI " +
                        "union " +
                        "select email from colaborador " +
                        "where LOWER(colaborador.email) like LOWER('%" + email + "%') collate SQL_Latin1_General_CP1_CI_AI " +
                        "union " +
                        "select email from responsaveis " +
                        "where LOWER(responsaveis.email) like LOWER('%" + email + "%') collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<string>(query, new { email = email });
                connection.Close();

                return results;

            }
        }

        public async Task<IEnumerable<string>> SearchCPF(string cpf)
        {
            var query = "select email from aluno " +
                        "where LOWER(aluno.cpf) like LOWER('%" + cpf + "%') collate SQL_Latin1_General_CP1_CI_AI " +
                        "union " +
                        "select email from colaborador " +
                        "where LOWER(colaborador.cpf) like LOWER('%" + cpf + "%') collate SQL_Latin1_General_CP1_CI_AI " +
                        "union " +
                        "select email from responsaveis " +
                        "where LOWER(responsaveis.cpf) like LOWER('%" + cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<string>(query);
                connection.Close();

                return results;

            }
        }

        //public async Task<PaginatedItemsViewModel<ColaboradorDto>> GetColaboradores(int itemsPerPage, int currentPage, ParametrosDTO param, int unidadeId)
        public async Task<PaginatedItemsViewModel<AlunoDto>> BuscaAlunos(int itemsPerPage, int currentPage, ParametrosDTO param, int unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT Aluno.id, Aluno.nome, aluno.cpf, unidade.sigla as Bairro from Aluno inner join unidade on aluno.UnidadeCadastrada = unidade.Id where ");
            if (param.nome != "") query.Append("LOWER(Aluno.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") query.Append("AND LOWER(Aluno.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") query.Append("AND LOWER(Aluno.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");

            if (param.todasUnidades == false) query.Append("AND Aluno.UnidadeCadastrada = " + unidadeId);

            if (param.ativo == false) query.Append(" AND Aluno.Ativo = 'True' ");

            query.Append(" ORDER BY Aluno.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) from Aluno where ");
            if (param.nome != "") queryCount.Append("LOWER(Aluno.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") queryCount.Append("AND LOWER(Aluno.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") queryCount.Append("AND LOWER(Aluno.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");

            if (param.todasUnidades == false) queryCount.Append("AND Aluno.UnidadeCadastrada = " + unidadeId);

            if (param.ativo == false) queryCount.Append(" AND Aluno.Ativo = 'True' ");


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

        public async Task<IEnumerable<AlunoDto>> BuscarLeads(string email = null, string cpf = null, string nome = null)
        {
            var query = "";

            if (email != null & cpf != null & nome != null) { query = "select* from leads where leads.Email like '%@" + email + "%' or leads.cpf like '%" + cpf + "%' or leads.nome like '%" + nome + "%'"; }
            else if (email != null & cpf != null & nome == null) { query = "select* from leads where leads.Email like '%" + email + "%' or leads.cpf like '%" + cpf + "%'"; }
            else if (email != null & cpf == null & nome == null) { query = "select* from leads where leads.Email like '%" + email + "%' "; }
            else if (email == null & cpf == null & nome != null) { query = "select* from leads where leads.nome like '%" + nome + "%'"; }
            else if (email == null & cpf != null & nome != null) { query = "select* from leads where leads.cpf like '%" + cpf + "%' or leads.nome like '%" + nome + "%'"; }
            else if (email != null & cpf == null & nome != null) { query = "select* from leads where leads.Email like '%" + email + "%' or leads.nome like '%" + nome + "%'"; }
            else if (email == null & cpf != null & nome == null) { query = "select* from leads where leads.cpf like '%" + cpf + "%'"; }



            //var query = "SELECT* from Colaborador ORDER BY Colaborador.Nome OFFSET(@currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY";
            //var queryCount = "select count(*) from Colaborador";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<AlunoDto>(query);

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return results;

            }
        }
    }
}
