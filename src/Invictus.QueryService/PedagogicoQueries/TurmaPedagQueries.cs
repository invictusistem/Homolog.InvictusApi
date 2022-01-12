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

        public async Task<IEnumerable<TurmaNotasViewModel>> GetNotasFromTurma(Guid turmaId, Guid materiaId)
        {
            var query = @"select 
                        *
                        from TurmasNotas where
                        TurmasNotas.TurmaId = @turmaId AND 
                        TurmasNotas.MateriaId = @materiaId ";

            var query2 = @"select 
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
                var notas = await connection.QueryAsync<TurmaNotasViewModel>(query, new { turmaId = turmaId, materiaId  = materiaId });

                foreach (var item in notas)
                {
                    item.nome = await connection.QuerySingleAsync<string>(query2, new { turmaId = turmaId, matriculaId = item.matriculaId });
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
