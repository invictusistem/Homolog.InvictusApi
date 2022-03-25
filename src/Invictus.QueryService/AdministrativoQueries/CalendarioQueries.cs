using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public async Task<TurmaCalendarioViwModel> GetCalendarioById(Guid calendarioId)
        {
            var query = @"SELECT * FROM Calendarios WHERE Calendarios.Id = @calendarioId";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QuerySingleAsync<TurmaCalendarioViwModel>(query, new { calendarioId = calendarioId });

                connection.Close();// 2022 01 30


                return results;
            }
        }

        public async Task<IEnumerable<TurmaCalendarioViwModel>> GetCalendarioByProfessorId(Guid professorId)
        {
            var query = @"SELECT * FROM Calendarios WHERE Calendarios.ProfessorId = @professorId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaCalendarioViwModel>(query, new { professorId = professorId });

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
                        materiastemplate.id as materiaId,
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

                var hoje = DateTime.Now;

                //foreach (var cal in results)
                //{

                //    //Debug.WriteLine(diaSeguinte);
                //    if (cal.diaaula < hoje)
                //    {
                //        cal.podeVerRelatorioAula = true;
                //    }
                //    else if (cal.diaaula == hoje)
                //    {
                //        cal.podeVerRelatorioAula = null;
                //    }
                //    else
                //    {
                //        cal.podeVerRelatorioAula = false;
                //    }

                //}

                foreach (var cal in results)
                {

                    // var calendario = await connection.QueryAsync<CalendarioDto>(query2, new { turmaId = turma.id });


                    //if (calendario.Any())
                    //{
                    //    turma.calendarioId = calendario.Select(c => c.id).First();


                    var horaCompletaAula = cal.diaaula;
                    var horaInicio = cal.horainicial.Split(":");// calendario.Select(c => c.horaInicial).First().Split(":");
                    var horaFinal = cal.horafinal.Split(":");// calendario.Select(c => c.horaFinal).First().Split(":");

                    horaCompletaAula = new DateTime(horaCompletaAula.Year, horaCompletaAula.Month, horaCompletaAula.Day,
                        Convert.ToInt32(horaInicio[0]), Convert.ToInt32(horaInicio[1]), 0);

                    var horaCompletaFinal = new DateTime(horaCompletaAula.Year, horaCompletaAula.Month, horaCompletaAula.Day,
                        Convert.ToInt32(horaFinal[0]), Convert.ToInt32(horaFinal[1]), 0);
                    var timeSpan = new TimeSpan(0, 15, 0);

                    var iniciar = horaCompletaAula - timeSpan;

                    if (hoje < horaCompletaFinal & hoje >= horaCompletaAula - timeSpan)
                    {
                        //hoje.podeIniciarAula = true;
                        cal.podeVerRelatorioAula = null;
                    }
                    else
                    {
                        if (hoje < horaCompletaAula - timeSpan)
                        {
                            cal.podeVerRelatorioAula = false;
                        }
                        else
                        {
                            cal.podeVerRelatorioAula = true;
                        }
                    }
                }

                return results;
            }
        }

        public async Task<IEnumerable<TurmaCalendarioViwModel>> GetFutureCalendarsByProfessorIdAndUnidadeId(Guid unidadeId, Guid professorId)
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            var query = @"SELECT * FROM Calendarios WHERE Calendarios.ProfessorId = @professorId 
                        AND Calendarios.UnidadeId = @unidadeId AND Calendarios.DiaAula >= @hoje AND Calendarios.AulaIniciada = 'false' ORDER BY DiaAula";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaCalendarioViwModel>(query, new { professorId = professorId, unidadeId = unidadeId, hoje = hoje });

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

        public async Task<IEnumerable<TurmaCalendarioViwModel>> GetFutureCalendarsByProfessorIdAndMateriaId(Guid materiaId, Guid professorId)
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            var query = @"SELECT * FROM Calendarios WHERE Calendarios.ProfessorId = @professorId 
                        AND Calendarios.materiaId = @materiaId AND Calendarios.DiaAula >= @hoje AND Calendarios.AulaIniciada = 'false' ORDER BY DiaAula";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaCalendarioViwModel>(query, new { professorId = professorId, materiaId = materiaId, hoje = hoje });

                connection.Close();

                return results;
            }
        }

        public async Task<IEnumerable<TurmaCalendarioViwModel>> GetFutureCalendarsByTurmaIdAndMateriaId(Guid materiaId, Guid turmaId)
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            var query = @"SELECT * FROM Calendarios WHERE Calendarios.turmaId = @turmaId 
                        AND Calendarios.materiaId = @materiaId AND Calendarios.DiaAula >= @hoje AND Calendarios.AulaIniciada = 'false' ORDER BY DiaAula";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaCalendarioViwModel>(query, new { turmaId = turmaId, materiaId = materiaId, hoje = hoje });

                connection.Close();

                return results;
            }
        }

        public async Task<PaginatedItemsViewModel<TurmaCalendarioViewModel>> GetCalendarioPaginatedByTurmaId(Guid turmaId, int itemsPerPage, int currentPage, string paramsJson)
        {
            var parametros = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var allDatesQuery = @"select 
                                    calendarios.diaaula
                                    from calendarios                        
                                    where Calendarios.TurmaId = @turmaId 
                                    order by Calendarios.DiaAula asc  "; // apenas se 

            var calendarioQuery = @"select 
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
                        order by Calendarios.DiaAula asc 
                        OFFSET( @currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY ";

            var calendariosCount = @"select Count(*) from calendarios where Calendarios.TurmaId = @turmaId "; // apenas se 


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                
                if (parametros.primeiraReq)
                {
                    var dates = await connection.QueryAsync<DateTime>(allDatesQuery, new { turmaId = turmaId });

                    connection.Close();

                    var data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                    var closestTime = dates.OrderBy(t => Math.Abs((t - data).Ticks))
                                 .First();

                    int index = dates.ToList().IndexOf(closestTime);

                    decimal number = (index + 1) / itemsPerPage;

                    currentPage = Convert.ToInt32(Math.Floor(number)) + 1;

                    //Debug.WriteLine("");
                }

                var results = await connection.QueryAsync<TurmaCalendarioViewModel>(calendarioQuery, new { itemsPerPage = itemsPerPage, currentPage = currentPage, turmaId = turmaId });

                var count = await connection.QuerySingleAsync<int>(calendariosCount, new { turmaId = turmaId });

                Debug.WriteLine("");

                var hoje = DateTime.Now;

                //foreach (var cal in results)
                //{

                //    //Debug.WriteLine(diaSeguinte);
                //    if (cal.diaaula < hoje)
                //    {
                //        cal.podeVerRelatorioAula = true;
                //    }
                //    else if (cal.diaaula == hoje)
                //    {
                //        cal.podeVerRelatorioAula = null;
                //    }
                //    else
                //    {
                //        cal.podeVerRelatorioAula = false;
                //    }

                //}

                foreach (var cal in results)
                {

                    // var calendario = await connection.QueryAsync<CalendarioDto>(query2, new { turmaId = turma.id });


                    //if (calendario.Any())
                    //{
                    //    turma.calendarioId = calendario.Select(c => c.id).First();


                    var horaCompletaAula = cal.diaaula;
                    var horaInicio = cal.horainicial.Split(":");// calendario.Select(c => c.horaInicial).First().Split(":");
                    var horaFinal = cal.horafinal.Split(":");// calendario.Select(c => c.horaFinal).First().Split(":");

                    horaCompletaAula = new DateTime(horaCompletaAula.Year, horaCompletaAula.Month, horaCompletaAula.Day,
                        Convert.ToInt32(horaInicio[0]), Convert.ToInt32(horaInicio[1]), 0);

                    var horaCompletaFinal = new DateTime(horaCompletaAula.Year, horaCompletaAula.Month, horaCompletaAula.Day,
                        Convert.ToInt32(horaFinal[0]), Convert.ToInt32(horaFinal[1]), 0);
                    var timeSpan = new TimeSpan(0, 15, 0);

                    var iniciar = horaCompletaAula - timeSpan;

                    if (hoje < horaCompletaFinal & hoje >= horaCompletaAula - timeSpan)
                    {
                        //hoje.podeIniciarAula = true;
                        cal.podeVerRelatorioAula = null;
                    }
                    else
                    {
                        if (hoje < horaCompletaAula - timeSpan)
                        {
                            cal.podeVerRelatorioAula = false;
                        }
                        else
                        {
                            cal.podeVerRelatorioAula = true;
                        }
                    }
                }

                var paginatedItems = new PaginatedItemsViewModel<TurmaCalendarioViewModel>(currentPage, itemsPerPage, count, results.ToList());

                return paginatedItems;
            }
        }
    }
}
