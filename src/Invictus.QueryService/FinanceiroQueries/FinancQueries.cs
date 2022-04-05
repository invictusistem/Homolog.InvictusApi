using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Invictus.Core.Extensions;

namespace Invictus.QueryService.FinanceiroQueries
{
    public class FinancQueries : IFinanceiroQueries
    {
        private readonly IConfiguration _config;
        private readonly IUnidadeQueries _unidadeQueries;
        public FinancQueries(IConfiguration config, IUnidadeQueries unidadeQueries)
        {
            _config = config;
            _unidadeQueries = unidadeQueries;
        }

        public async Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetAlunosFinanceiro(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var unidade = await _unidadeQueries.GetUnidadeDoUsuario();

            var alunos = await GetAlunosByFilter(itemsPerPage, currentPage, param, unidade.id);

            var alunosCount = await CountAlunosByFilter(itemsPerPage, currentPage, param, unidade.id);

            var paginatedItems = new PaginatedItemsViewModel<ViewMatriculadosDto>(currentPage, itemsPerPage, alunosCount, alunos.ToList());

            return paginatedItems;
        }

        public async Task<IEnumerable<ViewMatriculadosDto>> GetAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT alunos.nome, alunos.email, alunos.cpf, alunos.ativo, Matriculas.id as matriculaId, Matriculas.NumeroMatricula, unidades.descricao ");
            query.Append("FROM Matriculas inner join alunos on Matriculas.AlunoId = Alunos.Id inner join unidades on alunos.UnidadeId = unidades.Id WHERE ");
            if (param.todasUnidades == false) query.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Alunos.Ativo = 'True' "); } else { query.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); }
            query.Append(" ORDER BY Alunos.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ViewMatriculadosDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                if (results.Count() > 0)
                {
                    foreach (var aluno in results)
                    {
                        aluno.cpf = aluno.cpf.BindingCPF();
                    }
                   
                }
                connection.Close();

                return results;
            }
        }

        private async Task<int> CountAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("select Count(*) ");
            queryCount.Append("FROM Matriculas inner join alunos on Matriculas.AlunoId = Alunos.Id inner join unidades on alunos.UnidadeId = unidades.Id WHERE  ");
            if (param.todasUnidades == false) queryCount.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Alunos.Ativo = 'True' "); } else { queryCount.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); }


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString());

                connection.Close();

                return countItems;

            }
        }

        public async Task<IEnumerable<BoletoDto>> GetDebitoAlunos(Guid matriculaId)
        {

            var query = @"select * from Boletos where Boletos.InformacaoDebitoId = (
                        select id from InformacoesDebitos where InformacoesDebitos.MatriculaId = @matriculaId 
                        )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var debitos = await connection.QueryAsync<BoletoDto>(query, new { matriculaId = matriculaId });

                return debitos;

            }
        }

    }
}
