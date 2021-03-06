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
using Invictus.Core.Enumerations;

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

        private async Task<IEnumerable<ViewMatriculadosDto>> GetAlunosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param, Guid unidadeId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT Pessoas.nome, Pessoas.email, Pessoas.cpf, Pessoas.ativo, Matriculas.id as matriculaId, Matriculas.NumeroMatricula, unidades.descricao ");
            query.Append("FROM Matriculas INNER JOIN Pessoas on Matriculas.AlunoId = Pessoas.Id INNER JOIN unidades on Pessoas.UnidadeId = unidades.Id WHERE Pessoas.tipoPessoa = 'Aluno' AND ");
            if (param.todasUnidades == false) query.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") query.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") query.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") query.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { query.Append(" Pessoas.Ativo = 'True' "); } else { query.Append(" (Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False') "); }
            query.Append(" ORDER BY Pessoas.Nome ");
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
            queryCount.Append("SELECT COUNT(*) ");
            queryCount.Append("FROM Matriculas INNER JOIN Pessoas ON Matriculas.AlunoId = Pessoas.Id INNER JOIN unidades ON Pessoas.UnidadeId = unidades.Id WHERE Pessoas.tipoPessoa = 'Aluno' AND ");
            if (param.todasUnidades == false) queryCount.Append(" Pessoas.UnidadeId = '" + unidadeId + "' AND ");
            if (param.nome != "") queryCount.Append(" LOWER(Pessoas.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.email != "") queryCount.Append(" LOWER(Pessoas.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.cpf != "") queryCount.Append(" LOWER(Pessoas.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            if (param.ativo == false) { queryCount.Append(" Pessoas.Ativo = 'True' "); } else { queryCount.Append(" Pessoas.Ativo = 'True' OR Pessoas.Ativo = 'False' "); }


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

            var query = @"SELECT * FROM Boletos WHERE Boletos.PessoaId = @matriculaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var debitos = await connection.QueryAsync<BoletoDto>(query, new { matriculaId = matriculaId });

                return debitos.OrderBy(c => c.vencimento);

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

        public async Task<IEnumerable<BoletoDto>> GetContasReceber(string meioPagamentoId, DateTime start, DateTime end, bool ativo)
        {
            var inicio = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            var fim = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT
                        Boletos.id, 
                        Pessoas.nome,
                        Pessoas.TipoPessoa,
                        Boletos.vencimento,
                        Boletos.historico,
                        Boletos.valor,
                        Boletos.statusBoleto,
                        Boletos.PessoaId,
                        Boletos.ativo
                        FROM Boletos 
                        LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.Id  
                        WHERE (Boletos.statusBoleto = 'Vencido' OR Boletos.statusBoleto = 'Em aberto' OR Boletos.statusBoleto = 'Cancelado')  
                        AND Vencimento >= @inicio AND Vencimento <= @fim  ");
            if (ativo == false) query.Append(" AND Boletos.ativo = 'True' ");
            query.Append(@" AND Tipo = 'Crédito' 
                            AND Boletos.CentroCustoUnidadeId = @unidadeId  ");

            if (meioPagamentoId != "null") query.Append(@"AND MeioPagamentoId = '" + meioPagamentoId + "'");

            query.Append(@" ORDER BY Boletos.Vencimento");

            //
            var matriculadoQuery = @"SELECT 
                            Pessoas.Nome 
                            FROM Pessoas
                            INNER JOIN Matriculas on Pessoas.Id = Matriculas.AlunoId
                            WHERE Matriculas.Id = @id ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                // itemsPerPage currentPage  param   unidadeId

                var boletos = await connection.QueryAsync<BoletoDto>(query.ToString(), new { inicio = inicio, fim = fim, unidadeId = unidadeId });

                foreach (var boleto in boletos)
                {
                    if (boleto.nome == null)
                    {
                        boleto.nome = await connection.QuerySingleAsync<string>(matriculadoQuery, new { id = boleto.pessoaId });
                        boleto.tipoPessoa = TipoPessoa.Matriculado.DisplayName;
                    }
                }


                connection.Close();

                return boletos;
            }
        }

        public async Task<IEnumerable<BoletoDto>> GetContasPagar(string meioPagamentoId, DateTime start, DateTime end, bool ativo)
        {
            var inicio = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            var fim = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();


            //var inicio = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            //var fim = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
            //var meioPgmId = Guid.NewGuid();
            //var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT
                        Boletos.id, 
                        Boletos.vencimento,    
                        Boletos.valor,
                        Boletos.statusBoleto,
                        Boletos.PessoaId,
                        Boletos.EhFornecedor,
                        Boletos.Historico,
                        Pessoas.nome,
                        Pessoas.TipoPessoa,
                        Boletos.historico,
                        Boletos.ativo
                        FROM Boletos 
                        LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.Id  
                        WHERE Vencimento >= @inicio AND Vencimento <= @fim  ");
            if (ativo == false) query.Append(" AND Boletos.ativo = 'True' ");
            query.Append(@" AND Tipo = 'Débito' 
                            AND Boletos.CentroCustoUnidadeId = @unidadeId  ");

            if (meioPagamentoId != "null") query.Append(@" AND MeioPagamentoId = '" + meioPagamentoId + "' ");

            query.Append(@" ORDER BY Boletos.Vencimento");

            //var query = @"SELECT 
            //            Boletos.Id,
            //            Boletos.Vencimento,
            //            Boletos.Valor,
            //            Boletos.StatusBoleto,
            //            Boletos.PessoaId,

            //            Bancos.Nome as banco
            //            FROM Boletos 
            //            LEFT JOIN Bancos ON Boletos.BancoId = Bancos.Id
            //            WHERE Vencimento >= @inicio 
            //            AND Vencimento <= @fim 
            //            AND Tipo = 'Débito' 
            //            AND Boletos.CentroCustoUnidadeId = @unidadeId  ";

            //if (meioPagamentoId != "null") query = query + " AND MeioPagamentoId = '" + meioPgmId + "'";


            //query = query + " ORDER BY Boletos.Vencimento";

            var fornecedorQuery = @"SELECT Fornecedores.RazaoSocial as nome FROM Fornecedores WHERE Fornecedores.id = @id";

            var colaboradorQuery = @"SELECT Colaboradores.nome FROM Colaboradores WHERE Colaboradores.id = @id";

            var professorQuery = @"SELECT Professores.nome FROM Professores WHERE Professores.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                // itemsPerPage currentPage  param   unidadeId

                var boletos = await connection.QueryAsync<BoletoDto>(query.ToString(), new { inicio = inicio, fim = fim, unidadeId = unidadeId });

                foreach (var boleto in boletos)
                {
                    if (boleto.ehFornecedor)
                    {
                        boleto.nome = await connection.QuerySingleAsync<string>(fornecedorQuery, new { id = boleto.pessoaId });
                    }
                    else
                    {

                        var nomes = await connection.QueryAsync<string>(colaboradorQuery, new { id = boleto.pessoaId });

                        if (nomes.Any())
                        {
                            boleto.nome = nomes.FirstOrDefault();
                        }
                        else
                        {
                            boleto.nome = await connection.QuerySingleAsync<string>(professorQuery, new { id = boleto.pessoaId });
                        }
                    }
                }


                connection.Close();

                return boletos;
            }
        }

        public async Task<BoletoDto> GetContaReceber(Guid id)
        {
            //StringBuilder query = new StringBuilder();
            //var inicio = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            //var fim = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
            //var meioPgmId = Guid.NewGuid();
            //var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            //if (meioPagamentoId == "null")
            //{

            //}
            //else
            //{
            //    meioPgmId = new Guid(meioPagamentoId);
            //}
            var query = @"SELECT * FROM Boletos WHERE Boletos.id = @id ";

            //if (meioPagamentoId != "null") query = query + " AND MeioPagamentoId = '" + meioPgmId + "'";


            //query = query + " ORDER BY Boletos.Vencimento";

            //var fornecedorQuery = @"SELECT Fornecedores.RazaoSocial as nome FROM Fornecedores WHERE Fornecedores.id = @id";

            //var alunoQuery = @"SELECT 
            //                Alunos.Nome 
            //                FROM Alunos
            //                INNER JOIN Matriculas on Alunos.Id = Matriculas.AlunoId
            //                WHERE Matriculas.Id = @id ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                // itemsPerPage currentPage  param   unidadeId

                var boleto = await connection.QuerySingleAsync<BoletoDto>(query, new { id = id });

                //foreach (var boleto in boletos)
                //{
                //    if (boleto.ehFornecedor)
                //    {
                //        boleto.nome = await connection.QuerySingleAsync<string>(fornecedorQuery, new { id = boleto.pessoaId });
                //    }
                //    else
                //    {
                //        boleto.nome = await connection.QuerySingleAsync<string>(alunoQuery, new { id = boleto.pessoaId });
                //    }
                //}


                connection.Close();

                return boleto;
            }
        }

        public async Task<IEnumerable<BoletoDto>> GetAllBoletosVencidos()
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            //var boletos = await _db.Boletos.Where(b => b.Vencimento < hoje &
            //                                b.StatusBoleto == "Em aberto").ToListAsync();

            var query = @"SELECT * FROM Boletos WHERE Boletos.ativo = 'True' 
                        AND Boletos.statusBoleto = 'Em aberto' 
                        AND Boletos.Vencimento < @hoje";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var boleto = await connection.QueryAsync<BoletoDto>(query, new { hoje = hoje });

                connection.Close();

                return boleto;
            }
        }

        public async Task<IEnumerable<CaixaViewModel>> GetCaixa(bool cartao, DateTime start, DateTime end)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            start = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            end = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);

            StringBuilder query = new StringBuilder();

            query.Append(@"SELECT 
                        Boletos.id,
                        (ISNULL(Pessoas.Nome, Matriculas.nome)) as nome,
                        Boletos.Vencimento,
                        Boletos.DataPagamento,
                        Boletos.historico,
                        Boletos.ValorPago,
                        Boletos.statusBoleto,
                        boletos.PessoaId,
                        boletos.tipo,
                        Boletos.DigitosCartao,
                        FormasRecebimento.descricao,
                        FormasRecebimento.Taxa,
                        FormasRecebimento.DiasParaCompensacao
                        FROM Boletos
                        INNER JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
                        LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.Id
                        LEFT JOIN Matriculas ON Boletos.pessoaId = Matriculas.id");

            if (cartao)
            {
                query.Append(@" WHERE (Boletos.StatusBoleto = 'Confirmado' OR Boletos.StatusBoleto = 'Pago' OR Boletos.StatusBoleto = 'Estornado' ) 
                                AND Boletos.FormaRecebimentoId IN (
                                SELECT FormasRecebimento.id FROM FormasRecebimento 
                                WHERE FormasRecebimento.EhCartao = @cartao
                                AND FormasRecebimento.UnidadeId = @unidadeId)
                                AND Boletos.DataPagamento >= @start AND Boletos.DataPagamento <= @end ");
            }
            else
            {
                query.Append(@" WHERE Boletos.StatusBoleto = 'Confirmado' 
                                AND Boletos.FormaRecebimentoId IN (
                                SELECT FormasRecebimento.id FROM FormasRecebimento 
                                WHERE FormasRecebimento.EhCartao = @cartao
                                AND FormasRecebimento.UnidadeId = @unidadeId)
                                AND Boletos.DataPagamento >= @start AND Boletos.DataPagamento <= @end ");
            }


            //var query = @"SELECT 
            //            Boletos.id,
            //            (ISNULL(Pessoas.Nome, Matriculas.nome)) as nome,
            //            Boletos.Vencimento,
            //            Boletos.DataPagamento,
            //            Boletos.historico,
            //            Boletos.ValorPago,
            //            boletos.PessoaId,
            //            boletos.tipo,
            //            Boletos.DigitosCartao,
            //            FormasRecebimento.descricao,
            //            FormasRecebimento.Taxa,
            //            FormasRecebimento.DiasParaCompensacao
            //            FROM Boletos
            //            INNER JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
            //            LEFT JOIN Pessoas ON Boletos.PessoaId = Pessoas.Id
            //            LEFT JOIN Matriculas ON Boletos.pessoaId = Matriculas.id
            //            WHERE Boletos.StatusBoleto = 'Confirmado'
            //            AND Boletos.FormaRecebimentoId IN 
            //            (
            //            SELECT FormasRecebimento.id FROM FormasRecebimento
            //            WHERE FormasRecebimento.EhCartao = @cartao
            //            AND FormasRecebimento.UnidadeId = @unidadeId
            //            )
            //            AND Boletos.DataPagamento >= @start AND Boletos.DataPagamento <= @end ";

            var queryFuncionario = @"SELECT Pessoas.nome FROM Pessoas INNER JOIN LogBoletos ON Pessoas.id = LogBoletos.UserId 
                                    AND LogBoletos.BoletoId = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var boletos = await connection.QueryAsync<CaixaViewModel>(query.ToString(), new { unidadeId = unidadeId, cartao = cartao.ToString(), start = start, end = end });

                if (cartao)
                {
                    foreach (var item in boletos)
                    {
                        item.dataCompensacao = item.dataPagamento.AddDays(item.diasParaCompensacao);
                    }
                }

                foreach (var item in boletos)
                {
                    var nome = await connection.QueryAsync<string>(queryFuncionario, new { id = item.id });
                    item.usuario = nome.LastOrDefault();
                }

                connection.Close();

                return boletos;
            }
        }
    }
}
