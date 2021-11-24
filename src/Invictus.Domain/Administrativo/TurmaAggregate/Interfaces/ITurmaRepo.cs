using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate.Interfaces
{
    public interface ITurmaRepo : IDisposable
    {
        Task Save(Turma turma);
        Task Edit(Turma turma);
        Task SavePrevisoes(Previsoes previ);
        Task IniciarTurma(Guid turmaId);
        Task AdiarInicio(Guid turmaId);
        void Commit();
    }
}
