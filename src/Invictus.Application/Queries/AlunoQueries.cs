using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Application.Queries
{
    public class AlunoQueries : IAlunoQueries
    {
        private readonly IConfiguration _config;
        public AlunoQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<AlunoDto> GetAluno(int alunoId)
        {
            string query = @"select
                            *
                            from Aluno 
                            left join Responsaveis on aluno.id = Responsaveis.AlunoId
                            where Aluno.id = @alunoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                //var profsDictionary = new Dictionary<int, ProfessoresDto>();
                connection.Open();

                //var profs = await connection.QueryAsync<ProfessoresDto>(query, new { turmaId = turmaId });

                //var listIds = new List<int>();
                //if (profs.Count() > 0)
                //{
                //    foreach (var item in profs)
                //    {
                //        var id = item.id;
                //        item.materias = await connection.QueryAsync<MateriaDto>(query2, new { turmaId = turmaId, id = id });
                //    }
                //}            


                var alunoDictionary = new Dictionary<int, AlunoDto>();

                var list = connection.Query<AlunoDto, Resp, AlunoDto>(query,
                    (alunoDto, respDto) =>
                    {
                        AlunoDto alunoentry;

                        if (!alunoDictionary.TryGetValue(alunoDto.id, out alunoentry))
                        {
                            alunoentry = alunoDto;
                            alunoentry.responsaveis = new List<Resp>();
                            alunoDictionary.Add(alunoentry.id, alunoentry);
                        }

                        if (respDto != null)
                        {
                            alunoentry.responsaveis.Add(respDto);
                        }
                        return alunoentry;

                    }, new { alunoId = alunoId }, splitOn: "Id").Distinct().ToList();

                //foreach (var item in list)
                //{
                //    if (item.materias[0].id == 0)
                //    {
                //        item.materias = null;
                //        item.materias = new List<ProfessoresMateriaDto>();
                //    }
                //}



                return list.FirstOrDefault();


            }
        }

        public async Task<IEnumerable<DocumentoAlunoDto>> GetDocsAluno(int alunoId)
        {
            var query = @"select * from documento_aluno 
                        where documento_aluno.AlunoId = @alunoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<DocumentoAlunoDto>(query, new { alunoId = alunoId });

                connection.Close();

                return results;
            }
        }
    }
}
