using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class CalendarioQueries : ICalendarioQueries
    {
        private readonly IConfiguration _config;
        public CalendarioQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<AulaViewModel> GetAulaViewModel(Guid calendarioId)
        {
            var query = @"select 
                        Calendarios.id,
                        Calendarios.materiaId,
                        Calendarios.professorId,
                        Calendarios.salaId,
                        Calendarios.DiaDaSemana,
                        Calendarios.turmaId,
                        turmas.Descricao,
                        turmas.Identificador,
                        turmas.unidadeId,
                        Professores.Id as professorId,
                        Professores.Nome,
                        UnidadesSalas.Titulo,
                        MateriasTemplate.Nome as materiaDescricao,
                        Calendarios.DiaAula,
                        Calendarios.HoraInicial,
                        Calendarios.HoraFinal,
                        Calendarios.AulaIniciada,
                        Calendarios.AulaConcluida,
                        Calendarios.DateAulaIniciada,
                        Calendarios.DateAulaConcluida, 
                        Calendarios.Observacoes
                        from Calendarios 
                        left join turmas on Calendarios.TurmaId = Turmas.Id 
                        left join Professores on Calendarios.ProfessorId = Professores.Id
                        left join UnidadesSalas on Calendarios.SalaId = UnidadesSalas.Id
                        left join MateriasTemplate on Calendarios.MateriaId = MateriasTemplate.Id
                        where Calendarios.id = @calendarioId ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var result = await connection.QuerySingleAsync<AulaViewModel>(query, new { calendarioId = calendarioId });

                connection.Close();
                // 2022 01 30

                return result;
            }
        }

        public async Task<CalendarioDto> GetCalendarioById(Guid calendarioId)
        {
            var query = @"SELECT * FROM Calendarios WHERE Calendarios.Id = @calendarioId";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QuerySingleAsync<CalendarioDto>(query, new { calendarioId = calendarioId });

                connection.Close();// 2022 01 30


                return results;
            }
        }

        public async Task<IEnumerable<CalendarioDto>> GetCalendarioByProfessorId(Guid professorId)
        {
            var query = @"SELECT * FROM Calendarios WHERE Calendarios.ProfessorId = @professorId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                
                var results = await connection.QueryAsync<CalendarioDto>(query, new { professorId = professorId });

                connection.Close();

                return results;
            }
        }

        public async Task<IEnumerable<TurmaCalendarioViewModel>> GetCalendarioByTurmaId(Guid turmaId)
        {
            var query = @"select 
                        calendarios.Id,
                        calendarios.diaaula,
                        calendarios.diadasemana,
                        calendarios.horainicial,
                        calendarios.horafinal,
                        calendarios.aulainiciada,
                        calendarios.aulaconcluida,
                        calendarios.observacoes,
                        Unidadessalas.titulo,
                        materiastemplate.nome,
                        Professores.Nome as professor 
                        from calendarios 
                        left join Unidadessalas on Calendarios.SalaId = Unidadessalas.Id
                        left join materiastemplate on Calendarios.MateriaId = materiastemplate.Id 
                        left join Professores on Calendarios.ProfessorId = Professores.Id
                        where Calendarios.TurmaId = @turmaId 
                        order by Calendarios.DiaAula asc ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<TurmaCalendarioViewModel>(query, new { turmaId = turmaId });

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

        public async Task<IEnumerable<CalendarioDto>> GetFutureCalendarsByProfessorIdAndUnidadeId(Guid unidadeId, Guid professorId)
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            var query = @"SELECT * FROM Calendarios WHERE Calendarios.ProfessorId = @professorId 
                        AND Calendarios.UnidadeId = @unidadeId AND Calendarios.DiaAula >= @hoje AND Calendarios.AulaIniciada = 'false' ORDER BY DiaAula";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<CalendarioDto>(query, new { professorId = professorId, unidadeId = unidadeId, hoje = hoje });

                connection.Close();

                return results;
            }
        }

        public async Task<TurmaCalendarioViewModel> GetCalendarioViewModelById(Guid calendarioId)
        {
            var query = @"select 
                        calendarios.Id,
                        calendarios.diaaula,
                        calendarios.diadasemana,
                        calendarios.horainicial,
                        calendarios.horafinal,
                        calendarios.aulainiciada,
                        calendarios.aulaconcluida,
                        calendarios.observacoes,
                        Unidadessalas.titulo,
                        materiastemplate.nome,
                        Professores.Nome as professor 
                        from calendarios 
                        left join Unidadessalas on Calendarios.SalaId = Unidadessalas.Id
                        left join materiastemplate on Calendarios.MateriaId = materiastemplate.Id 
                        left join Professores on Calendarios.ProfessorId = Professores.Id
                        where Calendarios.id = @calendarioId 
                        order by Calendarios.DiaAula asc ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var result = await connection.QuerySingleAsync<TurmaCalendarioViewModel>(query, new { calendarioId = calendarioId });

                connection.Close();
                // 2022 01 30

                var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                //foreach (var cal in results)
                //{

                //Debug.WriteLine(diaSeguinte);
                if (result.diaaula < hoje)
                {
                    result.podeVerRelatorioAula = true;
                }
                else if (result.diaaula == hoje)
                {
                    result.podeVerRelatorioAula = null;
                }
                else
                {
                    result.podeVerRelatorioAula = false;
                }

                //}

                return result;
            }
        }

        public async Task<IEnumerable<CalendarioDto>> GetFutureCalendarsByProfessorIdAndMateriaId(Guid materiaId, Guid professorId)
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            var query = @"SELECT * FROM Calendarios WHERE Calendarios.ProfessorId = @professorId 
                        AND Calendarios.materiaId = @materiaId AND Calendarios.DiaAula >= @hoje AND Calendarios.AulaIniciada = 'false' ORDER BY DiaAula";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<CalendarioDto>(query, new { professorId = professorId, materiaId = materiaId, hoje = hoje });

                connection.Close();

                return results;
            }
        }

        public async Task<IEnumerable<CalendarioDto>> GetFutureCalendarsByTurmaIdAndMateriaId(Guid materiaId, Guid turmaId)
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            var query = @"SELECT * FROM Calendarios WHERE Calendarios.turmaId = @turmaId 
                        AND Calendarios.materiaId = @materiaId AND Calendarios.DiaAula >= @hoje AND Calendarios.AulaIniciada = 'false' ORDER BY DiaAula";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<CalendarioDto>(query, new { turmaId = turmaId, materiaId = materiaId, hoje = hoje });

                connection.Close();

                return results;
            }
        }
    }
}
