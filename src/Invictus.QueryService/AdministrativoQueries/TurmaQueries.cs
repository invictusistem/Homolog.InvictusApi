using Dapper;
using Invictus.Core.Interfaces;
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
        public TurmaQueries(IConfiguration config, IAspNetUser aspUser,
            IUnidadeQueries unidadeQueries)
        {
            _config = config;
            _aspUser = aspUser;
            _unidadeQueries = unidadeQueries;
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
            query.Append(" ProfessoresMaterias.ProfessorId = '"+ professorId +"' ) and TurmasMaterias.TurmaId = '"+ turmaId + "' ");
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
                try
                {
                    results = await connection.QueryAsync<MateriaView>(query.ToString());
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

                var results = connection.Query< ProfessorTurmaView, MateriaProfessorView, ProfessorTurmaView > (query,
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
