using Invictus.Data.Context;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.PacoteAggregate.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class PacoteRepository : IPacoteRepository
    {
        private readonly InvictusDbContext _db;
        public PacoteRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task Save(Pacote newPacote)
        {
            await _db.Pacotes.AddAsync(newPacote);

        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Edit(Pacote pacote)
        {
            await _db.Pacotes.SingleUpdateAsync(pacote);
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}
