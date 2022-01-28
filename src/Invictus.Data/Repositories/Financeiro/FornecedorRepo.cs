using Invictus.Data.Context;
using Invictus.Domain.Financeiro.Fornecedores;
using Invictus.Domain.Financeiro.Fornecedores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Financeiro
{
    public class FornecedorRepo : IFornecedorRepo
    {
        private readonly InvictusDbContext _db;
        public FornecedorRepo(InvictusDbContext db)
        {
            _db = db;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.DisposeAsync();
        }

        public async Task Edit(Fornecedor fornecedor)
        {
            await _db.Fornecedors.SingleUpdateAsync(fornecedor);
        }

        public async Task SaveFornecedor(Fornecedor fornecedor)
        {
            await _db.Fornecedors.AddAsync(fornecedor);
        }
    }
}
