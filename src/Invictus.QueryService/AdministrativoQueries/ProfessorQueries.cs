using Dapper;
using Invictus.Core.Enumerations;
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
    public class ProfessorQueries : IProfessorQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _user;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly ITurmaQueries _turmaQueries;
        private readonly List<ProfessorDto> _professores;
        public ProfessorQueries(IAspNetUser user, IUnidadeQueries unidadeQueries, IConfiguration config,
            ITurmaQueries turmaQueries)
        {
            _user = user;
            _unidadeQueries = unidadeQueries;
            _config = config;
            _turmaQueries = turmaQueries;
            _professores = new List<ProfessorDto>();
        }

        public async Task<PaginatedItemsViewModel<ProfessorDto>> GetProfessores(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var professores = await GetProfessoresByFilter(itemsPerPage, currentPage, param);

            var profCount = await CountProfessoresByFilter(itemsPerPage, currentPage, param);

            var paginatedItems = new PaginatedItemsViewModel<ProfessorDto>(currentPage, itemsPerPage, profCount, professores.ToList());

            return paginatedItems;
        }

        private async Task<IEnumerable<ProfessorDto>> GetProfessoresByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidade = await _unidadeQueries.GetUnidadeBySigla(_user.ObterUnidadeDoUsuario());

            StringBuilder query = new StringBuilder();
            query.Append("SELECT * from Professores where ");

            if (param.nome != "") query.Append("LOWER(Professores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append("LOWER(Professores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append("LOWER(Professores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            query.Append("Professores.UnidadeId = '" + unidade.id + "'");
            if (param.ativo == false) query.Append(" AND Professores.Ativo = 'True' ");
            query.Append(" ORDER BY Professores.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");



            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                //var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidade.id });

                var results = await connection.QueryAsync<ProfessorDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                /// var paginatedItems = new PaginatedItemsViewModel<ProfessorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return results;

            }
        }

        private async Task<int> CountProfessoresByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidade = await _unidadeQueries.GetUnidadeBySigla(_user.ObterUnidadeDoUsuario());

            //StringBuilder query = new StringBuilder();
            //query.Append("SELECT * from Professores where ");
            //if (param.nome != "") query.Append("LOWER(Professores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            //if (param.email != "") query.Append("AND LOWER(Professores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            //if (param.cpf != "") query.Append("AND LOWER(Professores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            //query.Append("AND Professores.UnidadeId = " + unidade.id);
            //if (param.ativo == false) query.Append(" AND Professores.Ativo = 'True' ");
            //query.Append(" ORDER BY Professores.Nome ");
            //query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) from Professores where ");
            if (param.nome != "") queryCount.Append("LOWER(Professores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append("LOWER(Professores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append("LOWER(Professores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            queryCount.Append(" Professores.UnidadeId = '" + unidade.id + "'");
            if (param.ativo == false) queryCount.Append(" AND Professores.Ativo = 'True' ");


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidade.id });

                //var results = await connection.QueryAsync<ProfessorDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                //var paginatedItems = new PaginatedItemsViewModel<ProfessorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return countItems;

            }
        }

        public async Task<ProfessorDto> GetProfessorById(Guid professorId)
        {
            string query = @"SELECT * From Professores WHERE Professores.Id = @professorId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<ProfessorDto>(query, new { professorId = professorId });

                connection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<MateriaHabilitadaViewModel>> GetProfessoresMaterias(Guid professorId)
        {
            string query = @"select 
                            ProfessoresMaterias.id,
                            ProfessoresMaterias.PacoteMateriaId,
                            MateriasTemplate.nome,
                            TypePacote.nome as nomePacote
                            from ProfessoresMaterias
                            inner join MateriasTemplate on ProfessoresMaterias.PacoteMateriaId = MateriasTemplate.id
                            inner join TypePacote on MateriasTemplate.TypePacoteId = TypePacote.Id
                            where ProfessoresMaterias.ProfessorId = @professorId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<MateriaHabilitadaViewModel>(query, new { professorId = professorId });

                connection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<UnidadeDto>> GetProfessoresUnidadesDisponiveis(Guid professorId)
        {
            string query = @"select * from unidades 
                            where unidades.id not in (
                            select professoresDisponibilidades.unidadeId 
                            from professoresDisponibilidades 
                            where professoresDisponibilidades.PessoaId = @professorId )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<UnidadeDto>(query, new { professorId = professorId });

                connection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<DisponibilidadeView>> GetProfessorDisponibilidade(Guid professorId)
        {
            string query = @"select 
                            ProfessoresDisponibilidades.Id,
                            ProfessoresDisponibilidades.Domingo,
                            ProfessoresDisponibilidades.Segunda,
                            ProfessoresDisponibilidades.Terca,
                            ProfessoresDisponibilidades.Quarta,
                            ProfessoresDisponibilidades.Quinta,
                            ProfessoresDisponibilidades.Sexta,
                            ProfessoresDisponibilidades.Sabado,
                            ProfessoresDisponibilidades.UnidadeId,
                            ProfessoresDisponibilidades.PessoaId,
                            ProfessoresDisponibilidades.DataAtualizacao,
                            Unidades.Descricao
                            from ProfessoresDisponibilidades 
                            inner join Unidades on ProfessoresDisponibilidades.UnidadeId = Unidades.id
                            where ProfessoresDisponibilidades.PessoaId = @professorId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<DisponibilidadeView>(query, new { professorId = professorId });

                connection.Close();

                return result;
            }
        }

        #region GetProfDisponiveis
        public async Task<IEnumerable<ProfessorDto>> GetProfessoresDisponiveis(Guid turmaId)
        {
            // var turmaId = "";
            var turma = await _turmaQueries.GetTurma(turmaId);
            var turmaHorarios = await GetDiasSemanaDaTurma(turmaId);
            // parse turmaHorarios para os Bools do ProfessorDisponibilidades
            //var diasTurma = ParseDiaSemana
            // var profIds = GetProfJaAdicionadoNaTurma();
            var professores = await GetProfessoresDisponiveisNoDia(turma.unidadeId, turmaHorarios, turmaId);
            // REMOVER PROF JA ADICIONADO!!!!!!!!!!!!!!!!!!!
            return professores;
        }

        private string ParseDiaSemana(string diaSemana)
        {
            if (diaSemana == "Segunda-feira")
            {
                return "Segunda";

            }
            else if (diaSemana == "Terça-feira")
            {
                return "Terca";
            }
            else if (diaSemana == "Quarta-feira")
            {
                return "Quarta";
            }
            else if (diaSemana == "Quinta-feira")
            {
                return "Quinta";
            }
            else if (diaSemana == "Sexta-feira")
            {
                return "Sexta";
            }
            else if (diaSemana == "Sábado")
            {
                return "Sabado";
            }
            else
            {
                return "Domingo";
            }
        }

        private async Task<List<ProfessorDto>> GetProfessoresDisponiveisNoDia(Guid unidadeId, List<string> diasSemana, Guid turmaId)
        {   
            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("select professores.id, professores.Nome, professores.Email from ProfessoresDisponibilidades ");
            queryCount.Append("inner join Professores on ProfessoresDisponibilidades.PessoaId = Professores.Id where ");
            queryCount.Append(" ProfessoresDisponibilidades.UnidadeId = '" + unidadeId + "'");
            queryCount.Append(" AND Professores.ativo = 'True' ");
            queryCount.Append(" AND ProfessoresDisponibilidades.PessoaId not in ");// (select TurmasMaterias.ProfessorId from TurmasMaterias 
            queryCount.Append(" (select TurmasProfessores.ProfessorId from TurmasProfessores where TurmasProfessores.TurmaId = '" + turmaId+"') AND ");

            if (diasSemana.Count() == 1)
            {
                var stringDiaSemana = ParseDiaSemana(diasSemana[0]);
                queryCount.Append(" ProfessoresDisponibilidades."+ stringDiaSemana+" = 'True'  ");
               

            }
            else
            {
                for (int i = 0; i < diasSemana.Count(); i++)
                {
                    var stringDiaSemana = ParseDiaSemana(diasSemana[i]);
                    queryCount.Append("ProfessoresDisponibilidades." + stringDiaSemana + " = 'True'  ");
                    if(i != diasSemana.Count() - 1) queryCount.Append(" OR ");
                    //queryCount.Append(" AND ProfessoresDisponibilidades.UnidadeId = '" + unidadeId + "'");
                }

            }

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ProfessorDto>(queryCount.ToString());

                connection.Close();

                return result.ToList();
            }
        }

        private async Task<List<Guid>> GetProfessoresDaTurma(Guid turmaId)
        {
            string query = @"select 
                            TurmasMaterias.ProfessorId 
                            from TurmasMaterias 
                            where TurmasMaterias.TurmaId = @turmaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<Guid>(query, new { turmaId  = turmaId });

                connection.Close();

                return result.Distinct().ToList();
            }
        }

        private async Task<List<string>> GetDiasSemanaDaTurma(Guid turmaId)
        {
            string query = @"select 
                            TurmasHorarios.DiaSemanada
                            from TurmasHorarios 
                            where TurmasHorarios.turmaId = @turmaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<string>(query, new { turmaId = turmaId });

                connection.Close();

                return result.ToList();
            }
        }


        #endregion


    }
}