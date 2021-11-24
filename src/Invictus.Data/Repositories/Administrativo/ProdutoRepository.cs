using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AdmProduto.Interfaces;
using Invictus.Domain.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly InvictusDbContext _db;
        public ProdutoRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task AddProduto(Produto newProduto)
        {
            await _db.Produtos.AddAsync(newProduto);
            //await _db.SaveChangesAsync();
        }

        public async Task UpdateProduto(Produto editedProduto)
        {
            await _db.Produtos.SingleUpdateAsync(editedProduto);

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
