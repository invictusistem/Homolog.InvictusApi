using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.AlunoAggregate.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class AlunoRepo : IAlunoRepo
    {
        private readonly InvictusDbContext _db;
        public AlunoRepo(InvictusDbContext db)
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

        public async Task Edit(Aluno newAluno)
        {
            await _db.Alunos.SingleUpdateAsync(newAluno);
        }

        public async Task EditAlunoDoc(AlunoDocumento doc)
        {
            await _db.AlunosDocs.SingleUpdateAsync(doc);
        }

        public async Task SaveAluno(Aluno newAluno)
        {
            await _db.Alunos.AddAsync(newAluno);
        }

        public async Task SaveAlunoDocs(IEnumerable<AlunoDocumento> docs)
        {
            await _db.AlunosDocs.AddRangeAsync(docs);
        }

        public async Task SaveAlunoPlano(AlunoPlanoPagamento newPlano)
        {
            await _db.AlunoPlanos.AddAsync(newPlano);
        }
    }
}
