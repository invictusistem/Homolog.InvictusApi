using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Application.Queries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Invictus.Application.Queries
{
    public class CalendarioQueries : ICalendarioQueries
    {
        private readonly IConfiguration _config;

        public CalendarioQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<TurmaCalendarioViewModel>> GetCalendarioByTurmaId(int turmaId)
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
                        salas.titulo,
                        materias.descricao,
                        Colaborador.Nome
                        from calendarios 
                        left join Salas on Calendarios.SalaId = Salas.Id
                        left join Materias on Calendarios.MateriaId = materias.Id 
                        left join Colaborador on Calendarios.ProfessorId = Colaborador.Id
                        where Calendarios.TurmaId = @turmaId 
                        order by Calendarios.DiaAula asc";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<TurmaCalendarioViewModel>(query, new { turmaId = turmaId });

                connection.Close();

                return results;
            }
        }

        /*
select * from calendarios where DiaAula >= '2021-07-15' 
AND DiaAula <= '2021-08-12' 
AND Turno <> 'IntegralManhaTardeNoite'
order by DiaAula asc
* */
        public async Task<IEnumerable<CalendarioDto>> GetDatas(DateTime inicio, DateTime fim)
        {
            //var query = @"select * from calendarios where DiaAula >= @inicio 
            //            AND DiaAula <= @fim 
            //            AND Turno<> 'IntegralManhaTardeNoite' 
            //            order by DiaAula asc";

            var query = @"select * from calendarios where DiaAula >= @inicio 
                        AND DiaAula <= @fim                        
                        order by DiaAula asc";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<CalendarioDto>(query, new
                { inicio = inicio, fim = fim });

                connection.Close();

                return results;
            }
        }
    }



}
