using Dapper;
using Invictus.Core.Extensions;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
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
        public TurmaPedagQueries(IConfiguration config)
        {
            _config = config;
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

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var notas = await connection.QueryAsync<TurmaNotasViewModel>(query, new { matriculaId = matriculaId });

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
    }
}
