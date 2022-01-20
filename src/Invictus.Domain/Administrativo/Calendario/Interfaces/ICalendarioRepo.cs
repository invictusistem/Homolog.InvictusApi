using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Calendarios.Interfaces
{
    public interface ICalendarioRepo : IDisposable
    {
        Task SaveCalendarios(IEnumerable<Calendario> calendarios);
        void UpdateCalendarios(List<Calendario> calendarios);
        Task UpdateCalendario(Calendario calend);
        void Commit();
    }
}
