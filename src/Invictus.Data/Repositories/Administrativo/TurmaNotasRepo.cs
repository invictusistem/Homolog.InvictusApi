using Invictus.Data.Context;
using Invictus.Domain.Padagogico.NotasTurmas;
using Invictus.Domain.Padagogico.NotasTurmas.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class TurmaNotasRepo : ITurmaNotasRepo
    {
        private readonly InvictusDbContext _db;
        public TurmaNotasRepo(InvictusDbContext db)
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

        public async Task SaveList(IEnumerable<TurmaNotas> notas)
        {
            await _db.TurmasNotas.AddRangeAsync(notas);
        }

        public void UpdateNotas(IEnumerable<TurmaNotas> notas)
        {
            _db.TurmasNotas.UpdateRange(notas);
        }
    }
}
