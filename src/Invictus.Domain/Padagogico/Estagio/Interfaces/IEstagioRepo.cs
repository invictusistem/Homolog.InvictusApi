using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Padagogico.Estagio.Interfaces
{
    public interface IEstagioRepo : IDisposable
    {
        Task CreateEstagio(Estagio estagio);
        Task EditEstagio(Estagio estagio);
        void Commit();
    }
}
