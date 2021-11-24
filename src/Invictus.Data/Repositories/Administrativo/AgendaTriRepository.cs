using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AgendaTri.Interfaces;
using Invictus.Domain.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class AgendaTriRepository : IAgendaTriRepository
    {
        private readonly InvictusDbContext _db;
        public AgendaTriRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task AddAgendaTrimestre(AgendaTrimestre agenda)
        {
            await _db.AgendasTrimestres.AddAsync(agenda);
        }

        public async Task EditAgendaTrimestre(AgendaTrimestre agenda)
        {
            await _db.AgendasTrimestres.SingleUpdateAsync(agenda);
        }
        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
       
    }
}
