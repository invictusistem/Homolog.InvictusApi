using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.Parametros.Interfaces;
using System;
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

        public async Task EditParamValue(ParametrosValue parametro)
        {
            await _db.ParametrosValues.SingleUpdateAsync(parametro);
        }

        public async Task RemoveParamValue(Guid paramId)
        {
            var paramValue = await _db.ParametrosValues.FindAsync(paramId);

            await _db.ParametrosValues.SingleDeleteAsync(paramValue);
        }
    }
}
