using Invictus.Data.Context;
using Invictus.Domain.Pedagogico.Models;
using Invictus.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Data
{
    public class AgendaProvasRepository : IAgendaProvasRepository
    {
        private readonly InvictusDbContext _db;
        public AgendaProvasRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task CreateScheduleProof(int turmaId)
        {
            var turma = await _db.Turmas.Where(t => t.Id == turmaId).SingleOrDefaultAsync();
            var materias = await _db.Materias.Where(m => m.ModuloId == turma.ModuloId).ToListAsync();

            var agendas = new List<ProvasAgenda>();

            foreach (var mat in materias)
            {
                //Nullable<DateTime> data1 = null;
                //Nullable<DateTime> data2 = null;
                agendas.Add(new ProvasAgenda(0, turmaId, null, null, null, null, null, null, mat.Id, mat.Descricao));
            }

            _db.ProvasAgenda.AddRange(agendas);
             _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void EditAgenda(ProvasAgenda agenda)
        {
             _db.ProvasAgenda.Update(agenda);
            _db.SaveChanges();
        }
    }
}
