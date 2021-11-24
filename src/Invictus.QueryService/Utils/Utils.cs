using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.Utilitarios
{
    public class Utils : IUtils
    {
        private readonly IConfiguration _config;
        private List<string> _inconsistencies;

        public Utils(IConfiguration config)
        {
            _config = config;
            _inconsistencies = new List<string>();
        }
        public async Task<IEnumerable<string>> ValidaDocumentosAluno(string cpf, string rg, string email)
        {

            if (!String.IsNullOrEmpty(cpf))
            {
                var query = @"select alunos.cpf from alunos where alunos.cpf = @cpf";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { cpf = cpf });
                    if (result.Count() > 0) _inconsistencies.Add("Já existe aluno com o CPF cadastrado.");
                }
            }

            if (!String.IsNullOrEmpty(rg))
            {
                var query = @"select alunos.rg from alunos where alunos.rg = @rg";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { rg = rg });
                    if (result.Count() > 0) _inconsistencies.Add("Já existe aluno com o RG cadastrado.");
                }
            }

            if (!String.IsNullOrEmpty(email))
            {
                var query = @"select alunos.email from alunos where alunos.email = @email";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { email = email });
                    if (result.Count() > 0) _inconsistencies.Add("Já existe aluno com o E-mail cadastrado.");
                }
            }

            return _inconsistencies;
        }

        public async Task<IEnumerable<string>> ValidaDocumentosColaborador(string cpf, string rg, string email)
        {
            if (!String.IsNullOrEmpty(cpf))
            {
                var query = @"select colaboradores.cpf from colaboradores where colaboradores.cpf = @cpf";
                var query2 = @"select professores.cpf from professores where professores.cpf = @cpf";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { cpf = cpf });
                    var result2 = await connection.QueryAsync<string>(query2, new { cpf = cpf });
                    if (result.Count() > 0 || result2.Count() > 0) _inconsistencies.Add("Já existe colaborador com o CPF cadastrado.");
                }
            }

            if (!String.IsNullOrEmpty(rg))
            {
                var query = @"select colaboradores.rg from colaboradores where colaboradores.rg = @rg";
                var query2 = @"select professores.rg from professores where professores.rg = @rg";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { rg = rg });
                    var result2 = await connection.QueryAsync<string>(query2, new { rg = rg });
                    if (result.Count() > 0 || result2.Count() > 0) _inconsistencies.Add("Já existe colaborador com o RG cadastrado.");
                }
            }

            if (!String.IsNullOrEmpty(email))
            {
                var query = @"select colaboradores.email from colaboradores where colaboradores.email = @email";
                var query2 = @"select professores.email from professores where professores.email = @email";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { email = email });
                    var result2 = await connection.QueryAsync<string>(query2, new { email = email });
                    if (result.Count() > 0 || result2.Count() > 0) _inconsistencies.Add("Já existe colaborador com o E-mail cadastrado.");
                }
            }

            return _inconsistencies;
        }

        public async Task<IEnumerable<string>> ValidaUnidade(string sigalUnidade)
        {
            var query = "SELECT * from Unidades where Unidades.sigla = @sigalUnidade";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<UnidadeDto>(query, new { sigalUnidade = sigalUnidade });

                connection.Close();

                if (result.Count() > 0) _inconsistencies.Add("Já existe unidade cadastrada com esta sigla.");

                return _inconsistencies;

            }
        }
    }
}
