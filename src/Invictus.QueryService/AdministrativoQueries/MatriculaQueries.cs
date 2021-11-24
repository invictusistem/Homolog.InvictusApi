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
    public class MatriculaQueries : IMatriculaQueries
    {
        private readonly IConfiguration _config;
        private readonly ITypePacoteQueries _types;
        private readonly ITurmaQueries _turmaQueries;
        public MatriculaQueries(IConfiguration config, ITypePacoteQueries types, ITurmaQueries turmaQueries)
        {
            _config = config;
            _types = types;
            _turmaQueries = turmaQueries;
        }
        public async Task<IEnumerable<TypePacoteDto>> GetTypesLiberadorParaMatricula(Guid alunoId)
        {
            /*
              // ver os Ids das turmas onde ele está matriculado na table Matriculas
                // turma 1 - pacodeId - typepacoteId
                // turma 2 - Pacote id - typePacoteId
            //ver os pacotesId e buscas os Types diferentes destes, se nao tiver, nao tem tipo liberado
            //se nao, retorna os typepra ele escolher


            -- ver se está matriculados
select Matriculas.TurmaId from Matriculas WHERE Matriculas.AlunoId = '123123'
-- retourou vazio, liberar todos os types
-- se retornou mais que um, prosseguir

-- pegar os pacotes ids das tumas
select Turmas.pacoteId from Turmas WHERE Turmas.Id in ('f2bb154b-c45d-4347-a0dc-391f2c5a22f3')

--pegas o TYPEPACOTES ID
select Pacotes.TypePAcoteId from Pacotes WHERE Pacotes.id not in ('f2bb154b-c45d-4347-a0dc-391f2c5a22f3','7c0b5f64-5724-4196-6c0d-08d9a86e2c53')

-- PEGAS OS OBJETOS TYPES


            */

            var typePacotesTurmasMatriculadas = await _turmaQueries.GetTypePacotesTurmasMatriculadas(alunoId);// GetTypePacotesTurmasMatriculadas(alunoId);


            //var turmasMatriculadas = await GetTurmasMatriculadas(alunoId);

            if (typePacotesTurmasMatriculadas.Count() == 0) return await _types.GetTypePacotes();

            //var getTypePacotesDistintos = await GetTypePacotesLiberador();


           // var pacoteIdDasTurmasMatriculadas = await GetPacoteIdDasTurmas(turmasMatriculadas);

            //var typesIdss = await GetPacotesLiberados(typePacotesTurmasMatriculadas);

            //return await GetTypePacotes(typesIdss);

            return await GetPacotesLiberados(typePacotesTurmasMatriculadas);


        }

        private async Task<IEnumerable<TypePacoteDto>> GetTypePacotes(IEnumerable<Guid> pacoteIds)
        {
            var query = @"select * from typepacote WHERE typepacote.id not in @pacoteIds";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TypePacoteDto>(query, new { pacoteIds = pacoteIds.ToArray() });

                connection.Close();

                return results;

            }
        }

        private async Task<IEnumerable<TypePacoteDto>> GetPacotesLiberados(IEnumerable<Guid> typePacotesIds)
        {
            var query = @"select * from TypePacote Where TypePacote.id not in @typePacotesIds";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TypePacoteDto>(query, new { typePacotesIds = typePacotesIds.ToArray() });

                connection.Close();

                return results;

            }
        }

        private async Task<IEnumerable<Guid>> GetPacoteIdDasTurmas(IEnumerable<Guid> idTurmas)
        {
            var query = @"select Turmas.pacoteId from Turmas WHERE Turmas.Id in @idTurmas";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<Guid>(query, new { idTurmas = idTurmas.ToArray() });

                connection.Close();

                return results;

            }
        }

        private async Task<IEnumerable<Guid>> GetTurmasMatriculadas(Guid alunoId)
        {
            var query = @"select Matriculas.TurmaId from Matriculas WHERE Matriculas.AlunoId = @alunoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<Guid>(query, new { alunoId = alunoId });

                connection.Close();

                return results;

            }
        }

        public async Task<int> TotalMatriculados()
        {
            var query = @"SELECT Count(*) FROM Matriculas";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var qnt = await connection.QuerySingleAsync<int>(query);

                connection.Close();

                return qnt;

            }
        }
    }
}
