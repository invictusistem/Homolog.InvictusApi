using Invictus.Data.Context;
using Invictus.Domain.Financeiro;
using Invictus.Domain.Financeiro.Interfaces;
using System;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Financeiro
{
    public class InfoFinancRepo : IDebitosRepos
    {
        private readonly InvictusDbContext _db;
        public InfoFinancRepo(InvictusDbContext db)
        {
            _db = db;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
             _db.DisposeAsync();
        }

        public async Task SaveInfoFinanceira(InformacaoDebito infoDebito)
        {
            await _db.InformacoesDebito.AddAsync(infoDebito);
        }

       
    }
}
