using Invictus.Data.Context;
using Invictus.Domain.Financeiro;
using Invictus.Domain.Financeiro.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task EditBoleto(Boleto conta)
        {
            await _db.Boletos.SingleUpdateAsync(conta);
        }

        public async Task SaveBoleto(Boleto boleto)
        {
            await _db.Boletos.AddAsync(boleto);
        }

        public async Task SaveBoletos(IEnumerable<Boleto> boleto)
        {
            await _db.Boletos.AddRangeAsync(boleto);
        }

        

       
    }
}
