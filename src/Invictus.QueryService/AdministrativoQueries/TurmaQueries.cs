using AutoMapper;
using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
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
    public class TurmaQueries : ITurmaQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspUser;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IMapper _mapper;
        private readonly ITurmaRepo _turmaRepo;
        public TurmaQueries(IConfiguration config, IAspNetUser aspUser,
            IUnidadeQueries unidadeQueries, IMapper mapper, ITurmaRepo turmaRepo)
        {
            _config = config;
            _aspUser = aspUser;
            _unidadeQueries = unidadeQueries;
            _mapper = mapper;
            _turmaRepo = turmaRepo;
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
                var alunos = new List<AlunoDto>();
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
            StringBuilder query = new StringBuilder();
            query.Append("select TurmasMaterias.id, TurmasMaterias.nome, TurmasMaterias.ProfessorId from TurmasMaterias where ");
            query.Append(" TurmasMaterias.MateriaId in ( select ProfessoresMaterias.PacoteMateriaId from ProfessoresMaterias where ");
            query.Append(" ProfessoresMaterias.ProfessorId = '" + professorId + "' ) and TurmasMaterias.TurmaId = '" + turmaId + "' ");
            query.Append(" and TurmasMaterias.ProfessorId = '00000000-0000-0000-0000-000000000000' OR TurmasMaterias.ProfessorId = '" + professorId + "' ");

            //string guid = "00000000-0000-0000-0000-000000000000";
            //Guid nullGuid = new Guid("00000000-0000-0000-0000-000000000000");
            //string query = @"select 
            //                TurmasMaterias.id,
            //                TurmasMaterias.nome,
            //                TurmasMaterias.ProfessorId
            //                from TurmasMaterias 
            //                where TurmasMaterias.MateriaId in (
            //                select ProfessoresMaterias.PacoteMateriaId from ProfessoresMaterias 
            //                where ProfessoresMaterias.ProfessorId = @professorId 
            //                )
            //                and TurmasMaterias.TurmaId = @turmaId  
            //                and TurmasMaterias.ProfessorId = @nullGuid 
            //                OR TurmasMaterias.ProfessorId = @professorId  ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                IEnumerable<MateriaView> results = new List<MateriaView>();

                results = await connection.QueryAsync<MateriaView>(query.ToString());

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
                            professores.nome,
                            professores.email,
                            TurmasMaterias.id as materiaId,
                            TurmasMaterias.nome
                            from turmasprofessores
                            inner join Professores on TurmasProfessores.ProfessorId = Professores.Id
                            left join TurmasMaterias on Professores.Id = TurmasMaterias.ProfessorId
                            where TurmasProfessores.TurmaId = @turmaId";

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
            var query = @"select * from Turmas inner join TurmasPrevisoes on turmas.id = TurmasPrevisoes.turmaid 
                        where Turmas.id = @turmaId ";

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

            var query = @"SELECT id, identificador, descricao, 
                        statusAndamento
                        FROM Turmas WHERE Turmas.UnidadeId = @unidadeId 
                        AND Turmas.statusAndamento <> 'Aguardando início'  ";

            var dateNow = DateTime.Now;
            var query2 = @"select id from calendarios 
                        where Calendarios.DiaAula = '" + dateNow.Year + "-" + dateNow.Month + "-" + dateNow.Day + @"' 
                        and Calendarios.TurmaId = @turmaId ";

            //var query2 = @"select 
            //                DiaAula, 
            //                HoraInicial, 
            //                HoraFinal
            //                from calendarios where turmaId = @turmaId  
            //                and DiaAula = @DataPesquisa
            //                order by DiaAula asc";

            var DataPesquisa = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var turmas = await connection.QueryAsync<TurmaDiarioClasseViewModel>(query, new { unidadeId = unidade.id });

                // traz Id do calendario se tiver aula no DIa
                foreach (var turma in turmas)
                {

                    var calendario = await connection.QueryAsync<CalendarioDto>(query2, new { turmaId = turma.id });

                    // confirmar se tem o guid e pegar o default
                    if (calendario.Any())
                    {
                        turma.calendarioId = calendario.Select(c => c.id).First();
                        //TEMP
                        turma.podeIniciarAula = true;
                    }
                    else
                    {
                        turma.podeIniciarAula = false;
                    }

                    /*
                     TODO HORARIO
                     TODO AUTORIZAÇÃO
                     
                     */

                }

                

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
                        Turmas.PrevisaoTerminoAtual, 
                        UnidadesSalas.Capacidade as Vagas 
                        From Turmas 
                        inner join UnidadesSalas on Turmas.SalaId = UnidadesSalas.Id 
                        Where Turmas.UnidadeId = @unidadeId   
                        Order BY Turmas.Identificador ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaViewModel>(query, new { unidadeId = unidadeId });

                connection.Close();

                return results;
            }

        }
    }
}
