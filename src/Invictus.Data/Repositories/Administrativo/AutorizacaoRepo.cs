using Invictus.Data.Context;
using Invictus.Domain.Administrativo.UnidadeAuth;
using Invictus.Domain.Administrativo.UnidadeAuth.Interfaces;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class AutorizacaoRepo : IAutorizacaoRepo
    {
        private readonly InvictusDbContext _db;
        public AutorizacaoRepo(InvictusDbContext db)
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

        public async Task SaveAutorizacao(Autorizacao aut)
        {
            await _db.Autorizacoes.AddAsync(aut);
        }
    }
}
