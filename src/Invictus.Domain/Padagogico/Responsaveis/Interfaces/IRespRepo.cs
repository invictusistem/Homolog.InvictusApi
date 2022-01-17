using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Pedagogico.Responsaveis.Interfaces
{
    public interface IRespRepo : IDisposable
    {
        Task Save(Responsavel responsavel);
        Task Edit(Responsavel responsavel);
        void Commit();
    }
}
