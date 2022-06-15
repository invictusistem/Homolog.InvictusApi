using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class PessoaQueries : IPessoaQueries
    {
        private readonly IConfiguration _config;
        public PessoaQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<PessoaDto> GetFornecedorById(Guid fornecedorId)
        {
            string query = @"SELECT * FROM Pessoas WHERE Pessoas.id = @fornecedorId AND Pessoas.tipoPessoa = 'Fornecedor' ";

            await using (var _connect = new SqlConnection(_config.GetConnectionString("InvictusConnection")))
            {
                _connect.Open();

                var pessoa = await _connect.QueryAsync(query, new { fornecedorId = fornecedorId });

                return pessoa.FirstOrDefault();

            }
        }
    }
}
