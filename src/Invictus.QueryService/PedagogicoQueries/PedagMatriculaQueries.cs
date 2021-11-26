using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
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
        public PedagMatriculaQueries(IConfiguration config)
        {
            _config = config;
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

        public async Task<ResponsavelDto> GetRespFinanceiroByMatriculaId(Guid matriculaId)
        {
            var query = @"select 
        responsaveis.id, 
        responsaveis.tipo, 
        responsaveis.nome ,
        responsaveis.cpf ,
        responsaveis.rg ,
        responsaveis.nascimento ,
        responsaveis.naturalidade ,
        responsaveis.naturalidadeUF ,
        responsaveis.email ,
        responsaveis.temRespFin ,
        responsaveis.telCelular ,
        responsaveis.telResidencial ,
        responsaveis.telWhatsapp ,
        responsaveis.bairro,
        responsaveis.cep ,
        responsaveis.complemento ,
        responsaveis.logradouro,
        responsaveis.numero,
        responsaveis.cidade ,
        responsaveis.uf 
FROM Responsaveis 
left join matriculas on Responsaveis.matriculaId = matriculas.id
where matriculas.id = @matriculaId AND
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
            var query = @"select 
        responsaveis.id, 
        responsaveis.tipo, 
        responsaveis.nome ,
        responsaveis.cpf ,
        responsaveis.rg ,
        responsaveis.nascimento ,
        responsaveis.naturalidade ,
        responsaveis.naturalidadeUF ,
        responsaveis.email ,
        responsaveis.temRespFin ,
        responsaveis.telCelular ,
        responsaveis.telResidencial ,
        responsaveis.telWhatsapp ,
        responsaveis.bairro,
        responsaveis.cep ,
        responsaveis.complemento ,
        responsaveis.logradouro,
        responsaveis.numero,
        responsaveis.cidade ,
        responsaveis.uf 
FROM Responsaveis 
left join matriculas on Responsaveis.matriculaId = matriculas.id
where matriculas.id = @matriculaId AND
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
            var query = @"select 
        responsaveis.id, 
        responsaveis.tipo, 
        responsaveis.nome ,
        responsaveis.cpf ,
        responsaveis.rg ,
        responsaveis.nascimento ,
        responsaveis.naturalidade ,
        responsaveis.naturalidadeUF ,
        responsaveis.email ,
        responsaveis.temRespFin ,
        responsaveis.telCelular ,
        responsaveis.telResidencial ,
        responsaveis.telWhatsapp ,
        responsaveis.bairro,
        responsaveis.cep ,
        responsaveis.complemento ,
        responsaveis.logradouro,
        responsaveis.numero,
        responsaveis.cidade ,
        responsaveis.uf 
FROM Responsaveis 
left join matriculas on Responsaveis.matriculaId = matriculas.id
where matriculas.id = @matriculaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<ResponsavelDto>(query, new { matriculaId = matriculaId });

                // connection.Close();

                return result.FirstOrDefault();

            }
        }
    }
}
