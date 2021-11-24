﻿using Dapper;
using Invictus.Dtos.AdmDtos;
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

                return alunos;

            }
        }        
    }
}