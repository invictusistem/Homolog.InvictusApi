using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.AlunoAggregate.Interfaces;

namespace Invictus.Data
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly InvictusDbContext _db;
        public AlunoRepository(InvictusDbContext db)
        {
            _db = db;
        }
        public int AddAluno(Aluno aluno)
        {
            _db.Alunos.Add(aluno);
            _db.SaveChanges();

            return aluno.Id;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
