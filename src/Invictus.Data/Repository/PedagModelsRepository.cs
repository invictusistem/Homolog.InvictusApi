using Invictus.Data.Context;
using Invictus.Domain.Pedagogico.EstagioAggregate;
using Invictus.Domain.Pedagogico.Models;
using Invictus.Domain.Pedagogico.Models.IPedagModelRepository;

namespace Invictus.Data.Repository
{
    public class PedagModelsRepository : IPedagModelsRepository
    {
        private readonly InvictusDbContext _db;
        public PedagModelsRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public void AddFilesEstagio(Documento documento, int alunoId)
        {
            _db.DocumentosEStagio.Add(documento);
        }

        public void CreateEstagio(Estagio estagio)
        {
            _db.Estagios.Add(estagio);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
