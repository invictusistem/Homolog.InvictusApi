using Invictus.Domain.Pedagogico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Repository
{
    public interface IAgendaProvasRepository : IDisposable
    {
        Task CreateScheduleProof(int turmaId);
        void EditAgenda(ProvasAgenda agenda);
    }
}
