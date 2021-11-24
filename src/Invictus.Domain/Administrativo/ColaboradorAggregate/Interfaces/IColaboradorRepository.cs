using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.ColaboradorAggregate.Interfaces
{
    public interface IColaboradorRepository : IDisposable
    {
        Task AddColaborador(Colaborador newColaborador);
        Task EditColaborador(Colaborador newColaborador);

        void Commit();
    }
}
