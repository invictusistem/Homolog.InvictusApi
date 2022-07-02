using Invictus.Data.Context;
using Invictus.Domain.Padagogico.Requerimento;
using Invictus.Domain.Padagogico.Requerimento.interfaces;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Pedagogico
{
    public class CategoriaRepo : ICategoriaRepo
    {
        private readonly InvictusDbContext _db;
        public CategoriaRepo(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task AddCategoria(Categoria categoria)
        {
            await _db.Categorias.AddAsync(categoria);
        }

        public async Task AddTipo(Tipo tipo)
        {
            await _db.Tipos.AddAsync(tipo);
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditCategoria(Categoria categoria)
        {
            await _db.Categorias.SingleUpdateAsync(categoria);
        }

        public async Task EditTipo(Tipo tipo)
        {
            await _db.Tipos.SingleUpdateAsync(tipo);
        }
    }
}
