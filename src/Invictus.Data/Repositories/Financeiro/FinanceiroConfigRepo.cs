using Invictus.Data.Context;
using Invictus.Domain.Financeiro.Configuracoes;
using Invictus.Domain.Financeiro.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Financeiro
{
    public class FinanceiroConfigRepo : IFinanceiroConfigRepo
    {
        private readonly InvictusDbContext _db;
        public FinanceiroConfigRepo(InvictusDbContext db)
        {
            _db = db;
        }
        public async Task AddBanco(Banco banco)
        {
            await _db.Bancos.AddAsync(banco);
        }

        public async Task AddCentroCusto(CentroCusto banco)
        {
            await _db.CentroCustos.AddAsync(banco);
        }

        public async Task AddMeioPagamento(MeioPagamento banco)
        {
            await _db.MeiosPagamento.AddAsync(banco);
        }

        public async Task AddFormaRecebimento(FormaRecebimento banco)
        {
            await _db.FormasRecebimento.AddAsync(banco);
        }

        public async Task AddPlanoConta(PlanoConta banco)
        {
            await _db.PlanosConta.AddAsync(banco);
        }

        public async Task AddSubConta(SubConta banco)
        {
            await _db.SubContas.AddAsync(banco);
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.DisposeAsync();
        }
    }
}
