using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.PlanoPagamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class PlanoPgmRepository : IPlanoPgmRepository
    {
        private readonly InvictusDbContext _db;
        public PlanoPgmRepository(InvictusDbContext db)
        {
            _db = db;
        }
        public async Task CreatePlano(PlanoPagamentoTemplate plano)
        {
            await _db.PlanosPgmTemplate.AddAsync(plano);
        }

        public async Task EditPlano(PlanoPagamentoTemplate plano)
        {
            await _db.PlanosPgmTemplate.SingleUpdateAsync(plano);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}
