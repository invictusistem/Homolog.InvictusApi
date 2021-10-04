using Invictus.Data.Context;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Pedagogico.TurmaAggregate;
using Invictus.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly InvictusDbContext _db;
        protected DbSet<Turma> DbSet;
        public TurmaRepository(InvictusDbContext db)
        {
            _db = db;
            DbSet = _db.Set<Turma>();
        }

        public void AddTurma(Turma turma)
        {
            // _db.Turmas.Include(p => p.Previsoes);
            // _db.Entry(turma).State = EntityState.Added;
            _db.Turmas.Add(turma);
            //DbSet.Add(turma);
            _db.SaveChanges();
        }

        public void CreateProfessoresMaterias(IEnumerable<Professor> professoresMateria)
        {
            throw new NotImplementedException();
        }

        //public void UpdateProfessoresTurma(IEnumerable<ProfessoresMateria> professoresMateria)
        //{
        //    _db.ProfessoresMaterias.UpdateRange(professoresMateria);
        //    _db.SaveChanges();
        //}

        //public void CreateProfessoresMaterias(IEnumerable<ProfessoresMateria> professoresMateria)
        //{
        //    _db.ProfessoresMaterias.AddRange(professoresMateria);
        //    _db.SaveChanges();
        //}

        public void Dispose()
        {
            _db.Dispose();
        }

        public void UpdateProfessoresTurma(IEnumerable<Professor> professoresMateria)
        {
            throw new NotImplementedException();
        }
    }
}
