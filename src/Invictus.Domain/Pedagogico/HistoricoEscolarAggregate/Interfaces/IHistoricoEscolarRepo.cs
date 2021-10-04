using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Pedagogico.HistoricoEscolarAggregate.Interfaces
{
    public interface IHistoricoEscolarRepo : IDisposable
    {
        void CreateHistoricoEscolar(HistoricoEscolar historico);
    }
}
