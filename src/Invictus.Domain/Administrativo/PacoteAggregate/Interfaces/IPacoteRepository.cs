using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.PacoteAggregate.Interfaces
{
    public interface IPacoteRepository : IDisposable
    {
        Task Save(Pacote pacote);
        Task Edit(Pacote pacote);
        void Commit();

    }
}
