using Invictus.Data.Context;
using Invictus.Domain.Administrativo.RequerimentoAggregate;
using Invictus.Domain.Administrativo.RequerimentoAggregate.Interfaces;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class RequerimentoRepo : IRequerimentoRepo
    {
        private readonly InvictusDbContext _db;
        public RequerimentoRepo(InvictusDbContext db)
        {
            _db = db;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditTypeRequerimento(TipoRequerimento tipo)
        {
            await _db.TypeRequerimentos.SingleUpdateAsync(tipo);
        }

        public async Task SaveRequerimento(Requerimento requerimento)
        {
            await _db.Requerimentos.AddAsync(requerimento);
        }

        public async Task SaveTypeRequerimento(TipoRequerimento tipo)
        {
            await _db.TypeRequerimentos.AddAsync(tipo);
        }
    }
}
