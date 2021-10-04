using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Queries
{
    public class ComercialQueries : IComercialQueries
    {
        private readonly IConfiguration _config;
        public ComercialQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<ResultMetricaDto>> GetAlunosMatriculados()
        {
            string query = @"select 
                            Aluno.nome,
                            Aluno.email,
                            Matriculados.DiaMatricula 
                            from Aluno
                            inner join Matriculados 
                            on Aluno.Id = Matriculados.AlunoId 
                            where Aluno.id in (
                            select id from Matriculados
                            )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {   
                connection.Open();

                var list = await connection.QueryAsync<ResultMetricaDto>(query);

                connection.Close();

                return list;


            }
        }
    }

    public class ResultMetricaDto
    {
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime diaMatricula { get; set; }
    }
}
