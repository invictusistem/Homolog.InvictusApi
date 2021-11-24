using Invictus.Data.Context;
using Invictus.Domain.Administrativo.MatTemplate.Interfaces;
using Invictus.Domain.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class MateriaRepository : IMateriaRepo
    {
        private readonly InvictusDbContext _db;
        public MateriaRepository(InvictusDbContext db)
        {
            _db = db;
        }
        


        public async Task Edit(MateriaTemplate materia)
        {
            await _db.SingleUpdateAsync(materia);
        }

        public async Task Save(MateriaTemplate materia)
        {
            await _db.MateriasTemplates.AddAsync(materia);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}
