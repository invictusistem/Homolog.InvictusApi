using Invictus.Domain.Administrativo.Models;
using System;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.AgendaTri.Interfaces
{
    public interface IAgendaTriRepository : IDisposable
    {
        Task AddAgendaTrimestre(AgendaTrimestre agenda);
        Task EditAgendaTrimestre(AgendaTrimestre agenda);
        void Commit();
    }
}
