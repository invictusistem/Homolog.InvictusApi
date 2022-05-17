﻿using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.Financeiro.Configuracoes;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries
{
    public class FinConfigQueries : IFinConfigQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        public FinConfigQueries(IConfiguration config, IAspNetUser aspNetUser)
        {
            _config = config;
            _aspNetUser = aspNetUser;
        }


        public async Task<IEnumerable<BancoDto>> GetAllBancos()
        {
            var unidadeIdDoUsuario = _aspNetUser.GetUnidadeIdDoUsuario();
            var query = @"SELECT * FROM Bancos WHERE Bancos.UnidadeId = @unidadeId ORDER BY Bancos.EhCaixaEscola DESC";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<BancoDto>(query, new { unidadeId = unidadeIdDoUsuario });

                return bancos;

            }
        }

        public async Task<IEnumerable<CentroCustoDto>> GetAllCentroCusto()
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var query = @"SELECT * FROM CentroCustos WHERE CentroCustos.UnidadeId = @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var centroCusto = await connection.QueryAsync<CentroCustoDto>(query, new { unidadeId = unidadeId });

                return centroCusto;

            }
        }

        public async Task<IEnumerable<FormaRecebimentoDto>> GetAllFormasRecebimentos()
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var query = @"SELECT * FROM FormasRecebimento WHERE FormasRecebimento.UnidadeId = @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<FormaRecebimentoDto>(query, new { unidadeId = unidadeId });

                return bancos;

            }
        }

        public async Task<IEnumerable<MeioPagamentoDto>> GetAllMeiosPagamento()
        {
            var query = @"SELECT * FROM MeioPagamentos";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var meiosPgm = await connection.QueryAsync<MeioPagamentoDto>(query);

                return meiosPgm;

            }
        }

        public async Task<IEnumerable<PlanoContaDto>> GetAllPlanos()
        {
            var query = @"SELECT * FROM PlanoContas";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<PlanoContaDto>(query);

                return bancos;

            }
        }

        public async Task<IEnumerable<SubContaDto>> GetAllSubContas()
        {
            var query = @"SELECT * FROM SubContas";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var bancos = await connection.QueryAsync<SubContaDto>(query);

                return bancos;

            }
        }

        public async Task<BancoDto> GetBancoById(Guid id)
        {
            var query = @"SELECT * FROM Bancos WHERE Bancos.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var banco = await connection.QuerySingleAsync<BancoDto>(query, new { id = id });

                return banco;

            }
        }

        public async Task<CentroCustoDto> GetCentroCustoById(Guid id)
        {
            var query = @"SELECT * FROM CentroCustos WHERE CentroCustos.Id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var centroCusto = await connection.QuerySingleAsync<CentroCustoDto>(query, new { id = id });

                return centroCusto;

            }
        }

        public async Task<FormaRecebimentoDto> GetFormaRecebimentoById(Guid id)
        { 
            var query = @"SELECT * FROM FormasRecebimento WHERE FormasRecebimento.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var forma = await connection.QuerySingleAsync<FormaRecebimentoDto>(query, new { id = id });

                return forma;

            }
        }

        public async Task<IEnumerable<FornecedorDto>> GetFornecedoresForCreateFormaRecebimento()
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var query = @"SELECT Fornecedores.id, Fornecedores.RazaoSocial FROM Fornecedores WHERE Fornecedores.id = @unidadeId AND Fornecedores.Ativo = 'True'";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var fornecedores = await connection.QueryAsync<FornecedorDto>(query, new { unidadeId = unidadeId });

                return fornecedores;

            }
        }

        public async Task<MeioPagamentoDto> GetMeiosPagamentoById(Guid id)
        {
            var query = @"SELECT * FROM MeioPagamentos WHERE MeioPagamentos.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var meioPgm = await connection.QuerySingleAsync<MeioPagamentoDto>(query, new { id = id });

                return meioPgm;

            }
        }

        public async Task<PlanoContaDto> GetPlanosById(Guid id)
        {
            var query = @"SELECT * FROM PlanoContas WHERE PlanoContas.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var plano = await connection.QuerySingleAsync<PlanoContaDto>(query, new { id = id });

                return plano;

            }
        }

        public async Task<SubContaDto> GetSubContasById(Guid id)
        {
            var query = @"SELECT * FROM SubContas WHERE Subcontas.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var subConta = await connection.QuerySingleAsync<SubContaDto>(query, new { id = id });

                return subConta;

            }

        }

        public async Task<IEnumerable<SubContaDto>> GetSubContasByPlanoId(Guid id)
        {
            var query = @"SELECT * FROM Subcontas WHERE SubContas.PlanoContaId = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var subContas = await connection.QueryAsync<SubContaDto>(query, new { id = id });

                return subContas;

            }
        }
    }
}
