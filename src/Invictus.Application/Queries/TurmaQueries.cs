using Invictus.Application.Queries.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Invictus.Application.Dtos;

namespace Invictus.Application.Queries
{
    public class TurmaQueries : ITurmaQueries
    {
        private readonly IConfiguration _config;
        public TurmaQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<AgendasProvasDto>> GetAgendas(int turmaId, int avaliacao)
        {
            var query = "";
            //if(avaliacao == 1)
            //{
            //    query = @"select 
            //              id, materiaId, materia, turmaId, 
            //              convert(varchar, AvaliacaoUm, 103) AvaliacaoUm, 
            //              convert(varchar, SegundachamadaAvaliacaoUm, 103) SegundachamadaAvaliacaoUm  
            //              from provasagenda 
            //              where provasAgenda.TurmaId = @turmaId";
            //}
            //else if(avaliacao == 2)
            //{
            //    query = @"select  
            //              id, materiaId, materia, turmaId, 
            //              convert(varchar, AvaliacaoDois, 103) AvaliacaoDois, 
            //              convert(varchar, SegundachamadaAvaliacaoDois, 103) SegundachamadaAvaliacaoDois   
            //              from provasagenda 
            //              where provasAgenda.TurmaId = @turmaId";
            //}
            //else
            //{
            //    query = @"select 
            //              id, materiaId, materia, turmaId, 
            //              convert(varchar, AvaliacaoTres, 103) AvaliacaoTres, 
            //              convert(varchar, SegundachamadaAvaliacaoTres, 103) SegundachamadaAvaliacaoTres  
            //              from provasagenda 
            //              where provasAgenda.TurmaId = @turmaId";
            //}

            if (avaliacao == 1)
            {
                query = @"select 
                          id, materiaId, materia, turmaId, 
                          AvaliacaoUm, 
                          SegundachamadaAvaliacaoUm  
                          from provasagenda 
                          where provasAgenda.TurmaId = @turmaId";
            }
            else if (avaliacao == 2)
            {
                query = @"select  
                          id, materiaId, materia, turmaId, 
                          AvaliacaoDois, 
                          SegundachamadaAvaliacaoDois   
                          from provasagenda 
                          where provasAgenda.TurmaId = @turmaId";
            }
            else
            {
                query = @"select 
                          id, materiaId, materia, turmaId, 
                          AvaliacaoTres, 
                          SegundachamadaAvaliacaoTres  
                          from provasagenda 
                          where provasAgenda.TurmaId = @turmaId";
            }


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<AgendasProvasDto>(query, new { turmaId = turmaId });

                return results;
            }
        }

        public async Task<string> GetPrevisaoAtual(int turmaId)
        {
            var query = @"select previsao from turma where id = @turmaId";
            /* select * from agendaprovas where turmaId = 1070*/
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var previsao = await connection.QuerySingleAsync<string>(query, new { turmaId = turmaId });

                return previsao;
            }
        }

        public async Task<IEnumerable<TurmaViewModel>> GetTurmasComVagas(int unidadeId)
        {
            var query = @"select* from turma 
                        inner join Salas on 
                        turma.SalaId = Salas.Id 
                        where turma.TotalAlunos<> Salas.Capacidade AND 
                        turma.UnidadeId = @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var turmas = await connection.QueryAsync<TurmaViewModel>(query, new { unidadeId = unidadeId });

                return turmas;
            }
        }

        public async Task<IEnumerable<TurmaViewModel>> GetTurmasMatriculadosOutraUnidade(int alunoId, int unidadeId)
        {
            var query = @"select* from turma 
                        inner join Matriculados on 
                        turma.Id = Matriculados.TurmaId
                        where Matriculados.AlunoId = @alunoId
                        AND turma.unidadeId <> @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var turmas = await connection.QueryAsync<TurmaViewModel>(query, new { alunoId = alunoId, unidadeId = unidadeId });

                return turmas;
            }
        }
    }
}
