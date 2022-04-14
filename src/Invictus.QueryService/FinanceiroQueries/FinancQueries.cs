using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Invictus.Core.Extensions;
using Invictus.Core.Interfaces;

namespace Invictus.QueryService.FinanceiroQueries
{
    public class FinancQueries : IFinanceiroQueries
    {
        private readonly IConfiguration _config;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspNetUser;
        public FinancQueries(IConfiguration config, IUnidadeQueries unidadeQueries, IAspNetUser aspNetUser)
        {
            _config = config;
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
        }

        public async Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetAlunosFinanceiro(int itemsPerPage, int currentPage, string paramsJson)
        {
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
            query.Append("SELECT alunos.nome, alunos.email, alunos.cpf, alunos.ativo, Matriculas.id as matriculaId, Matriculas.NumeroMatricula, unidades.descricao ");
            query.Append("FROM Matriculas inner join alunos on Matriculas.AlunoId = Alunos.Id inner join unidades on alunos.UnidadeId = unidades.Id WHERE ");
            if (param.todasUnidades == false) query.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Alunos.Ativo = 'True' "); } else { query.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); }
            query.Append(" ORDER BY Alunos.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ViewMatriculadosDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                if (results.Count() > 0)
                {
                    foreach (var aluno in results)
                    {
                        aluno.cpf = aluno.cpf.BindingCPF();
                    }

                }
                connection.Close();

                return results;
            }
        }

        private async Task<int> CountAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("select Count(*) ");
            queryCount.Append("FROM Matriculas inner join alunos on Matriculas.AlunoId = Alunos.Id inner join unidades on alunos.UnidadeId = unidades.Id WHERE  ");
            if (param.todasUnidades == false) queryCount.Append(" Alunos.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Alunos.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Alunos.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Alunos.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Alunos.Ativo = 'True' "); } else { queryCount.Append(" Alunos.Ativo = 'True' OR Alunos.Ativo = 'False' "); }


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString());

                connection.Close();

                return countItems;

            }
        }

        public async Task<IEnumerable<BoletoDto>> GetDebitoAlunos(Guid matriculaId)
        {

            var query = @"select * from Boletos where Boletos.InformacaoDebitoId = (
                        select id from InformacoesDebitos where InformacoesDebitos.MatriculaId = @matriculaId 
                        )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var debitos = await connection.QueryAsync<BoletoDto>(query, new { matriculaId = matriculaId });

                return debitos;

            }
        }

        public async Task<PaginatedItemsViewModel<BoletoDto>> GetProdutosVendaByRangeDate(int itemsPerPage, int currentPage, string paramsJson)
        {

            var parametros = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);
            parametros.end = new DateTime(parametros.end.Year, parametros.end.Month, parametros.end.Day, 23, 59, 59);

            var unidade = await _unidadeQueries.GetUnidadeDoUsuario();

            var registros = await GetBoletosPaginatedByFilter(itemsPerPage, currentPage, parametros, unidade.id);

            var boletosCount = await GetBoletosCountByFilter(itemsPerPage, currentPage, parametros, unidade.id);

            var paginatedItems = new PaginatedItemsViewModel<BoletoDto>(currentPage, itemsPerPage, boletosCount, registros.ToList());

            return paginatedItems;

        }

        private async Task<IEnumerable<BoletoDto>> GetBoletosPaginatedByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {
            //StringBuilder query = new StringBuilder();
            var query = @"select 
                        Boletos.Vencimento,
                        Boletos.DataPagamento,
                        Boletos.Valor,
                        Boletos.ValorPago,
                        Boletos.Juros,
                        Boletos.JurosFixo,
                        Boletos.Multa,
                        Boletos.MultaFixo,
                        Boletos.Desconto,
                        Boletos.Tipo,
                        Boletos.DiasDesconto,
                        Boletos.StatusBoleto,
                        Boletos.Historico,
                        Boletos.SubConta,
                        Boletos.FormaPagamento,
                        Boletos.DigitosCartao,
                        Boletos.DataCadastro,
                        Boletos.ReparcelamentoId,
                        Boletos.CentroCustoUnidadeId,
                        Boletos.InformacaoDebitoId,
                        Boletos.ResponsavelCadastroId,
                        Boletos.Id_unico,
                        Boletos.Id_unico_original,
                        Boletos.Status,
                        Boletos.Msg,
                        Boletos.Nossonumero,
                        Boletos.LinkBoleto,
                        Boletos.LinkGrupo,
                        Boletos.LinhaDigitavel,
                        Boletos.Pedido_numero,
                        Boletos.Banco_numero,
                        Boletos.Token_facilitador,
                        Boletos.Credencial,
                        Colaboradores.Nome
                        from Boletos
                        left join Colaboradores on Boletos.ResponsavelCadastroId  = Colaboradores.Id                        
                        WHERE Boletos.CentroCustoUnidadeId = @unidadeId
                        AND boletos.DataCadastro >= @start
                        AND boletos.DataCadastro <= @end
                        ORDER BY boletos.DataPagamento 
                        OFFSET( @currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                // itemsPerPage currentPage  param   unidadeId

                var boletos = await connection.QueryAsync<BoletoDto>(query, new
                {
                    unidadeId = unidadeId,
                    start = param.start,
                    end = param.end,
                    currentPage = currentPage,
                    itemsPerPage = itemsPerPage
                });

                connection.Close();                

                return boletos;
            }
        }

        private async Task<int> GetBoletosCountByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {
            var query = @"select Count(*)
                        from Boletos
                        left join Colaboradores on Boletos.ResponsavelCadastroId  = Colaboradores.Id                        
                        WHERE Boletos.CentroCustoUnidadeId = @unidadeId
                        AND boletos.DataCadastro >= @start
                        AND boletos.DataCadastro <= @end ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var count = await connection.QuerySingleAsync<int>(query.ToString(), new
                {
                    unidadeId = unidadeId,
                    start = param.start,
                    end = param.end,
                    currentPage = currentPage,
                    itemsPerPage = itemsPerPage
                });

                connection.Close();

                return count;
            }
        }
    }
}
