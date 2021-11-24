using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly InvictusDbContext _db;
        public ColaboradorRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task AddColaborador(Colaborador newColaborador)
        {
            await _db.Colaboradores.AddAsync(newColaborador);
        }
        

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditColaborador(Colaborador newColaborador)
        {
            await _db.Colaboradores.SingleUpdateAsync(newColaborador);
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}
