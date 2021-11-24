using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.Parametros.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class ParametroRepo : IParametroRepo
    {
        private readonly InvictusDbContext _db;
        public ParametroRepo(InvictusDbContext db)
        {
            _db = db;
        }
        public async Task AddParamValue(ParametrosValue parametro)
        {
            await _db.ParametrosValues.AddAsync(parametro);
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
