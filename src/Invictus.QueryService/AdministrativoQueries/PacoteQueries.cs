using Dapper;
using Invictus.Core.Interfaces;
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
    public class PacoteQueries : IPacoteQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidadeQueries;
        public PacoteQueries(IConfiguration config, IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries)
        {
            _config = config;
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
        }

        public async Task<IEnumerable<DocumentacaoExigidaDto>> GetDocsByPacoteId(Guid pacoteId)
        {
            var query = @"SELECT * FROM PacotesDocumentacao WHERE PacotesDocumentacao.PacoteId = @pacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<DocumentacaoExigidaDto>(query, new { pacoteId = pacoteId });

                connection.Close();

                return results;

            }
        }

        public async Task<IEnumerable<PacoteMateriaDto>> GetMateriasPacote(Guid pacoteId)
        {
            var query = @"SELECT * FROM PacotesMaterias WHERE PacotesMaterias.PacoteId = @pacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PacoteMateriaDto>(query, new { pacoteId = pacoteId });

                connection.Close();

                return results;

            }
        }

        public async Task<IEnumerable<string>> GetPacoteByDescricao(string descricao)
        {
            var query = @"SELECT Pacotes.descricao FROM Pacotes WHERE LOWER(Pacotes.descricao) = LOWER(@descricao) collate SQL_Latin1_General_CP1_CI_AI ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<string>(query, new { descricao = descricao });

                connection.Close();

                return results;

            }
        }

        public async Task<PacoteDto> GetPacoteById(Guid pacoteId)
        {
            var query = @"SELECT * FROM Pacotes WHERE Pacotes.Id = @pacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<PacoteDto>(query, new { pacoteId = pacoteId });

                connection.Close();

                return results;

            }
        }

        public async Task<PacoteDtoTeste> GetPacoteByIdTeste(Guid pacoteId)
        {
            var query = @"SELECT * FROM Pacotes WHERE Pacotes.Id = @pacoteId";
            var query1 = @"SELECT * FROM PacotesMaterias WHERE PacotesMaterias.PacoteId = @pacoteId";
            var query2 = @"SELECT * FROM PacotesDocumentacao WHERE PacotesDocumentacao.PacoteId  = @pacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<PacoteDtoTeste>(query, new { pacoteId = pacoteId });

                results.materias = new List<PacoteMateriaDto>();
                var materias = await connection.QueryAsync<PacoteMateriaDto>(query1, new { pacoteId = pacoteId });
                results.materias = materias.ToList();

                results.DocumentosExigidos = new List<DocumentacaoExigDto>();
                var docs = await connection.QueryAsync<DocumentacaoExigDto>(query2, new { pacoteId = pacoteId });
                results.DocumentosExigidos = docs.ToList();


                connection.Close();

                return results;

            }
        }

        public async Task<IEnumerable<PacoteDto>> GetPacotes(Guid typePacoteId, Guid unidadeId)
        {
            var query = @"SELECT * FROM Pacotes WHERE Pacotes.TypePacoteId = @typePacoteId AND  Pacotes.UnidadeId = @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<PacoteDto>(query, new { typePacoteId = typePacoteId, unidadeId = unidadeId });

                connection.Close();

                return results;

            }
        }

        public async Task<IEnumerable<PacoteDto>> GetPacotesByUserUnidade(Guid typePacoteId)
        {
            var siglaUnidade = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(siglaUnidade);
            var pacotes = await GetPacotes(typePacoteId, unidade.id);
            return pacotes;
        }
    }
}
