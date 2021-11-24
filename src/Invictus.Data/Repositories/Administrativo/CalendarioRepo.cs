using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.Calendarios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class CalendarioRepo : ICalendarioRepo
    {
        private readonly InvictusDbContext _db;
        public CalendarioRepo(InvictusDbContext db)
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

        public async  Task SaveCalendarios(IEnumerable<Calendario> calendarios)
        {
            await _db.Calendarios.AddRangeAsync(calendarios);
        }
    }
}
