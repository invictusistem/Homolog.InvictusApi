using AutoMapper;
using Dapper;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class TurmaQueries : ITurmaQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspUser;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IMateriaTemplateQueries _materiaTemplateQueries;
        private readonly IMapper _mapper;
        private readonly ITurmaRepo _turmaRepo;
        private readonly ICalendarioQueries _calendaQueries;
        private readonly InvictusDbContext _db;
        public TurmaQueries(IConfiguration config, IAspNetUser aspUser,
            IUnidadeQueries unidadeQueries, IMapper mapper, ITurmaRepo turmaRepo, IMateriaTemplateQueries materiaTemplateQueries, InvictusDbContext db,
            ICalendarioQueries calendaQueries)
        {
            _config = config;
            _aspUser = aspUser;
            _unidadeQueries = unidadeQueries;
            _mapper = mapper;
            _turmaRepo = turmaRepo;
            _materiaTemplateQueries = materiaTemplateQueries;
            _db = db;
            _calendaQueries = calendaQueries;
        }
        public async Task<int> CountTurmas(Guid unidadeId)
        {
            string query = @"SELECT Count(*) FROM Turmas WHERE Turmas.unidadeId = @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var count = await connection.QuerySingleAsync<int>(query, new { unidadeId = unidadeId });

                connection.Close();

                return count;
            }
        }
        public async Task<IEnumerable<ListaPresencaDto>> GetListaPresencas(Guid calendarioId)
        {
            string query = @"SELECT * FROM TurmasPresencas WHERE TurmasPresencas.CalendarioId = @calendarioId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ListaPresencaDto>(query, new { calendarioId = calendarioId });

                connection.Close();

                return results;
            }
        }

        public async Task<IEnumerable<ListaPresencaDto>> GetInfoDiaPresencaLista(Guid calendarioId)
        {
            // verificar se ja tem na lista presença pelo calendarioId
            var lista = await GetListaPresencas(calendarioId);
            // se nao tiver, criar 
            if (!lista.Any())
            {
                var alunos = new List<PessoaDto>();
                var listaPresenca = new List<ListaPresencaDto>();

                foreach (var aluno in alunos)
                {
                    listaPresenca.Add(new ListaPresencaDto()
                    {
                        nome = aluno.nome,
                        calendarioId = calendarioId,
                        isPresent = false,
                        isPresentToString = "F",
                        alunoId = aluno.id
                    });
                }

                var presencas = _mapper.Map<IEnumerable<Presenca>>(listaPresenca);

                await _turmaRepo.SaveListPresencas(presencas);

                _turmaRepo.Commit();

                lista = listaPresenca;
            }

            return lista;
        }

        public async Task<IEnumerable<TurmaMateriasDto>> GetMateriasDaTurma(Guid turmaId)
        {
            string query = @"SELECT * FROM TurmasMaterias WHERE TurmasMaterias.turmaId = @turmaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaMateriasDto>(query, new { turmaId = turmaId });

                connection.Close();

                return results;
            }
        }

        public async Task<IEnumerable<TurmaMateriasDto>> GetMateriasFromPacotesMaterias(Guid pacoteId)
        {
            string query = @"select
                            PacotesMaterias.nome,
                            PacotesMaterias.CargaHoraria,
                            PacotesMaterias.modalidade,
                            PacotesMaterias.Ordem,
                            MateriasTemplate.id as materiaId, 
                            MateriasTemplate.nome,
                            MateriasTemplate.typepacoteId
                            from PacotesMaterias
                            left join MateriasTemplate on PacotesMaterias.MateriaId = MateriasTemplate.id
                            where PacotesMaterias.pacoteId = @pacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaMateriasDto>(query, new { pacoteId = pacoteId });

                connection.Close();

                return results;
            }
        }

        public async Task<List<MateriaView>> GetMateriasLiberadas(Guid turmaId, Guid professorId)
        {
            /*
             

             */

            string turmaHorariosQuery = @"SELECT * FROM TurmasHorarios WHERE TurmasHorarios.TurmaId = @turmaId";

            StringBuilder profDispo = new StringBuilder();

            profDispo.Append(@"SELECT* FROM ProfessoresDisponibilidades
                            WHERE ProfessoresDisponibilidades.PessoaId = @professorId ");

            string materiasDoProfessorQuery = @"SELECT ProfessoresMaterias.PacoteMateriaId 
                                            FROM ProfessoresMaterias 
                                            WHERE ProfessoresMaterias.ProfessorId = @professorId";

            string materiasLiberadas = @"SELECT * FROM TurmasMaterias 
                            WHERE TurmasMaterias.turmaId = @turmaId
                            AND TurmasMaterias.ProfessorId in (@professorId,'00000000-0000-0000-0000-000000000000') 
                            AND TurmasMaterias.MateriaId IN @listaMats";// trazer do msm prof e sem prof

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                IEnumerable<MateriaView> results = new List<MateriaView>();

                var horarios = await connection.QueryAsync<TurmaHorarioDto>(turmaHorariosQuery, new {  turmaId = turmaId });

                var listaHoratio = horarios.Select(t => t.DiaSemanada);

                foreach (var diaSemana in listaHoratio)
                {
                    profDispo.Append(" AND ProfessoresDisponibilidades.");
                    profDispo.Append(DiaDaSemana.TryParsToDisponibilidadeTable(diaSemana));
                    profDispo.Append(" = 'True'");
                }

                var disponibilidadeDoProfessor = await connection.QueryAsync<DisponibilidadeDto>(profDispo.ToString(), new { professorId = professorId });
                
                if (!disponibilidadeDoProfessor.Any())
                {
                    return results.ToList();
                }

                var profMaterias = await connection.QueryAsync<Guid>(materiasDoProfessorQuery, new { professorId = professorId });
                try
                {
                    results = await connection.QueryAsync<MateriaView>(materiasLiberadas, new
                    {
                        professorId = professorId,
                        turmaId = turmaId,
                        listaMats = profMaterias.ToArray()
                    });
                }catch(Exception ex)
                {

                }

                connection.Close();

                foreach (var mat in results)
                {
                    if (mat.professorId.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        mat.isProfessor = false;
                    }
                    else
                    {
                        mat.isProfessor = true;
                    }
                }

                return results.ToList();
            }


        }

        public async Task<IEnumerable<ProfessorTurmaView>> GetProfessoresDaTurma(Guid turmaId)
        {
            string query = @"select
                            TurmasProfessores.ProfessorId as id,
                            Pessoas.nome,
                            Pessoas.email,
                            TurmasMaterias.id as materiaId,
                            TurmasMaterias.nome
                            from turmasprofessores
                            inner join Pessoas on TurmasProfessores.ProfessorId = Pessoas.Id
                            left join TurmasMaterias on Pessoas.Id = TurmasMaterias.ProfessorId
                            where TurmasProfessores.TurmaId = @turmaId 
                            AND Pessoas.tipoPessoa = 'Professor' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var alunoDictionary = new Dictionary<Guid, ProfessorTurmaView>();

                var results = connection.Query<ProfessorTurmaView, MateriaProfessorView, ProfessorTurmaView>(query,
                    (profDto, materiaDto) =>
                    {
                        ProfessorTurmaView profEntry;

                        if (!alunoDictionary.TryGetValue(profDto.id, out profEntry))
                        {
                            profEntry = profDto;
                            profEntry.materias = new List<MateriaProfessorView>();
                            alunoDictionary.Add(profEntry.id, profEntry);
                        }

                        if (materiaDto != null)
                        {
                            profEntry.materias.Add(materiaDto);
                        }
                        return profEntry;

                    }, new { turmaId = turmaId }, splitOn: "materiaId").Distinct().ToList();

                connection.Close();

                return results;
            }

        }

        public async Task<TurmaDto> GetTurma(Guid turmaId)
        {
            string query = @"SELECT * FROM Turmas WHERE Turmas.id = @turmaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<TurmaDto>(query, new { turmaId = turmaId });

                connection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<TurmaInfoCompleta>> GetTurmaInfo(Guid turmaId)
        {
            //var query = @"SELECT 
            //            * 
            //            FROM Turmas 
            //            INNER JOIN TurmasPrevisoes ON turmas.id = TurmasPrevisoes.turmaid 
            //            INNER JOIN TurmasHorarios ON Turmas.Id = TurmasHorarios.TurmaId
            //            WHERE Turmas.id = @turmaId ";

            //var query2 = @"SELECT COUNT(*) FROM Matriculas WHERE Matriculas.TurmaId = @turmaId";

            //var query3 = @" SELECT UnidadesSalas.Capacidade FROM UnidadesSalas WHERE UnidadesSalas.Id = @salaId ";

            //var turmaDictionary = new Dictionary<Guid, TurmaInfoCompleta>();

            //await using (var connection = new SqlConnection(
            //        _config.GetConnectionString("InvictusConnection")))
            //{
            //    connection.Open();

            //    var results = connection.Query<TurmaInfoCompleta, PrevisoesDto, HorarioDto, TurmaInfoCompleta >(
            //            query,
            //            (turmaViewModel, privisoesDto, horarioDto) => 
            //            {
            //                TurmaInfoCompleta turmaViewEntry;

            //                if(!turmaDictionary.TryGetValue(turmaViewModel.id, out turmaViewEntry)){

            //                    turmaViewEntry = turmaViewModel;
            //                    turmaViewEntry.previsoes = privisoesDto;
            //                    turmaViewEntry.horarios = new List<HorarioDto>();
            //                    turmaDictionary.Add(turmaViewEntry.id, turmaViewEntry);
            //                }

            //                turmaViewEntry.horarios.Add(horarioDto);

            //                //turmaViewModel.previsoes = privisoesDto;
            //                return turmaViewModel;
            //            }, new { turmaId = turmaId },

            //            splitOn: "Id").Distinct().ToList();

            //    connection.Close();

            //    foreach (var turma in results)
            //    {
            //        turma.totalAlunos = await connection.QuerySingleAsync<int>(query2, new { turmaId = turma.id });
            //        turma.vagas = await connection.QuerySingleAsync<int>(query3, new { salaId = turma.salaId });
            //    }

            //    return results;//.OrderBy(d => d.prevInicio);

            //}
           
            var query = @"select * from Turmas inner join TurmasPrevisoes on turmas.id = TurmasPrevisoes.turmaid 
                        where Turmas.id = @turmaId ";

            var query2 = @"SELECT COUNT(*) FROM Matriculas WHERE Matriculas.TurmaId = @turmaId";

            var query3 = @" SELECT UnidadesSalas.Capacidade FROM UnidadesSalas WHERE UnidadesSalas.Id = @salaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaInfoCompleta, PrevisoesDto, TurmaInfoCompleta>(
                        query,
                        (turmaViewModel, privisoesDto) =>
                        {
                            turmaViewModel.previsoes = privisoesDto;
                            return turmaViewModel;
                        }, new { turmaId = turmaId },
                        splitOn: "Id");

                connection.Close();

                foreach (var turma in results)
                {
                    turma.totalAlunos = await connection.QuerySingleAsync<int>(query2, new { turmaId = turma.id });
                    turma.vagas = await connection.QuerySingleAsync<int>(query3, new { salaId = turma.salaId });
                }

                return results;//.OrderBy(d => d.prevInicio);

            }
            
        }

        public async Task<TurmaMateriasDto> GetTurmaMateria(Guid turmaMateriaId)
        {
            string query = @"SELECT * FROM TurmasMaterias WHERE TurmasMaterias.id = @turmaMateriaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<TurmaMateriasDto>(query, new { turmaMateriaId = turmaMateriaId });

                connection.Close();

                return results;
            }

        }

        public async Task<IEnumerable<TurmaMateriasDto>> GetTurmaMateriasFromProfessorId(Guid professorId, Guid turmaId)
        {
            var query = @"SELECT * FROM TurmasMaterias WHERE TurmasMaterias.ProfessorId = @professorId AND TurmasMaterias.TurmaId = @turmaId ";
            // 
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<TurmaMateriasDto>(query, new { professorId = professorId, turmaId = turmaId });

                connection.Close();

                return result;
            }
        }

        public async Task<TurmaProfessoresDto> GetTurmaProfessor(Guid professorId, Guid turmaId)
        {
            var query = @"SELECT * FROM TurmasProfessores WHERE TurmasProfessores.ProfessorId = @professorId AND TurmasProfessores.TurmaId = @turmaId ";
            // 
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<TurmaProfessoresDto>(query, new { professorId = professorId, turmaId = turmaId });

                connection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<TurmaViewModel>> GetTurmas()
        {
            var siglaUnidade = _aspUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(siglaUnidade);

            var turmas = await GetTurmasByUnidadeId(unidade.id);

            return turmas;
        }

        public async Task<TurmaMatriculaViewModel> GetTurmasById(Guid turmaId)
        {
            //var unidadeId = GetUnidadeId();
            string query = @"select 
                            turmas.Id, 
                            turmas.identificador,
                            turmas.descricao,
                            turmas.StatusAndamento,
                            turmas.typepacoteId, 
                            TurmasHorarios.id as turmaHorarioId,
                            TurmasHorarios.DiaSemanada,
                            TurmasHorarios.HorarioInicio,
                            TurmasHorarios.HorarioFim
                            from Turmas 
                            inner join TurmasHorarios on turmas.Id = TurmasHorarios.TurmaId 
                            WHERE turmas.id = @turmaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var alunoDictionary = new Dictionary<Guid, TurmaMatriculaViewModel>();

                var contrato = connection.Query<TurmaMatriculaViewModel, TurmaHorarioDto, TurmaMatriculaViewModel>(query,
                    (turmaDto, horarioDto) =>
                    {
                        TurmaMatriculaViewModel turmaEntry;

                        if (!alunoDictionary.TryGetValue(turmaDto.id, out turmaEntry))
                        {
                            turmaEntry = turmaDto;
                            turmaEntry.Horarios = new List<TurmaHorarioDto>();
                            alunoDictionary.Add(turmaEntry.id, turmaEntry);
                        }

                        if (horarioDto != null)
                        {
                            turmaEntry.Horarios.Add(horarioDto);
                        }
                        return turmaEntry;

                    }, new { turmaId = turmaId }, splitOn: "turmaHorarioId").Distinct().ToList();

                connection.Close();

                return contrato.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TurmaViewModel>> GetTurmasByType(Guid typepacote)
        {
            var siglaUnidade = _aspUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(siglaUnidade);

            string query = @"SELECT Turmas.Id, 
                        Turmas.Identificador, 
                        Turmas.Descricao, 
                        Turmas.StatusAndamento, 
                        Turmas.TotalAlunos, 
                        Turmas.PrevisaoInfo, 
                        Turmas.PrevisaoAtual, 
                        Turmas.PrevisaoTerminoAtual, 
                        UnidadesSalas.Capacidade as Vagas 
                        From Turmas 
                        inner join UnidadesSalas on Turmas.SalaId = UnidadesSalas.Id 
                        Where Turmas.UnidadeId = @unidadeId 
                        AND Turmas.typePacoteId = @typepacote
                        Order BY Turmas.Identificador ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaViewModel>(query, new { unidadeId = unidade.id, typepacote = typepacote });

                connection.Close();

                return results;
            }
        }

        public async Task<IEnumerable<TurmaDiarioClasseViewModel>> GetTurmasPedagViewModel()
        {
            var unidadeSigla = _aspUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);
            /*
             select calendarios.id, 
calendarios.diaAula, 
calendarios.horaInicial, 
calendarios.horaFinal,
Professores.Nome,
MateriasTemplate.nome
from calendarios 
LEFT JOIN Professores on Calendarios.ProfessorId = Professores.Id
LEFT JOIN MateriasTemplate on Calendarios.MateriaId = MateriasTemplate.Id
where Calendarios.DiaAula > '2022-03-23' 
and Calendarios.TurmaId = 'c21a80ce-9d44-4551-a64d-edafe1e51e2b' 
order by DiaAula
             */
            var query = @"SELECT id, identificador, descricao, 
                        statusAndamento
                        FROM Turmas WHERE Turmas.UnidadeId = @unidadeId 
                        AND Turmas.statusAndamento <> 'Aguardando início'  ";

            var dateNow = DateTime.Now;
            var query2 = @"SELECT Calendarios.id, 
                        Calendarios.diaAula, 
                        Calendarios.horaInicial, 
                        Calendarios.horaFinal,
                        Professores.Nome as professor,
                        MateriasTemplate.nome AS descAula
                        FROM Calendarios 
                        LEFT JOIN Professores ON Calendarios.ProfessorId = Professores.Id
                        LEFT JOIN MateriasTemplate ON Calendarios.MateriaId = MateriasTemplate.Id
                        WHERE Calendarios.DiaAula = '" + dateNow.Year + "-" + dateNow.Month + "-" + dateNow.Day + @"' 
                        AND Calendarios.TurmaId = @turmaId ";

            var query3 = @"SELECT Calendarios.id, 
                        Calendarios.diaAula, 
                        Calendarios.horaInicial, 
                        Calendarios.horaFinal,
                        Professores.Nome as professor,
                        MateriasTemplate.nome AS descAula
                        FROM Calendarios 
                        LEFT JOIN Professores ON Calendarios.ProfessorId = Professores.Id
                        LEFT JOIN MateriasTemplate ON Calendarios.MateriaId = MateriasTemplate.Id
                        WHERE Calendarios.DiaAula >= '" + dateNow.Year + "-" + dateNow.Month + "-" + dateNow.Day + @"' 
                        AND Calendarios.TurmaId = @turmaId 
                        order by DiaAula";

            var query4 = @"SELECT Calendarios.id, 
                        Calendarios.diaAula, 
                        Calendarios.horaInicial, 
                        Calendarios.horaFinal,
                        Professores.Nome as professor,
                        MateriasTemplate.nome AS descAula
                        FROM Calendarios 
                        LEFT JOIN Professores ON Calendarios.ProfessorId = Professores.Id
                        LEFT JOIN MateriasTemplate ON Calendarios.MateriaId = MateriasTemplate.Id
                        WHERE Calendarios.DiaAula > '" + dateNow.Year + "-" + dateNow.Month + "-" + dateNow.Day + @"' 
                        AND Calendarios.TurmaId = @turmaId 
                        order by DiaAula";

            var DataPesquisa = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var turmas = await connection.QueryAsync<TurmaDiarioClasseViewModel>(query, new { unidadeId = unidade.id });

                // traz Id do calendario se tiver aula no DIa
                foreach (var turma in turmas)
                {

                    var calendario = await connection.QueryAsync<TurmaDiarioClasseViewModel>(query2, new { turmaId = turma.id });

                    //Get próxima aula
                    var proximaAula = await connection.QueryAsync<TurmaDiarioClasseViewModel>(query3, new { turmaId = turma.id });
                    var now = DateTime.Now;
                    //var cal = proximaAula.OrderBy(c => c.DiaAula);

                    // confirmar se tem o guid e pegar o default
                    if (calendario.Any())
                    {
                        turma.calendarioId = calendario.Select(c => c.id).First();
                        //TEMP

                        var horaCompletaInicio = calendario.Select(c => c.diaAula).First();
                        var horaInicio = calendario.Select(c => c.horaInicial).First().Split(":");
                        var horaFinal = calendario.Select(c => c.horaFinal).First().Split(":");

                        horaCompletaInicio = new DateTime(horaCompletaInicio.Year, horaCompletaInicio.Month, horaCompletaInicio.Day,
                            Convert.ToInt32(horaInicio[0]), Convert.ToInt32(horaInicio[1]), 0);

                        var horaCompletaFinal = new DateTime(horaCompletaInicio.Year, horaCompletaInicio.Month, horaCompletaInicio.Day,
                            Convert.ToInt32(horaFinal[0]), Convert.ToInt32(horaFinal[1]), 0);
                        var timeSpan = new TimeSpan(0, 15, 0);

                        var iniciar = horaCompletaInicio - timeSpan;

                        if (dateNow < horaCompletaFinal & dateNow >= horaCompletaInicio - timeSpan)
                        {
                            turma.proximaAula = horaCompletaInicio;
                            turma.proximaAulaFinal = horaCompletaFinal;
                            turma.descAula = calendario.Select(c => c.descAula).First();
                            turma.professor = calendario.Select(c => c.professor).First();
                            turma.podeIniciarAula = true;
                        }
                        else
                        {
                            //var aulas = await connection.QueryAsync<CalendarioDto>(query3, new { turmaId = turma.id });

                            //var horaIni = aulas.ToList()[1].diaAula;
                            //var ini = aulas.ToList()[1].horaInicial.Split(":");
                            //var fim = aulas.ToList()[1].horaFinal.Split(":");

                            //var horaInicial = new DateTime(horaIni.Year, horaIni.Month, horaIni.Day,
                            //    Convert.ToInt32(ini[0]), Convert.ToInt32(ini[1]), 0);

                            //var horaFim = new DateTime(horaIni.Year, horaIni.Month, horaIni.Day,
                            //    Convert.ToInt32(fim[0]), Convert.ToInt32(fim[1]), 0);

                            //turma.proximaAula = horaInicial;
                            //turma.proximaAulaFinal = horaFim;
                            turma.descAula = calendario.Select(c => c.descAula).First();
                            turma.professor = calendario.Select(c => c.professor).First();
                            turma.proximaAula = horaCompletaInicio;
                            turma.proximaAulaFinal = horaCompletaFinal;
                            turma.podeIniciarAula = false;
                        }


                    }
                    else
                    {
                        var proximaAulaX = await connection.QueryAsync<TurmaDiarioClasseViewModel>(query4, new { turmaId = turma.id });
                        var horaCompletaAula = proximaAulaX.Select(c => c.diaAula).First();
                        var horaInicio = proximaAulaX.Select(c => c.horaInicial).First().Split(":");
                        var horaFinal = proximaAulaX.Select(c => c.horaFinal).First().Split(":");


                        horaCompletaAula = new DateTime(horaCompletaAula.Year, horaCompletaAula.Month, horaCompletaAula.Day,
                            Convert.ToInt32(horaInicio[0]), Convert.ToInt32(horaInicio[1]), 0);

                        var horaCompletaFinal = new DateTime(horaCompletaAula.Year, horaCompletaAula.Month, horaCompletaAula.Day,
                            Convert.ToInt32(horaFinal[0]), Convert.ToInt32(horaFinal[1]), 0);

                        turma.proximaAula = horaCompletaAula;
                        turma.proximaAulaFinal = horaCompletaFinal;
                        turma.descAula = proximaAulaX.Select(c => c.descAula).First();
                        turma.professor = proximaAulaX.Select(c => c.professor).First();

                        turma.podeIniciarAula = false;
                    }

                    /*
                     TODO HORARIO
                     TODO AUTORIZAÇÃO
                     
                     */

                }

                // Pegar dados da próxima aula - (próxima aula: Ética - 25/03/2022 08:00 às 12:00)

                return turmas;

            }

            //throw new NotImplemen tedException();
        }

        public async Task<IEnumerable<Guid>> GetTypePacotesTurmasMatriculadas(Guid alunoId)
        {
            var query = @"select turmas.TypePacoteId From Turmas
                        inner join Matriculas on Turmas.id = Matriculas.TurmaId
                        where Matriculas.AlunoId = @alunoId";
            // 
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<Guid>(query, new { alunoId = alunoId });

                connection.Close();

                return results;
            }
        }

        private async Task<IEnumerable<TurmaViewModel>> GetTurmasByUnidadeId(Guid unidadeId)
        {
            string query = @"SELECT Turmas.Id, 
                        Turmas.Identificador, 
                        Turmas.Descricao, 
                        Turmas.StatusAndamento, 
                        Turmas.TotalAlunos, 
                        Turmas.PrevisaoInfo, 
                        Turmas.PrevisaoAtual, 
                        Turmas.minimoAlunos, 
                        Turmas.typePacoteId, 
                        Turmas.PrevisaoTerminoAtual, 
                        UnidadesSalas.Capacidade as Vagas 
                        From Turmas 
                        inner join UnidadesSalas on Turmas.SalaId = UnidadesSalas.Id 
                        Where Turmas.UnidadeId = @unidadeId   
                        Order BY Turmas.Identificador ";

            string query2 = @"SELECT COUNT(*) FROM Matriculas WHERE Matriculas.TurmaId = @turmaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaViewModel>(query, new { unidadeId = unidadeId });

                if (results.Any())
                {
                    foreach (var turma in results)
                    {
                        turma.totalAlunos = await connection.QuerySingleAsync<int>(query2, new { turmaId = turma.id });
                    }
                }

                connection.Close();

                return results;
            }

        }

        public async Task<TurmaMateriasDto> GetTurmaMateriaByTurmaAndMateriaId(Guid materiaId, Guid turmaId)
        {
            string query = @"SELECT * FROM TurmasMaterias WHERE TurmasMaterias.materiaId = @materiaId AND TurmasMaterias.turmaId = @turmaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<TurmaMateriasDto>(query, new { materiaId = materiaId, turmaId = turmaId });

                connection.Close();

                return result;
            }
        }

        public async Task<AulaDiarioClasseViewModel> GetPresencaAulaViewModel(Guid calendarioId)
        {
            // NEW
            string procurarTurmaPresencaDoDiaQuery = @"SELECT id FROM TurmasPresencas WHERE TurmasPresencas.CalendarioId = @calendarioId ";

            string calendarioDoDiaQuery = @"SELECT id, turmaId FROM Calendarios WHERE Calendarios.Id = @calendarioId";

            string alunosDaTurmaQuerie = @"SELECT id, alunoId FROM Matriculas WHERE Matriculas.TurmaId = @turmaId";

            // OLD
            string infoAulaQuery = @"select
                                Calendarios.id,
                                Calendarios.DiaAula,
                                Calendarios.HoraInicial,
                                Calendarios.HoraFinal,
                                Calendarios.observacoes,
                                Turmas.id as turmaId,
                                Pessoas.Nome as nome,
                                MateriasTemplate.nome as materiaDescricao
                                FROM Calendarios
                                inner join Turmas on Calendarios.turmaId = Turmas.id 
                                left join Pessoas on Calendarios.ProfessorId = Pessoas.Id
                                inner join MateriasTemplate on Calendarios.MateriaId = MateriasTemplate.Id
                                WHERE Calendarios.Id = @calendarioId ";

            string presencasQuery = @"SELECT 
                                    TurmasPresencas.Id,
                                    TurmasPresencas.calendarioId,
                                    TurmasPresencas.alunoId,
                                    TurmasPresencas.isPresent,
                                    TurmasPresencas.isPresentToString,
                                    Pessoas.nome,
                                    Pessoas.nomeSocial
                                    FROM TurmasPresencas 
                                    INNER JOIN Pessoas ON TurmasPresencas.AlunoId = Pessoas.Id
                                    WHERE TurmasPresencas.CalendarioId = @calendarioId ";

            //string queryTwo = @"SELECT 
            //                    Alunos.id,
            //                    Alunos.Nome,
            //                    Alunos.NomeSocial
            //                    FROM Matriculas
            //                    INNER JOIN Alunos on Matriculas.AlunoId = Alunos.Id
            //                    WHERE Matriculas.TurmaId = @turmaId";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var listaPresenca = await connection.QueryAsync<Guid>(procurarTurmaPresencaDoDiaQuery, new { calendarioId = calendarioId });

                if (!listaPresenca.Any())
                {
                    var calendario = await connection.QuerySingleAsync<TurmaCalendarioViwModel>(calendarioDoDiaQuery, new { calendarioId = calendarioId });
                    var alunosDaTurma = await connection.QueryAsync<MatriculaDto>(alunosDaTurmaQuerie, new { turmaId = calendario.turmaId });

                    var turmaPresencaList = new List<Presenca>();

                    foreach (var aluno in alunosDaTurma)
                    {
                        var presenca = new Presenca(calendarioId, null, aluno.alunoId,aluno.id, null);;
                        turmaPresencaList.Add(presenca);
                    }

                    await _turmaRepo.SaveListPresencas(turmaPresencaList);
                    _turmaRepo.Commit();

                }

                var infoAula = await connection.QueryAsync<AulaViewModel>(infoAulaQuery, new { calendarioId = calendarioId });

                var presencas = await connection.QueryAsync<ListaPresencaViewModel>(presencasQuery, new { calendarioId = calendarioId });


                //var alunos = await connection.QueryAsync<AlunoDto>(queryTwo, new { turmaId = infoAula.turmaId });

                //var presencas = new List<ListaPresencaViewModel>();
                //foreach (var aluno in alunos)
                //{
                //    var presenca = new ListaPresencaViewModel();
                //    presenca.calendarioId = infoAula.id;
                //    presenca.alunoId = aluno.id;
                //    presenca.isPresent = null;
                //    presenca.isPresentToString = null;
                //    presenca.nome = aluno.nome;
                //    presenca.nomeSocial = aluno.nomeSocial;
                //    presencas.Add(presenca);
                //}

                //connection.Close();

                var aulaView = new AulaDiarioClasseViewModel();
                aulaView.aulaViewModel = infoAula.FirstOrDefault();
                aulaView.listaPresenca = presencas.ToList();

                return aulaView;
            }
        }

        public async Task<IEnumerable<TurmaMateriasDto>> GetTurmasMateriasByProfessorAndMateriaId(Guid materiaId, Guid professorId)
        {
            string query = @"SELECT * FROM TurmasMaterias WHERE TurmasMaterias.materiaId = @materiaId AND TurmasMaterias.ProfessorId = @professorId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<TurmaMateriasDto>(query, new { materiaId = materiaId, professorId = professorId });

                connection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<MateriaTemplateDto>> GetMateriasDoProfessorLiberadasParaNotas(Guid turmaId)
        {
            var dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var userRole = _aspUser.ObterRole();
            var listGuidMaterias = new List<Guid>();
            var calendarios = await GetMatriasConcluidas(turmaId);
            // TurmaMateriasDto
            var matsDistinct = calendarios.DistinctBy(c => c.materiaId);

            if (userRole == "MasterAdm")
            {
                foreach (var item in matsDistinct)
                {
                    var calendFilterByMateria = calendarios.Where(c => c.materiaId == item.materiaId);

                    var matsPendentes = calendFilterByMateria.Where(c => c.diaAula >= dateNow);

                    if (!matsPendentes.Any())
                        listGuidMaterias.Add(item.materiaId);

                }
            }
            else
            {

            }

            var materias = await _materiaTemplateQueries.GetMateriasByListIds(listGuidMaterias);


            return materias;
        }

        private async Task<IEnumerable<TurmaCalendarioViwModel>> GetMatriasConcluidas(Guid turmaId)
        {
            string query = @"SELECT * FROM Calendarios WHERE Calendarios.turmaId = @turmaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<TurmaCalendarioViwModel>(query, new { turmaId = turmaId });

                connection.Close();


                return result;
            }
        }

        public async Task<IEnumerable<MateriaTemplateDto>> GetMateriasDoProfessorLiberadasParaNotasV2(Guid turmaId)
        {
            var dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var userRole = _aspUser.ObterRole();
            var materias = new List<MateriaTemplateDto>();
            var aulasDaTurma = await _calendaQueries.GetCalendarioByTurmaId(turmaId);
            var aulasDistinct = aulasDaTurma.ToList().DistinctBy(a => a.materiaId);



            if (userRole == "MasterAdm" || userRole == "SuperAdm")
            {

                foreach (var cal in aulasDistinct)
                {

                    var aulas = aulasDaTurma.Where(a => a.materiaId == cal.materiaId);

                    var aulasPassadas = aulasDaTurma.Where(a => a.materiaId == cal.materiaId & a.diaaula < dateNow);

                    var aulasPassadasCount = aulasPassadas.Count();

                    var aulasCount = aulas.Count();

                    if (aulasCount == aulasPassadasCount)
                    {
                        var mat = new MateriaTemplateDto();
                        mat.id = cal.materiaId;
                        mat.nome = cal.Nome;
                        materias.Add(mat);
                    }
                }
            }
            else
            {

            }

            


            return materias;
        }
    }

    public class AulaDiarioClasseViewModel
    {
        public AulaDiarioClasseViewModel()
        {
            listaPresenca = new List<ListaPresencaViewModel>();
        }
        public AulaViewModel aulaViewModel { get; set; }
        public List<ListaPresencaViewModel> listaPresenca { get; set; }

    }

    public class ListaPresencaViewModel
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string nomeSocial { get; set; }
        public Guid calendarioId { get; set; }
        public bool? isPresent { get; set; }
        public string isPresentToString { get; set; }
        public Guid matriculaId { get; set; }
        public Guid alunoId { get; set; }
    }
}
