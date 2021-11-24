using Invictus.Data.Context;
using Invictus.Domain.Padagogico.AlunoAggregate.Interfaces;
using Invictus.Domain.Pedagogico.AlunoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Pedagogico
{
    public class AlunoPedagRepo : IAlunoPedagRepo
    {
        private readonly InvictusDbContext _db;
        public AlunoPedagRepo(InvictusDbContext db)
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

        public async Task SaveAnotacao(AlunoAnotacao anotacao)
        {
            await _db.AlunosAnotacoes.AddAsync(anotacao);
        }
    }
}
