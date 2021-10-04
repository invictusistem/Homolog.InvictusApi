using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Pedagogico.TurmaAggregate;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Invictus.Domain.Repository
{
    public interface ITurmaRepository : IDisposable
    {
        void AddTurma(Turma turma);
        void UpdateProfessoresTurma(IEnumerable<Professor> professoresMateria);
        void CreateProfessoresMaterias(IEnumerable<Professor> professoresMateria);
    }
}
