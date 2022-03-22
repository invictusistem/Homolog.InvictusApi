using Dapper;
using Invictus.Core.Extensions;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries
{
    public class TurmaPedagQueries : ITurmaPedagQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidade;
        public TurmaPedagQueries(IConfiguration config, IAspNetUser aspNetUser, IUnidadeQueries unidade)
        {
            _config = config;
            _aspNetUser = aspNetUser;
            _unidade = unidade;
        }

        public async Task<IEnumerable<AlunoDto>> GetAlunosDaTurma(Guid turmaId)
        {
            var query = @"select 
                            alunos.nome, 
                            alunos.email, 
                            alunos.cpf
                            from alunos 
                            inner join Matriculas on Alunos.id = Matriculas.alunoId
                            Where Matriculas.TurmaId = @turmaId";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var alunos = await connection.QueryAsync<AlunoDto>(query, new { turmaId = turmaId });

                foreach (var item in alunos)
                {
                    item.cpf = item.cpf.BindingCPF();
                }

                return alunos;

            }
        }

        public async Task<ListPresencaViewModel> GetInfoDiaPresencaLista(Guid turmaId, Guid calendarioId)
        {
            var listaPresencaViewModel = new ListPresencaViewModel();

            //Guid materiaId, = 

            var query = @"select * from calendarios where calendarios.id = @calendarioId ";

            var queryOne = @"select 
                        Calendarios.id,
                        Calendarios.DiaAula,
                        UnidadesSalas.Titulo, 
                        MateriasTemplate.nome as descricao,
                        Colaboradores.nome
                        from Professores 
                        inner join UnidadesSalas on Calendarios.SalaId = Salas.Id
                        inner join MateriasTemplate on Calendarios.MateriaId = Materias.id
                        inner join Professores on Calendarios.ProfessorId = Professores.Id
                        where Calendarios.id = @calendarioId ";

            var queryTwo = @"select
                            Aluno.Id as alunoId,
                            Aluno.Nome,
                            LivroPresencas.Id,
                            LivroPresencas.CalendarioId,
                            LivroPresencas.IsPresent
                            from LivroPresencas
                            right join Aluno on LivroPresencas.AlunoId = Aluno.Id
                            inner join Matriculados on Aluno.Id = Matriculados.AlunoId
                            where Matriculados.TurmaId = @turmaId";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                //Guid materiaId = 

                var infoDia = await connection.QuerySingleAsync<InfoDia>(queryOne, new { calendarioId = calendarioId });

                var listaPresenca = await connection.QueryAsync<ListaPresencaDto>(queryTwo, new { turmaId = turmaId });

                listaPresencaViewModel.infos = infoDia;
                listaPresencaViewModel.listaPresencas.AddRange(listaPresenca);

                foreach (var item in listaPresencaViewModel.listaPresencas)
                {
                    //item.calendarioId = calendarioId;
                }
                //var listaPresenca = 

                return listaPresencaViewModel;

            }
        }

        public async Task<IEnumerable<TurmaNotasViewModel>> GetNotaAluno(Guid matriculaId)
        {
            var query = @"select * from turmasNotas where TurmasNotas.MatriculaId = @matriculaId ";

            var matriculaQuery = @"SELECT Matriculas.TurmaId, matriculas.alunoId FROM Matriculas WHERE Matriculas.Id = @matriculaId ";

            var calendariosQuery = @"select * from calendarios where calendarios.turmaId = @turmaId ";

            var presencasQuery = @"SELECT * FROM TurmasPresencas WHERE calendarios.turmaId = @turmaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                // as notas sao criadas ao matricular o aluno
                var notas = await connection.QueryAsync<TurmaNotasViewModel>(query, new { matriculaId = matriculaId });

                var matricula = await connection.QuerySingleAsync<MatriculaDto>(matriculaQuery, new { matriculaId = matriculaId });

                // var aulas = await connection.QueryAsync<CalendarioDto>(calendarios, new { turmaId = matricula.turmaId });

                //var presencas = await connection.QueryAsync<Calenda rioDto>(presencasQuery, new { turmaId = matricula.turmaId });



                return notas;

            }
        }

        public async Task<IEnumerable<TurmaNotasViewModel>> GetNotasFromTurma(Guid turmaId, Guid materiaId)
        {
            var query = @"select 
                        TurmasMaterias.id  
                        from TurmasMaterias where
                        TurmasMaterias.TurmaId = @turmaId AND 
                        TurmasMaterias.MateriaId = @materiaId ";

            var query2 = @"select 
                        *
                        from TurmasNotas where
                        TurmasNotas.TurmaId = @turmaId AND 
                        TurmasNotas.MateriaId = @materiaId ";

            var query3 = @"select 
                        Alunos.Nome
                        from Alunos
                        inner join Matriculas on Alunos.Id = Matriculas.AlunoId 
                        where Matriculas.id = @matriculaId
                        AND Matriculas.TurmaId = @turmaId ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var turmaMateriaId = await connection.QuerySingleAsync<Guid>(query, new { turmaId = turmaId, materiaId = materiaId });

                //var matId = await connection.QueryAsync<TurmaNotasViewModel>(query2, new { turmaId = turmaId, materiaId = turmaMateriaId });

                var notas = await connection.QueryAsync<TurmaNotasViewModel>(query2, new { turmaId = turmaId, materiaId  = turmaMateriaId });

                foreach (var item in notas)
                {
                    item.nome = await connection.QuerySingleAsync<string>(query3, new { turmaId = turmaId, matriculaId = item.matriculaId });
                }

                return notas;

            }
        }

        public Task<IEnumerable<ProfessorDto>> GetProfessoresDaTurma(Guid turmaId)
        {
            throw new NotImplementedException();
        }

        public async Task<TurmaDto> GetTurmaByMatriculaId(Guid matriculaId)
        {
            var query = @"select 
                            Turmas.identificador, 
                            Turmas.descricao
                            from turmas 
                            where Turmas.id = (
                            select Matriculas.TurmaId from Matriculas where 
                            Matriculas.id = @matriculaId
                            )";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var turma = await connection.QuerySingleAsync<TurmaDto>(query, new { matriculaId = matriculaId });

                return turma;

            }
        }

        public async Task<string> VerificarSeAlunoDisponivelParaTransfInterna(string cpf)
        {
            var mensagem = "";

            var unidadeAtual = _aspNetUser.ObterUnidadeDoUsuario();

            var mats = await GetMatriculasDeOutraUnidade(cpf);
            //var unidadeAtual = await _context.Unidades.Where(u => u.Sigla == unidade).FirstOrDefaultAsync();
            //var aluno = 

            ////var materias = await _pedagogicoQuery.GetNotaAlunos(turmaId);
            //var aluno = await _context.Alunos.Where(aluno => aluno.CPF == CPF).SingleOrDefaultAsync();


            //if (aluno == null) { return NotFound(new { message = "Nenhum Aluno foi localizado com este CPF." }); }

            //if (aluno != null)
            //{
            //    // bool daMesmaUnidade = aluno.UnidadeCadastrada == unidade;
            //    bool daMesmaUnidade = aluno.UnidadeCadastrada == unidadeAtual.Id;
            //    if (daMesmaUnidade)
            //    {
            //        return Conflict(new { message = "O Aluno já está matriculado/cadastrado nesta unidade." });

            //    }

            //}

            return "";
        }

        public async Task<MatriculaDto> GetMatriculasDeOutraUnidade(string cpf)
        {
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await _unidade.GetUnidadeBySigla(unidadeSigla);

            var query = @"SELECT 
                        Alunos.CPF, 
                        Alunos.Nome,
                        Matriculas.TurmaId,
                        Turmas.UnidadeId
                        FROM Alunos 
                        INNER JOIN Matriculas on Alunos.id = Matriculas.AlunoId
                        INNER JOIN Turmas on Matriculas.TurmaId = Turmas.Id
                        WHERE Alunos.CPF = @cpf 
                        AND Turmas.UnidadeId != @unidadeId ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var turma = await connection.QuerySingleAsync<MatriculaDto>(query, new { cpf = cpf, unidadeId = unidade.id });

                return turma;

            }
        }
    }
}
