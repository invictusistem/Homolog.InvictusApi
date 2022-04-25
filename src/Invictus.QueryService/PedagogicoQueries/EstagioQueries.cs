using Dapper;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries
{
    public class EstagioQueries : IEstagioQueries
    {
        private readonly IConfiguration _config;
        private readonly IUnidadeQueries _unidadeQueries;
        public EstagioQueries(IConfiguration config, IUnidadeQueries unidadeQueries)
        {
            _config = config;
            _unidadeQueries = unidadeQueries;
        }
        public async Task<EstagioDto> GetEstagioById(Guid estagioId)
        {
            var query = @"SELECT * FROM Estagios WHERE Estagios.id = @estagioId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var estagio = await connection.QuerySingleAsync<EstagioDto>(query, new { estagioId = estagioId });

                return estagio;

            }
        }

        public async Task<IEnumerable<EstagioDto>> GetEstagios()
        {
            var query = @"SELECT * FROM Estagios";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var estagios = await connection.QueryAsync<EstagioDto>(query);

                return estagios;

            }
        }

        public async Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetMatriculadosView(int itemsPerPage, int currentPage, string paramsJson)
        {          
                //var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);
                //var uindade = await _unidadeQueries.GetUnidadeDoUsuario();


                // var alunosNaBase = await GetIdsAlunosDaBase(param, uindade.id);

                // get alunos na Table Alunos
                /*
                 lista Alunos
                 aluno.temMatricula ? sim =

                aluno.temMatricula ?

                 */
                /*
                 select alunos.id,
    matriculas.numeromatricula, 
    matriculas.cpf, 
    alunos.nome,
    matriculas.id as matriculaId,
    unidades.sigla
    from alunos
    full join matriculas on alunos.id = matriculas.alunoid
    inner join Unidades on Alunos.UnidadeId = Unidades.Id
    WHERE Alunos.nome = 'Mario Gomes'
                 */

                // old
                var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);
                var unidade = await _unidadeQueries.GetUnidadeDoUsuario();

                var alunos = await GetAlunosByFilter(itemsPerPage, currentPage, param, unidade.id);

                var alunosCount = await CountAlunosByFilter(itemsPerPage, currentPage, param, unidade.id);

                var paginatedItems = new PaginatedItemsViewModel<ViewMatriculadosDto>(currentPage, itemsPerPage, alunosCount, alunos.ToList());

                return paginatedItems;
            
        }

        public async Task<IEnumerable<ViewMatriculadosDto>> GetAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("select alunos.id,  alunos.cpf, alunos.rg, alunos.nascimento, alunos.dataCadastro, alunos.nome, alunos.ativo, estagiosMatriculas.id as matriculaId, unidades.sigla ");
            query.Append("from alunos full join estagiosMatriculas on alunos.id = estagiosMatriculas.alunoid inner join Unidades on Alunos.UnidadeId = Unidades.Id WHERE ");
            if (param.todasUnidades == false) query.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Alunos.Ativo = 'True' "); } else { query.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); }
            query.Append(" ORDER BY Alunos.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ViewMatriculadosDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                if (results.Count() > 0)
                   // results = BindCPF(results.ToList());

                connection.Close();

                return results;
            }
        }

        private async Task<int> CountAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {
            // estagiosMatriculas
            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("select Count(*) ");
            queryCount.Append("from alunos full join estagiosMatriculas on alunos.id = estagiosMatriculas.alunoid inner join Unidades on Alunos.UnidadeId = Unidades.Id WHERE ");
            if (param.todasUnidades == false) queryCount.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Alunos.Ativo = 'True' "); } else { queryCount.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); }


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString());

                connection.Close();

                return countItems;

            }
        }

        public async Task<IEnumerable<TypeEstagioDto>> GetTiposDeEstagios()
        {
            // estagiosMatriculas
            var query = @"SELECT * FROM TypeEstagio";
            

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var estagioTipos = await connection.QueryAsync<TypeEstagioDto>(query);

                connection.Close();

                return estagioTipos;

            }
        }
    }
}
