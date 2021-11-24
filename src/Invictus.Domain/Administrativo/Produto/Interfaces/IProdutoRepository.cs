using Invictus.Domain.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.AdmProduto.Interfaces
{
    public interface IProdutoRepository : IDisposable
    {
        Task AddProduto(Produto newProduto);
        Task UpdateProduto(Produto editedProduto);
        void Commit();
    }
}
