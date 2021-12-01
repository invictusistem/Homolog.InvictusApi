using Dapper;
using Invictus.Dtos.PedagDto;
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
    public class PedagDocsQueries : IPedagDocsQueries
    {
        private readonly IConfiguration _config;
        public PedagDocsQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<AlunoDocumentoDto>> GetDocsMatriculaViewModel(Guid matriculaId)
        {
            var query = @"select 
                        id,
                        matriculaId,
                        descricao,
                        comentario,
                        nome,
                        docEnviado,
                        analisado,
                        tamanho,
                        validado,
                        tipoArquivo,
                        contentArquivo,
                        --dataFile,
                        dataCriacao,
                        prazoValidade,
                        turmaId
                        from alunosDocumentos 
                        where alunosDocumentos.matriculaId = @matriculaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<AlunoDocumentoDto>(query, new { matriculaId = matriculaId });

                //connection.Close();

                return result;

            }
        }

        public async Task<AlunoDocumentoDto> GetDocumentById(Guid docId)
        {
            var query = @"select * from alunosDocumentos 
                        where alunosDocumentos.id = @docId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<AlunoDocumentoDto>(query, new { docId = docId });

                //connection.Close();

                return result;

            }
        }
    }
}
