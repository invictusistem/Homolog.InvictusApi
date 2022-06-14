using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
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
    public class Validations
    {
        public bool cpf { get; set; } = false;
        public bool rg { get; set; } = false;
        public bool email { get; set; } = false;
    }
    public class Utils : IUtils
    {
        private readonly IConfiguration _config;
        private readonly IUnidadeQueries _unidadeQueries;
        private List<string> _inconsistencies;
        private Validations _validations;
        public Utils(IConfiguration config, IUnidadeQueries unidadeQueries)
        {
            _config = config;
            _inconsistencies = new List<string>();
            _unidadeQueries = unidadeQueries;
            _validations = new Validations();
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
                    if (result.Count() > 0)
                    {
                        _validations.cpf = true;
                        _inconsistencies.Add("Já existe aluno com o CPF cadastrado.");
                    }
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
                var query2 = @"select colaboradores.email from colaboradores where colaboradores.email = @email";
                var query3 = @"select professores.email from professores where professores.email = @email";
                // var query4 = @"select fornecedores.email from fornecedores where fornecedores.email = @email";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { email = email });
                    var result2 = await connection.QueryAsync<string>(query2, new { email = email });
                    var result3 = await connection.QueryAsync<string>(query3, new { email = email });
                    // var result4 = await connection.QueryAsync<string>(query4, new { email = email });

                    if (result.Count() > 0 || result2.Count() > 0 || result3.Count() > 0) _inconsistencies.Add("Por favor, escolha outro e-mail.");
                }
            }

            return _inconsistencies;
        }

        public async Task<IEnumerable<string>> ValidaDocumentoPessoa(string cpf, string rg, string email)
        {
            if (!String.IsNullOrEmpty(cpf))
            {
                var query = @"SELECT Pessoas.cpf FROM Pessoas WHERE Pessoas.cpf = @cpf";
                var query2 = @"SELECT Pessoas.Email FROM Pessoas WHERE Pessoas.Email = @email";
                var query3 = @"SELECT Pessoas.RG FROM Pessoas WHERE Pessoas.RG = @rg";

                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { cpf = cpf });
                    if (result.Count() > 0)
                    {
                        _validations.cpf = true;
                        _inconsistencies.Add("Favor, escolha outro CPF.");
                    }

                    var result2 = await connection.QueryAsync<string>(query2, new { email = email });
                    if (result2.Count() > 0)
                    {
                        _validations.email = true;
                        _inconsistencies.Add("Favor, escolha outro e-mail.");
                    }

                    var result3 = await connection.QueryAsync<string>(query3, new { rg = rg });

                    if (result3.Count() > 0)
                    {
                        _validations.rg = true;
                        _inconsistencies.Add("Favor, escolha outro RG.");
                    }
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
                var query3 = @"select fornecedores.cnpj_cpf as cpf from fornecedores where fornecedores.cnpj_cpf = @cpf";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { cpf = cpf });
                    var result2 = await connection.QueryAsync<string>(query2, new { cpf = cpf });
                    var result3 = await connection.QueryAsync<string>(query3, new { cpf = cpf });
                    if (result.Count() > 0 || result2.Count() > 0 || result3.Count() > 0)
                    {
                        _validations.cpf = true;
                        _inconsistencies.Add("Favor, escolha outro CPF.");
                    }
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
                    if (result.Count() > 0 || result2.Count() > 0)
                    {
                        _validations.rg = true;
                        _inconsistencies.Add("Favor, escolha outro RG.");
                    }
                }
            }

            if (!String.IsNullOrEmpty(email))
            {
                var query = @"select colaboradores.email from colaboradores where colaboradores.email = @email";
                var query2 = @"select professores.email from professores where professores.email = @email";
                var query3 = @"select fornecedores.email from fornecedores where fornecedores.email = @email";
                await using (var connection = new SqlConnection(
                        _config.GetConnectionString("InvictusConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<string>(query, new { email = email });
                    var result2 = await connection.QueryAsync<string>(query2, new { email = email });
                    var result3 = await connection.QueryAsync<string>(query3, new { email = email });
                    if (result.Count() > 0 || result2.Count() > 0 || result3.Count() > 0)
                    {
                        _validations.email = true;
                        _inconsistencies.Add("Favor, escolha outro e-mail.");
                    }
                }
            }

            return _inconsistencies;
        }

        public async Task<IEnumerable<string>> ValidaUnidade(UnidadeDto newUnidade)
        {
            var query = @"SELECT Unidades.Id, Unidades.Sigla, unidades.isUnidadeGlobal 
                        FROM Unidades WHERE Unidades.sigla = @sigla 
                        OR Unidades.isUnidadeGlobal = 'True'";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<UnidadeDto>(query, new { sigla = newUnidade.sigla });

                //connection.Close();

                if (result.Count() > 0)
                {
                    var oldUnidade = await _unidadeQueries.GetUnidadeById(newUnidade.id);

                    if (oldUnidade != null)
                    {
                        if (result.Where(r => r.sigla == newUnidade.sigla & r.id != newUnidade.id).Any()) _inconsistencies.Add("Já existe outra unidade cadastrada com esta sigla.");

                        if (newUnidade.isUnidadeGlobal == true)
                            if (result.Where(r => r.isUnidadeGlobal == true & r.id != newUnidade.id).Any()) _inconsistencies.Add("Já existe uma unidade configurada como global.");
                    }
                    else
                    {
                        if (result.Where(r => r.sigla == newUnidade.sigla).Any()) _inconsistencies.Add("Já existe unidade cadastrada com esta sigla.");

                        if (newUnidade.isUnidadeGlobal == true)
                            if (result.Where(r => r.isUnidadeGlobal == true).Any()) _inconsistencies.Add("Já existe uma unidade configurada como global.");
                    }

                    
                }

                return _inconsistencies;

            }
        }

        public async Task<IEnumerable<string>> ValidaUnidadeEdit(UnidadeDto newUnidade)
        {
            var queryUnidade = "SELECT * FROM Unidades WHERE Unidades.sigla = @sigalUnidade";

            var queryUnidadeGlobal = "SELECT * FROM Unidades WHERE Unidades.isUnidadeGlobal = 'True' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<UnidadeDto>(queryUnidade, new { sigalUnidade = newUnidade.sigla });

                connection.Close();

                if (result.Count() > 0) _inconsistencies.Add("Já existe unidade cadastrada com esta sigla.");

                if (newUnidade.isUnidadeGlobal == true)
                {
                    var unidadeGlobal = await connection.QueryAsync<UnidadeDto>(queryUnidadeGlobal);

                    if (unidadeGlobal.Count() > 0) _inconsistencies.Add("Já existe uma unidade configurada como global.");
                }

                return _inconsistencies;

            }
        }
    }
}
