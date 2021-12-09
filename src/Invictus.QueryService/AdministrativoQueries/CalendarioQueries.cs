using Dapper;
using Invictus.Dtos.AdmDtos;
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
    public class CalendarioQueries : ICalendarioQueries
    {
        private readonly IConfiguration _config;
        public CalendarioQueries(IConfiguration config)
        {
            _config = config;
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

                return results;
            }
        }
    }
}
