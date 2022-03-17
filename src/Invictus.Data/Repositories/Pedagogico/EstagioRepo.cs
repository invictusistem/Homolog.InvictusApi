using Invictus.Data.Context;
using Invictus.Domain.Padagogico.Estagio;
using Invictus.Domain.Padagogico.Estagio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Pedagogico
{
    public class EstagioRepo : IEstagioRepo
    {
        private readonly InvictusDbContext _db;
        public EstagioRepo(InvictusDbContext db)
        {
            _db = db;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public async Task CreateEstagio(Estagio estagio)
        {
            await _db.Estagios.AddAsync(estagio);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditEstagio(Estagio estagio)
        {
            await _db.Estagios.SingleUpdateAsync(estagio);
        }
    }
}
