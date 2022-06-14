using Invictus.Data.Context;
using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Invictus.Domain.Administrativo.FuncionarioAggregate.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class PessoaRepository : IPessoaRepo
    {
        private readonly InvictusDbContext _db;
        public PessoaRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task AddPessoa(Pessoa pessoa)
        {
            await _db.Pessoas.AddAsync(pessoa);
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditPessoa(Pessoa pessoa)
        {
            _db.Entry(pessoa.Endereco).State = EntityState.Modified;

            await _db.Pessoas.SingleUpdateAsync(pessoa);
        }
    }
}
