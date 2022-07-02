using System;
using System.Threading.Tasks;

namespace Invictus.Domain.Padagogico.Requerimento.interfaces
{
    public interface ICategoriaRepo : IDisposable
    {
        Task AddCategoria(Categoria categoria);
        Task AddTipo(Tipo tipo);
        Task EditCategoria(Categoria categoria);
        Task EditTipo(Tipo tipo);

        void Commit();
    }
}
