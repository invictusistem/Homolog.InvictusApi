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
        public async Task<IEnumerable<PessoaDto>> FindAlunoByCPForEmailorRG(string cpf, string rg, string email)
        {
            var query = "SELECT * FROM Pessoas WHERE LOWER(Pessoas.cpf) like LOWER('" + cpf + "') collate SQL_Latin1_General_CP1_CI_AI " +
                "OR LOWER(Pessoas.rg) like LOWER('" + rg + "') collate SQL_Latin1_General_CP1_CI_AI " +
                "OR LOWER(Pessoas.email) like LOWER('" + email + "') collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PessoaDto>(query);

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

        public async Task<IEnumerable<PessoaDto>> SearchPerCPF(string cpf)
        {
            var query = "SELECT * FROM Pessoas WHERE LOWER(Alunos.cpf) like LOWER('" + cpf + "') " +
                        "collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PessoaDto>(query);

                connection.Close();

                return results;

            }
        }

        public async Task<DateTime> GetIdadeAluno(Guid alunoId)
        {
            var query = "SELECT Pessoas.nascimento FROM Pessoas WHERE Pessoas.Id = @alunoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<DateTime>(query, new { alunoId = alunoId });

                connection.Close();

                return results;

            }
        }

        public async Task<PessoaDto> GetAlunoById(Guid alunoId)
        {
           
            var query = "SELECT * FROM Pessoas INNER JOIN Enderecos ON Pessoas.id = Enderecos.PessoaId WHERE Pessoas.Id = @alunoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PessoaDto, EnderecoDto, PessoaDto>(query,
                    map: (pessoa, endereco) =>
                    {
                        pessoa.endereco = endereco;
                        return pessoa;
                    },
                    new { alunoId = alunoId },
                    splitOn: "Id");

                connection.Close();

                return results.FirstOrDefault();

            }
        }

        public async Task<PessoaDto> GetAlunoByMatriculaId(Guid matriculaId)
        {
            var query = @"select* from Pessoas Where Pessoas.id = (
                        select matriculas.alunoId from matriculas where matriculas.id = @matriculaId 
                        )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<PessoaDto>(query, new { matriculaId = matriculaId });

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
            queryCount.Append("from Matriculas left join Pessoas on Matriculas.AlunoId = Pessoas.Id left join Unidades on Pessoas.UnidadeId = Unidades.Id where ");
            if (param.todasUnidades == false) queryCount.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Pessoas.Ativo = 'True' "); } else { queryCount.Append(" (Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False') "); }


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
                        Pessoas.nome, 
                        Pessoas.CPF, 
                        Pessoas.email,
                        matriculas.NumeroMatricula,
                        matriculas.id as matriculaId, 
                        matriculas.status, 
                        Matriculas.MatriculaConfirmada,
                        Turmas.Descricao as turmaDescricao,
                        turmas.Identificador as turmaIdentificador, 
                        Unidades.Sigla, 
                        AspNetUserClaims.claimValue as acessoSistema ");

            query.Append(@"from Matriculas 
                        left join Pessoas on Matriculas.AlunoId = Pessoas.Id 
                        left join Turmas on Matriculas.turmaId = Turmas.id 
                        left join Unidades on Pessoas.UnidadeId = Unidades.Id 
                        left join AspNetUsers on Pessoas.Email = AspNetUsers.Email
                        left join AspNetUserClaims on AspNetUsers.Id = AspNetUserClaims.UserId where  ");

            query.Append(" AspNetUserClaims.ClaimType = 'IsActive' AND ");
            if (param.todasUnidades == false) query.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Pessoas.Ativo = 'True' "); } else { query.Append(" (Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False') "); }
            query.Append(" ORDER BY Pessoas.Nome ");
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
            queryCount.Append(@"from Pessoas full join matriculas on alunos.id = matriculas.alunoid inner join Unidades on Alunos.UnidadeId = Unidades.Id 
                              WHERE Pessoas.tipoPessoa = 'Aluno' AND ");
            if (param.todasUnidades == false) queryCount.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
          
            queryCount.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); 


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
            query.Append(@"select Pessoas.id, matriculas.numeromatricula, Pessoas.cpf, Pessoas.rg, Pessoas.nascimento, Pessoas.dataCadastro, Pessoas.nome, 
                        Pessoas.ativo, matriculas.id as matriculaId, unidades.sigla ");
            query.Append(@"from Pessoas full join matriculas on Pessoas.id = matriculas.alunoid inner join Unidades on Pessoas.UnidadeId = Unidades.Id WHERE 
                         Pessoas.tipoPessoa = 'Aluno' AND ");
            if (param.todasUnidades == false) query.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
          
            query.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); 
            query.Append(" ORDER BY Pessoas.Nome ");
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

        public async Task<IEnumerable<PessoaDto>> SearchPerCPFV2(string cpf)
        {
            var query = "SELECT * FROM Pessoas WHERE LOWER(Pessoas.cpf) like LOWER('" + cpf + "') " +
                        "collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PessoaDto>(query);

                connection.Close();

                return results;

            }
        }

        public async Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetMatriculadosViewV2(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var unidade = await _unidadeQueries.GetUnidadeDoUsuario();

            var alunos = await GetAlunosByFilterV2(itemsPerPage, currentPage, param, unidade.id);

            var alunosCount = await CountAlunosByFilterV2(itemsPerPage, currentPage, param, unidade.id);

            var paginatedItems = new PaginatedItemsViewModel<ViewMatriculadosDto>(currentPage, itemsPerPage, alunosCount, alunos.ToList());

            return paginatedItems;
        }

        private async Task<int> CountAlunosByFilterV2(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("select Count(*) ");
            queryCount.Append("from Pessoas full join matriculas on Pessoas.id = matriculas.alunoid inner join Unidades on Pessoas.UnidadeId = Unidades.Id WHERE Pessoas.tipoPessoa = 'Aluno' AND ");
            if (param.todasUnidades == false) queryCount.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Pessoas.Ativo = 'True' "); } else { queryCount.Append(" (Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False') "); }


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString());

                connection.Close();

                return countItems;

            }
        }

        public async Task<IEnumerable<ViewMatriculadosDto>> GetAlunosByFilterV2(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("select Pessoas.id, matriculas.numeromatricula, Pessoas.cpf, Pessoas.rg, Pessoas.nascimento, Pessoas.dataCadastro, Pessoas.nome, Pessoas.ativo, matriculas.id as matriculaId, unidades.sigla ");
            query.Append("from Pessoas full join matriculas on Pessoas.id = matriculas.alunoid inner join Unidades on Pessoas.UnidadeId = Unidades.Id WHERE Pessoas.tipoPessoa = 'Aluno' AND ");
            if (param.todasUnidades == false) query.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Pessoas.Ativo = 'True' "); } else { query.Append(" (Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False') "); }
            query.Append(" ORDER BY Pessoas.Nome ");
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
