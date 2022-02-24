using Invictus.Data.Context;
using Invictus.Domain.Financeiro.Bolsas;
using Invictus.Domain.Financeiro.Bolsas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Financeiro
{
    public class BolsaRepo : IBolsaRepo
    {
        private readonly InvictusDbContext _db;
        public BolsaRepo(InvictusDbContext db)
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

        public async Task EditBolsa(Bolsa bolsa)
        {
            await _db.Bolsas.SingleUpdateAsync(bolsa);
        }

        public async Task SaveBolsa(Bolsa bolsa)
        {
            await _db.Bolsas.AddAsync(bolsa);
        }

       
    }
}
