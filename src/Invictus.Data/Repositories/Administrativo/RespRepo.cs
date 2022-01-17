using Invictus.Data.Context;
using Invictus.Domain.Pedagogico.Responsaveis;
using Invictus.Domain.Pedagogico.Responsaveis.Interfaces;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class RespRepo : IRespRepo
    {
        private readonly InvictusDbContext _db;
        public RespRepo(InvictusDbContext db)
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

        public async Task Edit(Responsavel responsavel)
        {
            await _db.Responsaveis.SingleUpdateAsync(responsavel);
        }

        public async Task Save(Responsavel responsavel)
        {
            await _db.Responsaveis.AddAsync(responsavel);
        }
    }
}
