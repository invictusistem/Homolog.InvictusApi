using Invictus.Data.Context;
using Invictus.Domain.Administrativo.MatriculaRegistro;
using Invictus.Domain.Administrativo.RegistroMatricula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class MatriculaRepo : IMatriculaRepo
    {
        private readonly InvictusDbContext _db;
        public MatriculaRepo(InvictusDbContext db)
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

        public async Task Save(Matricula newMatricula)
        {
            await _db.Matriculas.AddAsync(newMatricula);
        }
    }
}
