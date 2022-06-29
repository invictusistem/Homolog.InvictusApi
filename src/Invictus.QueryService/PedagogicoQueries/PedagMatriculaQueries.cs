using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries
{
    public class PedagMatriculaQueries : IPedagMatriculaQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidadeQueries;
        public PedagMatriculaQueries(IConfiguration config, IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries)
        {
            _config = config;
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
        }
        public async Task<PessoaDto> GetAlunoByMatriculaId(Guid matriculaId)
        {
            var query = @"SELECT 
        Pessoas.id, 
        Pessoas.nome ,
        Pessoas.nomeSocial ,
        Pessoas.cpf ,
        Pessoas.rg ,
        Pessoas.pai ,
        Pessoas.mae ,
        Pessoas.nascimento ,
        Pessoas.naturalidade ,
        Pessoas.naturalidadeUF ,
        Pessoas.email ,
        Pessoas.telefoneContato ,
        Pessoas.nomeContato ,
        Pessoas.celular,
        Pessoas.telResidencial ,
        Pessoas.telWhatsapp ,
        Pessoas.dataCadastro,
        Pessoas.tipoPessoa, 
        Pessoas.pessoaRespCadastroId,
        Pessoas.ativo ,
        Pessoas.unidadeId,
        Enderecos.Id,
        Enderecos.bairro,
        Enderecos.cep ,
        Enderecos.complemento ,
        Enderecos.logradouro,
        Enderecos.numero,
        Enderecos.cidade ,
        Enderecos.uf,
        Enderecos.PessoaId
        FROM Pessoas 
        LEFT JOIN Enderecos ON Pessoas.Id = enderecos.PessoaId 
        LEFT JOIN matriculas ON Pessoas.id = matriculas.alunoid
        WHERE matriculas.id = @matriculaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<PessoaDto, EnderecoDto, PessoaDto>(query,
                    map: (pessoa, endereco) =>
                    {
                        pessoa.endereco = endereco;
                        return pessoa;
                    },
                    new { matriculaId = matriculaId },
                    splitOn: "Id");

                //connection.Close();

                return result.FirstOrDefault();

            }
        }

        public async Task<IEnumerable<PessoaDto>> GetAlunosIndicacao()
        {
            var query = @"SELECT 
                        Pessoas.id,
                        Pessoas.nome
                        FROM Pessoas 
                        INNER JOIN Matriculas ON Pessoas.id = Matriculas.AlunoId 
                        WHERE Pessoas.tipoPessoa = 'Aluno' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<PessoaDto>(query);

                // connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<AnotacaoDto>> GetAnotacoesMatricula(Guid matriculaId)
        {
            var query = @"select 
                        colaboradores.nome as titulo,
                        alunosanotacoes.comentario,
                        alunosanotacoes.dataregistro
                        from alunosanotacoes 
                        inner join colaboradores on alunosanotacoes.userId = colaboradores.id 
                        where alunosanotacoes.matriculaId = @matriculaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<AnotacaoDto>(query, new { matriculaId = matriculaId });

                // connection.Close();

                return result.OrderBy(a => a.dataRegistro);

            }
        }

        public async Task<MatriculaDto> GetMatriculaById(Guid matriculaId)
        {
            var query = @"SELECT * FROM Matriculas WHERE Matriculas.id = @matriculaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<MatriculaDto>(query, new { matriculaId = matriculaId });

                // connection.Close();

                return result.FirstOrDefault();

            }
        }

        public async Task<MatriculaViewModel> GetMatriculaByNumeroMatricula(string numeroMatricula)
        {
            var query = @"SELECT 
                        Matriculas.Id as matriculaId,
                        Matriculas.NumeroMatricula,
                        Alunos.Nome as alunoNome,
                        Alunos.Email,
                        Alunos.CPF,
                        Turmas.Id as turmaId,
                        Turmas.Descricao as turma,
                        Turmas.UnidadeId,
                        Unidades.Descricao as unidade
                        FROM Matriculas
                        INNER JOIN Alunos ON Matriculas.AlunoId = Alunos.Id
                        INNER JOIN Turmas ON Matriculas.TurmaId = Turmas.Id
                        INNER JOIN Unidades ON Turmas.UnidadeId = Unidades.Id
                        WHERE Matriculas.NumeroMatricula = @numeroMatricula ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<MatriculaViewModel>(query, new { numeroMatricula  = numeroMatricula });

                return result.FirstOrDefault();

            }
        }

        public async Task<IEnumerable<MatriculaViewModel>> GetMatriculadosFromUnidade()
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();           

            var query = @"SELECT 
                        Matriculas.Id as matriculaId,
                        Pessoas.Nome as alunoNome
                        FROM Matriculas
                        INNER JOIN Pessoas on Matriculas.alunoId = Pessoas.Id
                        WHERE Pessoas.UnidadeId = @unidadeId
                        ORDER BY Pessoas.Nome ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var matriculas = await connection.QueryAsync<MatriculaViewModel>(query, new { unidadeId = unidadeId });

                connection.Close();

                return matriculas;
            }
        }

        public async Task<IEnumerable<MatriculaViewModel>> GetRelatorioMatriculas(string param)
        {
            var parametro = JsonConvert.DeserializeObject<MatriculaRelatorioParam>(param);
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);
            IEnumerable<MatriculaViewModel> matriculas = new List<MatriculaViewModel>();

            var matriculaDateRangeQuery = @"SELECT 
                                        Matriculas.Id as matriculaId,
                                        Matriculas.Nome as alunoNome,
                                        Matriculas.DiaMatricula,
                                        Turmas.Descricao,
                                        Turmas.Identificador,
                                        Colaboradores.Nome as colaboradorNome
                                        FROM Matriculas
                                        INNER JOIN Turmas on Matriculas.TurmaId = Turmas.Id
                                        INNER JOIN Colaboradores on Matriculas.ColaboradorResponsavelMatricula = Colaboradores.Id
                                        WHERE Matriculas.DiaMatricula > @rangeIni AND Matriculas.DiaMatricula < @rangeFinal AND
                                        Turmas.UnidadeId = @unidadeId ";

            var matriculaTurmaQuery = @"SELECT 
                                        Matriculas.Id as matriculaId,
                                        Matriculas.Nome as alunoNome,
                                        Matriculas.DiaMatricula,
                                        Turmas.Descricao,
                                        Turmas.Identificador,
                                        Colaboradores.Nome as colaboradorNome
                                        FROM Matriculas
                                        INNER JOIN Turmas on Matriculas.TurmaId = Turmas.Id
                                        INNER JOIN Colaboradores on Matriculas.ColaboradorResponsavelMatricula = Colaboradores.Id
                                        WHERE Matriculas.TurmaId = @turmaId AND
                                        Turmas.UnidadeId = @unidadeId  ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                if (parametro.opcao == "periodo")
                {
                    matriculas = await connection.QueryAsync<MatriculaViewModel>(matriculaDateRangeQuery, new { rangeIni = parametro.inicio, rangeFinal = parametro.fim, unidadeId = unidade.id });

                }
                else
                {
                    matriculas = await connection.QueryAsync<MatriculaViewModel>(matriculaTurmaQuery, new { turmaId = parametro.turmaId, unidadeId = unidade.id });
                }

                connection.Close();

                return matriculas.OrderBy(m => m.diaMatricula);

            }
        }

        public async Task<ResponsavelDto> GetRespFinanceiroByMatriculaId(Guid matriculaId)
        {
            var query = @"SELECT * FROM Responsaveis where Responsaveis.MatriculaId = @matriculaId AND
                        Responsaveis.tipo = 'Responsável financeiro'";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ResponsavelDto>(query, new { matriculaId = matriculaId });

                // connection.Close();

                return result.FirstOrDefault();

            }
        }

        public async Task<ResponsavelDto> GetRespMenorByMatriculaId(Guid matriculaId)
        {
            var query = @"SELECT * FROM Responsaveis where Responsaveis.MatriculaId = @matriculaId AND
                        Responsaveis.tipo = 'Responsável menor'";
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ResponsavelDto>(query, new { matriculaId = matriculaId });

                //connection.Close();

                return result.FirstOrDefault();

            }
        }

        public async Task<ResponsavelDto> GetResponsavel(Guid matriculaId)
        {
            var query = @"SELECT * FROM Responsaveis WHERE Responsaveis.MatriculaId = @matriculaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ResponsavelDto>(query, new { matriculaId = matriculaId });

                // connection.Close();

                return result.FirstOrDefault();

            }
        }

        public async Task<ResponsavelDto> GetResponsavelById(Guid id)
        {
            var query = @"select * FROM Responsaveis WHERE Responsaveis.Id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ResponsavelDto>(query, new { id = id });

                return result.FirstOrDefault();

            }
        }
    }
}
