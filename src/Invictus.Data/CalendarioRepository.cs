using Invictus.Data.Context;
using Invictus.Domain;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Repository;
using System.Collections.Generic;

namespace Invictus.Data
{
    public class CalendarioRepository : ICalendarioRepository
    {
        private readonly InvictusDbContext _context;
        public CalendarioRepository(InvictusDbContext context)
        {
            _context = context;
        }

        public void SalvarCalendario(IEnumerable<Calendario> calendarios)
        {
            _context.AddRange(calendarios);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
