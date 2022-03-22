using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Invictus.Dtos.AdmDtos.Utils;
using System.Text.Json;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.Utilitarios.Interface;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class AlunoQueries : IAlunoQueries
    {
        private readonly IConfiguration _config;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IUtils _utils;
        public AlunoQueries(IConfiguration config, IUnidadeQueries unidadeQueries, IUtils utils)
        {
            _config = config;
            _unidadeQueries = unidadeQueries;
            _utils = utils;
        }
        public async Task<IEnumerable<AlunoDto>> FindAlunoByCPForEmailorRG(string cpf, string rg, string email)
        {
            var query = "SELECT * from Alunos where LOWER(Alunos.cpf) like LOWER('" + cpf + "') collate SQL_Latin1_General_CP1_CI_AI " +
                "OR LOWER(Alunos.rg) like LOWER('" + rg + "') collate SQL_Latin1_General_CP1_CI_AI " +
                "OR LOWER(Alunos.email) like LOWER('" + email + "') collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<AlunoDto>(query);

                connection.Close();

                return results;

            }
        }

        private async Task<IEnumerable<Guid>> GetIdsAlunosDaBase(ParametrosDTO param, Guid unidadeId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT Alunos.id from Alunos inner join unidades on alunos.UnidadeId = unidades.Id where ");
            if (param.todasUnidades == false) query.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) query.Append(" Alunos.Ativo = 'True' ");
            query.Append(" ORDER BY Alunos.Nome ");
            //query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<Guid>(query.ToString());

                connection.Close();

                return results;

            }
        }

        public async Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetMatriculadosView(int itemsPerPage, int currentPage, string paramsJson)
        {
            //var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);
            //var uindade = await _unidadeQueries.GetUnidadeDoUsuario();


            // var alunosNaBase = await GetIdsAlunosDaBase(param, uindade.id);

            // get alunos na Table Alunos
            /*
             lista Alunos
             aluno.temMatricula ? sim =

            aluno.temMatricula ?
              
             */
            /*
             select alunos.id,
matriculas.numeromatricula, 
matriculas.cpf, 
alunos.nome,
matriculas.id as matriculaId,
unidades.sigla
from alunos
full join matriculas on alunos.id = matriculas.alunoid
inner join Unidades on Alunos.UnidadeId = Unidades.Id
WHERE Alunos.nome = 'Mario Gomes'
             */

            // old
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);
            var unidade = await _unidadeQueries.GetUnidadeDoUsuario();

            var alunos = await GetAlunosByFilter(itemsPerPage, currentPage, param, unidade.id);

            var alunosCount = await CountAlunosByFilter(itemsPerPage, currentPage, param, unidade.id);

            var paginatedItems = new PaginatedItemsViewModel<ViewMatriculadosDto>(currentPage, itemsPerPage, alunosCount, alunos.ToList());

            return paginatedItems;
        }

        private async Task<int> CountAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("select Count(*) ");
            queryCount.Append("from alunos full join matriculas on alunos.id = matriculas.alunoid inner join Unidades on Alunos.UnidadeId = Unidades.Id WHERE ");
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

        public async Task<IEnumerable<ViewMatriculadosDto>> GetAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("select alunos.id, matriculas.numeromatricula, alunos.cpf, alunos.rg, alunos.nascimento, alunos.dataCadastro, alunos.nome, alunos.ativo, matriculas.id as matriculaId, unidades.sigla ");
            query.Append("from alunos full join matriculas on alunos.id = matriculas.alunoid inner join Unidades on Alunos.UnidadeId = Unidades.Id WHERE ");
            if (param.todasUnidades == false) query.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Alunos.Ativo = 'True' "); }else { query.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); }
            query.Append(" ORDER BY Alunos.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ViewMatriculadosDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                if (results.Count() > 0)
                    results = BindCPF(results.ToList());

                connection.Close();

                return results;
            }
        }
        public List<ViewMatriculadosDto> BindCPF(List<ViewMatriculadosDto> alunos)
        {

            foreach (var item in alunos)
            {

                string substr = item.cpf.Substring(6, 3);
                item.cpf = "******." + substr + "-**";
            }

            return alunos;

        }

        public async Task<IEnumerable<AlunoDto>> SearchPerCPF(string cpf)
        {
            var query = "SELECT * from Alunos where LOWER(Alunos.cpf) like LOWER('" + cpf + "') " +
                        "collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<AlunoDto>(query);

                connection.Close();

                return results;

            }
        }

        public async Task<DateTime> GetIdadeAluno(Guid alunoId)
        {
            var query = "SELECT Alunos.nascimento from Alunos where Alunos.Id = @alunoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<DateTime>(query, new { alunoId = alunoId });

                connection.Close();

                return results;

            }
        }

        public async Task<AlunoDto> GetAlunoById(Guid alunoId)
        {
            var query = "SELECT * from Alunos where Alunos.Id = @alunoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<AlunoDto>(query, new { alunoId = alunoId });

                connection.Close();

                return results;

            }
        }

        public async Task<AlunoDto> GetAlunoByMatriculaId(Guid matriculaId)
        {
            var query = @"select* from alunos Where alunos.id = (
                        select matriculas.alunoId from matriculas where matriculas.id = @matriculaId 
                        )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<AlunoDto>(query, new { matriculaId = matriculaId });

                connection.Close();

                return results;

            }
        }

        public async Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetSomenteMatriculadosView(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);
            var unidade = await _unidadeQueries.GetUnidadeDoUsuario();

            var alunos = await GetMatriculadosByFilter(itemsPerPage, currentPage, param, unidade.id);

            var alunosCount = await CountMatriculadosByFilter(itemsPerPage, currentPage, param, unidade.id);

            var paginatedItems = new PaginatedItemsViewModel<ViewMatriculadosDto>(currentPage, itemsPerPage, alunosCount, alunos.ToList());

            return paginatedItems;
        }

        private async Task<int> CountMatriculadosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("select Count(*) ");
            queryCount.Append("from Matriculas left join alunos on Matriculas.AlunoId = Alunos.Id left join Unidades on alunos.UnidadeId = Unidades.Id where ");
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

        public async Task<IEnumerable<ViewMatriculadosDto>> GetMatriculadosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append(@"select 
                        alunos.nome, 
                        alunos.CPF, 
                        alunos.email,
                        matriculas.NumeroMatricula,
                        matriculas.id as matriculaId, 
                        matriculas.status, 
                        Matriculas.MatriculaConfirmada,
                        Turmas.Descricao as turmaDescricao,
                        turmas.Identificador as turmaIdentificador, 
                        Unidades.Sigla, 
                        AspNetUserClaims.claimValue as acessoSistema ");

            query.Append(@"from Matriculas 
                        left join alunos on Matriculas.AlunoId = Alunos.Id 
                        left join Turmas on Matriculas.turmaId = Turmas.id 
                        left join Unidades on alunos.UnidadeId = Unidades.Id 
                        left join AspNetUsers on Alunos.Email = AspNetUsers.Email
                        left join AspNetUserClaims on AspNetUsers.Id = AspNetUserClaims.UserId where  ");

            query.Append(" AspNetUserClaims.ClaimType = 'IsActive' AND ");
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
                    results = BindCPF(results.ToList());

                connection.Close();

                return results;
            }
        }

        public async Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetAllMatriculadosView(int itemsPerPage, int currentPage, string paramsJson)
        {            

            // old
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);
            var unidade = await _unidadeQueries.GetUnidadeDoUsuario();

            var alunos = await GetAllAlunosByFilter(itemsPerPage, currentPage, param, unidade.id);

            var alunosCount = await CountAllAlunosByFilter(itemsPerPage, currentPage, param, unidade.id);

            var paginatedItems = new PaginatedItemsViewModel<ViewMatriculadosDto>(currentPage, itemsPerPage, alunosCount, alunos.ToList());

            return paginatedItems;
        }

        private async Task<int> CountAllAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("select Count(*) ");
            queryCount.Append("from alunos full join matriculas on alunos.id = matriculas.alunoid inner join Unidades on Alunos.UnidadeId = Unidades.Id WHERE ");
            if (param.todasUnidades == false) queryCount.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
           // if (param.nome != "") queryCount.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
           // if (param.email != "") queryCount.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
           // if (param.cpf != "") queryCount.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            queryCount.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); 


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString());

                connection.Close();

                return countItems;

            }
        }

        public async Task<IEnumerable<ViewMatriculadosDto>> GetAllAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("select alunos.id, matriculas.numeromatricula, alunos.cpf, alunos.rg, alunos.nascimento, alunos.dataCadastro, alunos.nome, alunos.ativo, matriculas.id as matriculaId, unidades.sigla ");
            query.Append("from alunos full join matriculas on alunos.id = matriculas.alunoid inner join Unidades on Alunos.UnidadeId = Unidades.Id WHERE ");
            if (param.todasUnidades == false) query.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            //if (param.nome != "") query.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
           // if (param.email != "") query.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
          //  if (param.cpf != "") query.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            query.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); 
            query.Append(" ORDER BY Alunos.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ViewMatriculadosDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                if (results.Count() > 0)
                    results = BindCPF(results.ToList());

                connection.Close();

                return results;
            }
        }
    }
}
