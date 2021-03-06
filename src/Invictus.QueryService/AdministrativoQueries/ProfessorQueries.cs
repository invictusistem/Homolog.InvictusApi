using Dapper;
using Invictus.Core.Enumerations;
using Invictus.Core.Extensions;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using MoreLinq;
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
        private readonly List<PessoaDto> _professores;
        public ProfessorQueries(IAspNetUser user, IUnidadeQueries unidadeQueries, IConfiguration config,
            ITurmaQueries turmaQueries)
        {
            _user = user;
            _unidadeQueries = unidadeQueries;
            _config = config;
            _turmaQueries = turmaQueries;
            _professores = new List<PessoaDto>();
        }

        public async Task<PaginatedItemsViewModel<PessoaDto>> GetProfessores(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var professores = await GetProfessoresByFilter(itemsPerPage, currentPage, param);

            var profCount = await CountProfessoresByFilter(itemsPerPage, currentPage, param);

            var paginatedItems = new PaginatedItemsViewModel<PessoaDto>(currentPage, itemsPerPage, profCount, professores.ToList());

            return paginatedItems;
        }

        public async Task<PaginatedItemsViewModel<PessoaDto>> GetProfessoresV2(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var professores = await GetProfessoresByFilterV2(itemsPerPage, currentPage, param);

            var profCount = await CountProfessoresByFilterV2(itemsPerPage, currentPage, param);

            var paginatedItems = new PaginatedItemsViewModel<PessoaDto>(currentPage, itemsPerPage, profCount, professores.ToList());

            return paginatedItems;
        }

        private async Task<IEnumerable<PessoaDto>> GetProfessoresByFilterV2(int itemsPerPage, int currentPage, ParametrosDTO param)
        {

            var unidadeId = _user.GetUnidadeIdDoUsuario();

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT
                        Pessoas.Id,
                        Pessoas.Nome,
                        Pessoas.Email,
                        Pessoas.Ativo,
                        Unidades.sigla as unidadeSigla
                        FROM Pessoas INNER JOIN Unidades on Pessoas.UnidadeId = Unidades.Id AND ");
            if (param.todasUnidades == false) query.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            //query.Append("Professores.UnidadeId = '" + unidade.id + "'");
            if (param.ativo == false) { query.Append(" Pessoas.Ativo = 'True' "); } else { query.Append(" (Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False') "); }
            query.Append(" AND Pessoas.tipoPessoa = 'Professor' ");
            query.Append(" ORDER BY Pessoas.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");



            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                //var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidade.id });

                var results = await connection.QueryAsync<PessoaDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                /// var paginatedItems = new PaginatedItemsViewModel<ProfessorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return results;

            }
        }

        private async Task<IEnumerable<PessoaDto>> GetProfessoresByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidade = await _unidadeQueries.GetUnidadeBySigla(_user.ObterUnidadeDoUsuario());

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT
                        Pessoas.Id,
                        Pessoas.Nome,
                        Pessoas.Email,
                        Pessoas.Ativo,
                        Unidades.sigla as unidadeSigla
                        FROM Pessoas INNER JOIN Unidades on Pessoas.UnidadeId = Unidades.Id WHERE Pessoas.tipoPessoa = 'Aluno' AND ");
            if (param.todasUnidades == false) query.Append(" Pessoas.UnidadeId = '" + unidade.id + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            //query.Append("Professores.UnidadeId = '" + unidade.id + "'");
            if (param.ativo == false) { query.Append(" Pessoas.Ativo = 'True' "); } else { query.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); }
            query.Append(" ORDER BY Pessoas.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");



            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                //var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidade.id });

                var results = await connection.QueryAsync<PessoaDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                /// var paginatedItems = new PaginatedItemsViewModel<ProfessorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return results;

            }
        }

        private async Task<int> CountProfessoresByFilterV2(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidade = await _unidadeQueries.GetUnidadeBySigla(_user.ObterUnidadeDoUsuario());

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) FROM Pessoas INNER JOIN Unidades on Pessoas.UnidadeId = Unidades.Id WHERE Pessoas.tipoPessoa = 'Professor' AND ");
            if (param.todasUnidades == false) queryCount.Append(" Pessoas.UnidadeId = '" + unidade.id + "' AND ");
            if (param.nome != "") queryCount.Append("LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append("LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append("LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Pessoas.Ativo = 'True' "); } else { queryCount.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); }
            //if (param.ativo == false) queryCount.Append(" AND Professores.Ativo = 'True' ");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidade.id });

                connection.Close();

                return countItems;

            }
        }

        private async Task<int> CountProfessoresByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            var unidade = await _unidadeQueries.GetUnidadeBySigla(_user.ObterUnidadeDoUsuario());

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) FROM Professores INNER JOIN Unidades on Professores.UnidadeId = Unidades.Id WHERE ");
            if (param.todasUnidades == false) queryCount.Append(" Professores.UnidadeId = '" + unidade.id + "' AND ");
            if (param.nome != "") queryCount.Append("LOWER(Professores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append("LOWER(Professores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append("LOWER(Professores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Professores.Ativo = 'True' "); } else { queryCount.Append(" Professores.Ativo = 'True' OR Professores.Ativo = 'False' "); }
            //if (param.ativo == false) queryCount.Append(" AND Professores.Ativo = 'True' ");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidade.id });

                connection.Close();

                return countItems;

            }
        }

        public async Task<IEnumerable<ProfessorCalendarioViewModel>> GetProfessorCalendario(Guid professorId)
        {
            var query = @"SELECT 
                        calendarios.Id,
                        calendarios.diaaula,
                        calendarios.diadasemana,
                        calendarios.horainicial,
                        calendarios.horafinal,
                        calendarios.aulainiciada,
                        calendarios.aulaconcluida,
                        calendarios.observacoes,
                        Turmas.Descricao as Turma,
                        Unidades.descricao as unidadeDescricao,
                        Unidadessalas.titulo,
                        materiastemplate.nome as materiaDescricao,
                        Pessoas.Nome as professor 
                        from calendarios
                        left join Turmas on Calendarios.TurmaId = Turmas.Id
                        left join Unidades on Calendarios.UnidadeId = Unidades.Id
                        left join Unidadessalas on Calendarios.SalaId = Unidadessalas.Id
                        left join materiastemplate on Calendarios.MateriaId = materiastemplate.Id 
                        left join Pessoas on Calendarios.ProfessorId = Pessoas.Id
                        where calendarios.professorId = @professorId  
                        order by Calendarios.DiaAula asc  ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<ProfessorCalendarioViewModel>(query, new { professorId = professorId });

                connection.Close();
                // 2022 01 30

                var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                foreach (var cal in results)
                {

                    //Debug.WriteLine(diaSeguinte);
                    if (cal.diaaula < hoje)
                    {
                        cal.podeVerRelatorioAula = true;
                    }
                    else if (cal.diaaula == hoje)
                    {
                        cal.podeVerRelatorioAula = null;
                    }
                    else
                    {
                        cal.podeVerRelatorioAula = false;
                    }

                }

                return results;
            }
        }


        public async Task<PessoaDto> GetProfessorById(Guid professorId)
        { 
            var query = @"SELECT * FROM Pessoas INNER JOIN Enderecos ON Pessoas.id = Enderecos.PessoaId WHERE Pessoas.id = @professorId ";

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
                         new { professorId = professorId }, splitOn: "Id");

                    connection.Close();

                    return results.FirstOrDefault();
            }
           
        }

        public async Task<IEnumerable<MateriaHabilitadaViewModel>> GetProfessoresMaterias(Guid professorId)
        {
            string query = @"select 
                            ProfessoresMaterias.id,
                            ProfessoresMaterias.PacoteMateriaId,
                            ProfessoresMaterias.ProfessorId, 
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

                return result.OrderBy(r => r.nome);
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

        public async Task<IEnumerable<PessoaDto>> GetProfessoresDisponiveisByFilter(string diaDaSemana, Guid unidadeId, Guid materiaId)
        {
            var diaSemana = DiaDaSemana.TryParsToDisponibilidadeTable(diaDaSemana);
            string query = @"select 
                            Pessoas.Id,
                            Pessoas.Nome
                            from Pessoas 
                            inner join professoresDisponibilidades on Pessoas.Id = professoresDisponibilidades.PessoaId
                            inner join ProfessoresMaterias on Pessoas.Id = ProfessoresMaterias.ProfessorId
                            WHERE professoresDisponibilidades." + diaSemana + @" = 'True' 
                            and Pessoas.Ativo = 'True'
                            AND professoresDisponibilidades.UnidadeId = @unidadeId
                            and ProfessoresMaterias.PacoteMateriaId = @materiaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<PessoaDto>(query, new { diaDaSemana = diaDaSemana, unidadeId = unidadeId, materiaId = materiaId });

                connection.Close();

                return result;
            }
        }


        #region GetProfDisponiveis
        public async Task<IEnumerable<PessoaDto>> GetProfessoresDisponiveis(Guid turmaId)
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

        private async Task<IEnumerable<PessoaDto>> GetProfessoresDisponiveisNoDia(Guid unidadeId, List<string> diasSemana, Guid turmaId)
        {/*
            StringBuilder profsDisponiveis = new StringBuilder();
            profsDisponiveis.Append("select professores.id, professores.Nome, professores.Email from ProfessoresDisponibilidades ");
            profsDisponiveis.Append("inner join Professores on ProfessoresDisponibilidades.PessoaId = Professores.Id where ");
            profsDisponiveis.Append(" ProfessoresDisponibilidades.UnidadeId = '" + unidadeId + "'");
            profsDisponiveis.Append(" AND Professores.ativo = 'True' AND ");

            if (diasSemana.Count() == 1)
            {
                var stringDiaSemana = ParseDiaSemana(diasSemana[0]);
                profsDisponiveis.Append(" ProfessoresDisponibilidades." + stringDiaSemana + " = 'True'  ");


            }
            else
            {
                for (int i = 0; i < diasSemana.Count(); i++)
                {
                    var stringDiaSemana = ParseDiaSemana(diasSemana[i]);
                    profsDisponiveis.Append("ProfessoresDisponibilidades." + stringDiaSemana + " = 'True'  ");
                    if (i != diasSemana.Count() - 1) profsDisponiveis.Append(" OR ");
                }

            }

            var profsDaTurma = @"SELECT ProfessorId FROM TurmasProfessores WHERE TurmasProfessores.TurmaId = @turmaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ProfessorDto>(profsDisponiveis.ToString());

                var professoresTurma = await connection.QueryAsync<Guid>(profsDaTurma, new { turmaId = turmaId });

                var professores = new List<ProfessorDto>();

                foreach (var item in result.DistinctBy(p => p.email))
                {
                    var prof = professoresTurma.Where(p => p == item.id).ToList();

                    if(!prof.Any())
                    {
                        professores.Add(item);
                    }
                }

                connection.Close();

                return professores.ToList();
            }
            */

            var query = @"SELECT 
                        Pessoas.Id,                        
                        Pessoas.Nome
                        FROM Pessoas
                        INNER JOIN ProfessoresDisponibilidades ON Pessoas.Id = ProfessoresDisponibilidades.PessoaId
                        WHERE ProfessoresDisponibilidades.UnidadeId = @unidadeId
                        AND Pessoas.id NOT IN (
                        SELECT TurmasProfessores.ProfessorId FROM TurmasProfessores WHERE TurmasProfessores.TurmaId = @turmaId
                        ) ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<PessoaDto>(query, new { unidadeId = unidadeId, turmaId = turmaId });                

                connection.Close();

                return result;
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

                var result = await connection.QueryAsync<Guid>(query, new { turmaId = turmaId });

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

        public async Task<MateriaHabilitadaViewModel> GetProfessorMateria(Guid professorMateriaId)
        {
            string query = @"select 
                            ProfessoresMaterias.id,
                            ProfessoresMaterias.PacoteMateriaId,
                            ProfessoresMaterias.ProfessorId, 
                            MateriasTemplate.nome,
                            TypePacote.nome as nomePacote
                            from ProfessoresMaterias
                            inner join MateriasTemplate on ProfessoresMaterias.PacoteMateriaId = MateriasTemplate.id
                            inner join TypePacote on MateriasTemplate.TypePacoteId = TypePacote.Id
                            where ProfessoresMaterias.id = @professorMateriaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<MateriaHabilitadaViewModel>(query, new { professorMateriaId = professorMateriaId });

                connection.Close();

                return result;
            }
        }

        public async Task<ProfessorRelatorioViewModel> GetReportHoursTeacher(DateTime rangeIni, DateTime rangeFinal, Guid teacherId)
        {
            rangeIni = new DateTime(rangeIni.Year, rangeIni.Month, rangeIni.Day, 0, 0, 0, 0);
            rangeFinal = new DateTime(rangeFinal.Year, rangeFinal.Month, rangeFinal.Day, 0, 0, 0, 0);

            ProfessorRelatorioViewModel report = new ProfessorRelatorioViewModel();

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT 
                        calendarios.Id,
                        calendarios.diaaula,
                        calendarios.diadasemana,
                        calendarios.horainicial,
                        calendarios.horafinal,
                        calendarios.aulainiciada,
                        calendarios.aulaconcluida,
                        calendarios.dateAulaIniciada,
                        calendarios.dateAulaConcluida,
                        calendarios.observacoes,
                        Turmas.Descricao as Turma,
                        Unidades.descricao as unidadeDescricao,
                        Unidadessalas.titulo,
                        materiastemplate.nome as materiaDescricao,
                        Pessoas.Nome as professor 
                        FROM calendarios
                        LEFT JOIN Turmas on Calendarios.TurmaId = Turmas.Id
                        LEFT JOIN Unidades on Calendarios.UnidadeId = Unidades.Id
                        LEFT JOIN Unidadessalas on Calendarios.SalaId = Unidadessalas.Id
                        LEFT JOIN materiastemplate on Calendarios.MateriaId = materiastemplate.Id 
                        LEFT JOIN Pessoas on Calendarios.ProfessorId = Pessoas.Id
                        WHERE calendarios.professorId = @teacherId 
                        AND calendarios.DiaAula >= @rangeIni  
                        AND calendarios.DiaAula < @rangeFinal
                        ORDER BY Calendarios.DiaAula asc ");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                
                report.calendars = await connection.QueryAsync<ProfessorCalendarioViewModel>(query.ToString(), new { rangeIni = rangeIni, rangeFinal = rangeFinal, teacherId = teacherId });

                connection.Close();

                if (report.calendars.Any())
                {
                    report = CalculateTime(report);
                }

                return report;
            }
        }

        private ProfessorRelatorioViewModel CalculateTime(ProfessorRelatorioViewModel report)
        {
            foreach (var calendar in report.calendars)
            {
                if (calendar.aulainiciada && calendar.aulaconcluida)
                {
                    var horarioInicial = calendar.horainicial.Split(":");
                    var horarioFinal = calendar.horafinal.Split(":");

                    DateTime dataAulaInicialCompleta = calendar.diaaula.ToCompleteTime(Convert.ToInt32(horarioInicial[0]), Convert.ToInt32(horarioInicial[1]));

                    if (calendar.dateAulaIniciada < dataAulaInicialCompleta)
                        calendar.dateAulaIniciada = dataAulaInicialCompleta;

                    DateTime dataAulaFinalCompleta = calendar.diaaula.ToCompleteTime(Convert.ToInt32(horarioFinal[0]), Convert.ToInt32(horarioFinal[1]));

                    if (calendar.dateAulaConcluida > dataAulaFinalCompleta)
                        calendar.dateAulaConcluida = dataAulaFinalCompleta;

                    calendar.totalClassroomMinutes = calendar.diaaula.CalculateTotalMinutes(calendar.dateAulaIniciada, calendar.dateAulaConcluida);

                    report.totalMinutes += calendar.totalClassroomMinutes;
                }                
            }

            report.TotalHoursToString = TransformeMinutesInHours(report.totalMinutes);

            return report;
        }

        private string TransformeMinutesInHours(int totalMinutes)
        {
            if (totalMinutes == 0) return "";

            double result = totalMinutes / 60;

            if (result < 1) return "0 horas e "+totalMinutes+" minutos";

            var total = Math.Floor(result) + " horas e ";

            var minutos = totalMinutes - (Math.Floor(result) * 60);

            total += Convert.ToInt32(minutos) + " minutos";

            return total;
        }

        private DateTime ConstructCompleteHour(DateTime diaAula, string horaInicio, string horaFinal)
        {
            int hour = Convert.ToInt32(horaInicio);
            int minute = Convert.ToInt32(horaFinal);

            DateTime diaAulaCompleta = new DateTime(diaAula.Year, diaAula.Month, diaAula.Day, hour, minute, 0);

            return diaAulaCompleta;
        }

        public async Task<string> GetEmailDoProfessorById(Guid professorId)
        {
            var query = @"SELECT Pessoas.Email FROM Pessoas WHERE Pessoas.id = @id";
            

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<string>(query, new { id = professorId });

                connection.Close();

                return result.FirstOrDefault();

            }
        }


        #endregion


    }
}