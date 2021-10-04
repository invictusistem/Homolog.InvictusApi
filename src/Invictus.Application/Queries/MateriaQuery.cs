using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;

namespace Invictus.Application.Queries
{
    public class MateriaQuery : IMateriaQueries
    {
        private readonly IConfiguration _config;
        public MateriaQuery(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<ProfessoresMateriaDto>> GetMaterias(int turmaId, int profId)
        {
            //string query = @"select * from ProfessoresMateria where ProfessoresMateria.profId in (0," + profId + @") and ProfessoresMateria.TurmaId = @turmaId ";

            var query1 = @"select id as materiaId, descricao from materias 
                            where Materias.Id not in(
                            select materiaId from MateriasDaTurma
                            where profId not in (@profId)
                            )";

            //string query2 = @"select 
            //                Materias.id,
            //                Materias.Descricao
            //                from materias 
            //                where id in (
            //                select materiaId from materiasDaTurma 
            //                where ProfId = @profId
            //                )";


            var query2 = @"select 
                            MateriasDaTurma.MateriaId as id,
                            Materias.Descricao
                            from ProfessorNew
                            left join MateriasdaTurma on ProfessorNew.Id = MateriasDaTurma.ProfessorId
                            left join Materias on MateriasDaTurma.MateriaId = Materias.Id 
                            where MateriasDaTurma.ProfId = @profId  
                            and ProfessorNew.TurmaId = @turmaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var materias = await connection.QueryAsync<ProfessoresMateriaDto>(query1, new { profId = profId });
                var professoresMateria = await connection.QueryAsync<ProfessoresMateriaDto>(query2, new { profId = profId, turmaId = turmaId });

                List<int> ids = new List<int>();

                foreach (var item in professoresMateria)
                {
                    ids.Add(item.id);
                }

                foreach (var item in materias)
                {
                    foreach (var item2 in ids)
                    {
                        if (item.materiaId == item2)
                        {
                            item.temProfessor = true;
                        }

                    }

                }
                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return materias;

            }
        }
    }
}
