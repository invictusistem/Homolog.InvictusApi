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

        public async Task EditBanco(Banco banco)
        {
            await _db.Bancos.SingleUpdateAsync(banco);
        }

        public async Task EditCentroCusto(CentroCusto centroCusto)
        {
            await _db.CentroCustos.SingleUpdateAsync(centroCusto);
        }

        public async Task EditFormaRecebimento(FormaRecebimento formareceb)
        {
            await _db.FormasRecebimento.SingleUpdateAsync(formareceb);
        }

        public async Task EditMeioPagamento(MeioPagamento meioPgm)
        {
            await _db.MeiosPagamento.SingleUpdateAsync(meioPgm);
        }

        public async Task EditPlanoConta(PlanoConta plano)
        {
            await _db.PlanosConta.SingleUpdateAsync(plano);
        }

        public async Task EditSubConta(SubConta subconta)
        {
            await _db.SubContas.SingleUpdateAsync(subconta);
        }

        public async Task DeleteBanco(Guid bancoId)
        {
            var entidade = await _db.Bancos.FindAsync(bancoId);
            await _db.Bancos.SingleDeleteAsync(entidade);

        }

        public async Task DeleteCentroCusto(Guid centroCustoId)
        {
            var entidade = await _db.CentroCustos.FindAsync(centroCustoId);
            await _db.CentroCustos.SingleDeleteAsync(entidade);
        }

        public async Task DeleteFormaRecebimento(Guid formarecebId)
        {
            var entidade = await _db.FormasRecebimento.FindAsync(formarecebId);
            await _db.FormasRecebimento.SingleDeleteAsync(entidade);
        }

        public async Task DeleteMeioPagamento(Guid meioPgmId)
        {
            var entidade = await _db.MeiosPagamento.FindAsync(meioPgmId);
            await _db.MeiosPagamento.SingleDeleteAsync(entidade);
        }

        public async Task DeletePlanoConta(Guid planoId)
        {
            var entidade = await _db.PlanosConta.FindAsync(planoId);
            await _db.PlanosConta.SingleDeleteAsync(entidade);
        }

        public async Task DeleteSubConta(Guid subcontaId)
        {
            var entidade = await _db.SubContas.FindAsync(subcontaId);
            await _db.SubContas.SingleDeleteAsync(entidade);
        }
    }
}
