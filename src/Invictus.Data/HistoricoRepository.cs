using Invictus.Data.Context;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate.Interfaces;

namespace Invictus.Data
{
    public class HistoricoRepository : IHistoricoEscolarRepo
    {
        private readonly InvictusDbContext _db;

        public HistoricoRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public void CreateHistoricoEscolar(HistoricoEscolar historico)
        {
            _db.HistoricosEscolares.Add(historico);
           /// _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        
    }
}
