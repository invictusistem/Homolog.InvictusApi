using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using MoreLinq;
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
        //private readonly IEstagioQueries _estagioqueries;
        private readonly IConfiguration _config;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspNetUser;
        public EstagioQueries(IConfiguration config, IUnidadeQueries unidadeQueries, IAspNetUser aspNetUser)
        {
            _config = config;
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
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
            query.Append(@"SELECT 
                        alunos.id,  
                        alunos.cpf, 
                        alunos.rg, 
                        alunos.nascimento, 
                        alunos.dataCadastro, 
                        alunos.nome, 
                        alunos.ativo,
                        Matriculas.Id as matriculaId,
                        estagiosMatriculas.id as estagioMatriculaId,
                        estagiosMatriculas.status, 
                        turmas.descricao as turmaDescricao,
                        turmas.identificador as turmaIdentificador,
                        unidades.sigla 
                        FROM alunos 
                        INNER JOIN Matriculas on Alunos.id = Matriculas.AlunoId
                        FULL JOIN estagiosMatriculas on Matriculas.id = estagiosMatriculas.matriculaId 
                        INNER JOIN Unidades on Alunos.UnidadeId = Unidades.Id 
                        INNER JOIN Turmas on Matriculas.TurmaId = Turmas.Id  WHERE ");

            // query.Append("from alunos full join estagiosMatriculas on alunos.id = estagiosMatriculas.alunoid inner join Unidades on Alunos.UnidadeId = Unidades.Id WHERE ");
            if (param.todasUnidades == false) query.Append(@" Alunos.UnidadeId = @unidadeId AND ");
            if (param.nome != "") query.Append(@" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Alunos.Ativo = 'True' "); } else { query.Append(" (Alunos.Ativo = 'True' OR Alunos.Ativo = 'False') "); }
            query.Append(" ORDER BY Alunos.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ViewMatriculadosDto>(query.ToString(), 
                    new { nome = param.nome, unidadeId = unidadeId, currentPage = currentPage, itemsPerPage = itemsPerPage });

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
            queryCount.Append(@"SELECT count(*)  
                                FROM alunos 
                                INNER JOIN Matriculas on Alunos.id = Matriculas.AlunoId
                                FULL JOIN estagiosMatriculas on Matriculas.id = estagiosMatriculas.matriculaId 
                                INNER JOIN Unidades on Alunos.UnidadeId = Unidades.Id 
                                INNER JOIN Turmas on Matriculas.TurmaId = Turmas.Id  WHERE ");

            if (param.todasUnidades == false) queryCount.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Alunos.Ativo = 'True' "); } else { queryCount.Append(" (Alunos.Ativo = 'True' OR Alunos.Ativo = 'False') "); }


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

        public async Task<IEnumerable<TypeEstagioDto>> GetTiposDeEstagiosLiberadorParaAluno(Guid matriculaId)
        {
            var query = @"SELECT 
                        TypeEstagio.id,
                        TypeEstagio. nome,
                        TypeEstagio.nivel,
                        TypeEstagio.ativo,
                        TypeEstagio.observacao
                        FROM Estagios
                        inner join TypeEstagio on Estagios.TipoEstagio = TypeEstagio.Id
                        WHERE Estagios.TipoEstagio 
                        in (
                        SELECT TypeEstagio.Id FROM TypeEstagio 
                        WHERE TypeEstagio.Id 
                        not in 
                        (
                        SELECT Estagios.Id
                        FROM Estagios 
                        inner join EstagiosMatriculas ON Estagios.Id = EstagiosMatriculas.EstagioId
                        WHERE EstagiosMatriculas.MatriculaId = @matriculaId
                        )
                        )";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var estagioTipos = await connection.QueryAsync<TypeEstagioDto>(query, new { matriculaId  = matriculaId } );

                connection.Close();

                return estagioTipos.DistinctBy(t => t.id);
            }
        }

        public async Task<IEnumerable<DocumentoEstagioDto>> GetDocumentosDoEstagio(Guid matriculaId)
        {
            var query = @"SELECT 
                        EstagioDocumentos.id,
                        EstagioDocumentos.Descricao,
                        EstagioDocumentos.Nome,
                        EstagioDocumentos.Analisado,
                        EstagioDocumentos.Validado,
                        EstagioDocumentos.TipoArquivo,
                        EstagioDocumentos.nomeArquivo,
                        EstagioDocumentos.ContentArquivo,
                        EstagioDocumentos.dataFile,
                        EstagioDocumentos.DataCriacao,
                        EstagioDocumentos.Status
                        FROM EstagiosMatriculas
                        INNER JOIN EstagioDocumentos ON EstagiosMatriculas.Id = EstagioDocumentos.MatriculaEstagioId
                        INNER JOIN Matriculas ON EstagiosMatriculas.MatriculaId = Matriculas.Id
                        WHERE Matriculas.Id = @matriculaId";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var documentacao = await connection.QueryAsync<DocumentoEstagioDto>(query, new { matriculaId = matriculaId });

                connection.Close();

                return documentacao;
            }
        }

        public async Task<DocumentoEstagioDto> GetDocumentById(Guid estagioDocId)
        {
            var user = _aspNetUser.ObterUsuarioId();

            var query = @"SELECT * FROM EstagioDocumentos WHERE EstagioDocumentos.id = @estagioDocId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<DocumentoEstagioDto>(query, new { estagioDocId = estagioDocId });




                return result;

            }
        }

        public async Task<IEnumerable<string>> GetEstagioByName(string nome)
        {
            var query = "SELECT TypeEstagio.Nome FROM TypeEstagio WHERE LOWER(TypeEstagio.Nome) like LOWER('" + nome + "') " +
                        "collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<string>(query);

                return result;

            }

            
        }
    }
}
