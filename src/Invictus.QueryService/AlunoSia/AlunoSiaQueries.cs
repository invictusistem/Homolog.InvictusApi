using Dapper;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AlunoSia.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Invictus.QueryService.AlunoSia
{
    public class AlunoSiaQueries : IAlunoSiaQueries
    {
        private readonly IConfiguration _config;
        public AlunoSiaQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<DocumentoEstagioDto>> GetDocumentosEstagio(Guid matriculaId)
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
                        WHERE Matriculas.Id = 'D41BC26F-DC80-4F33-8EEC-3727B37846C0'";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var documentacao = await connection.QueryAsync<DocumentoEstagioDto>(query, new { matriculaId = matriculaId });

                connection.Close();

                return documentacao;
            }
        }

        public async Task<EstagioDto> GetEstagio(Guid estagioId)
        {
            var query = @"SELECT * FROM Estagios WHERE Estagios.id = @estagioId ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var estagio = await connection.QuerySingleAsync<EstagioDto>(query, new { estagioId = estagioId });

                connection.Close();

                return estagio;
            }
        }

        public async Task<IEnumerable<EstagioDto>> GetEstagiosLiberados(Guid tipoEstagioId)
        {
            var query = @"SELECT * FROM estagios WHERE Estagios.TipoEstagio = @tipoEstagioId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var documentacao = await connection.QueryAsync<EstagioDto>(query, new { tipoEstagioId = tipoEstagioId });

                connection.Close();

                return documentacao;
            }
        }

        public async Task<IEnumerable<TypeEstagioDto>> GetEstagiosTiposLiberadosDoAluno(Guid matriculaId)
        {
            var query = @"SELECT 
                        TypeEstagio.id,
                        TypeEstagio.Nome,
                        EstagiosMatriculas.matriculaId
                        FROM EstagiosMatriculas
                        INNER JOIN TypeEstagio on EstagiosMatriculas.TypeEstagioId = TypeEstagio.Id
                        WHERE EstagiosMatriculas.Status = 'Aguardando escolha do aluno'
                        AND EstagiosMatriculas.MatriculaId = @matriculaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var documentacao = await connection.QueryAsync<TypeEstagioDto>(query, new { matriculaId = matriculaId });

                connection.Close();

                return documentacao;
            }
        }
    }
}
