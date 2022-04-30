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
        public async Task<AlunoDto> GetAlunoByMatriculaId(Guid matriculaId)
        {
            var query = @"select 
        alunos.id, 
        alunos.nome ,
        alunos.nomeSocial ,
        alunos.cpf ,
        alunos.rg ,
        alunos.nomePai ,
        alunos.nomeMae ,
        alunos.nascimento ,
        alunos.naturalidade ,
        alunos.naturalidadeUF ,
        alunos.email ,
        alunos.telReferencia ,
        alunos.nomeContatoReferencia ,
        alunos.telCelular ,
        alunos.telResidencial ,
        alunos.telWhatsapp ,
        alunos.bairro,
        alunos.cep ,
        alunos.complemento ,
        alunos.logradouro,
        alunos.numero,
        alunos.cidade ,
        alunos.uf ,
        alunos.dataCadastro ,
        alunos.ativo ,
        alunos.unidadeId
        FROM Alunos 
        left join matriculas on alunos.id = matriculas.alunoid
        where matriculas.id = @matriculaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<AlunoDto>(query, new { matriculaId = matriculaId });

                //connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<AlunoDto>> GetAlunosIndicacao()
        {
            var query = @"select 
                        alunos.id,
                        alunos.nome
                        from Alunos 
                        inner join Matriculas on Alunos.id = Matriculas.AlunoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<AlunoDto>(query);

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

                if(parametro.opcao == "periodo") 
                {
                    matriculas = await connection.QueryAsync<MatriculaViewModel>(matriculaDateRangeQuery, new { rangeIni = parametro.inicio, rangeFinal = parametro.fim, unidadeId = unidade.id });
 
                } else
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
