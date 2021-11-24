using Invictus.Data.Context;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate.Interfaces;
using System;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class UnidadeRepository : IUnidadeRepository
    {
        private readonly InvictusDbContext _db;
        public UnidadeRepository(InvictusDbContext db)
        {
            _db = db;
        }
        public async Task AddUnidade(Unidade unidade)
        {
            await _db.Unidades.AddAsync(unidade);
            //await _db.SaveChangesAsync();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task EditUnidade(Unidade editedUnidade)
        {
            await _db.SingleUpdateAsync(editedUnidade);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveSala(Sala newSala)
        {
            await _db.Salas.AddAsync(newSala);
        }

        public async Task EditSala(Sala editedSala)
        {
            await _db.Salas.SingleUpdateAsync(editedSala);
        }
    }
}
