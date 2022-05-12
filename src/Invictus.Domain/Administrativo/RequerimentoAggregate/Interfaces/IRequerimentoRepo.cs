using System;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.RequerimentoAggregate.Interfaces
{
    public interface IRequerimentoRepo : IDisposable
    {
        Task SaveRequerimento(Requerimento requerimento);
        Task SaveTypeRequerimento(TipoRequerimento tipo);
        Task EditTypeRequerimento(TipoRequerimento tipo);
        void Commit();
    }
}