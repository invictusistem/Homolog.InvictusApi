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
        Task AddProfsNaTurma(IEnumerable<TurmaProfessor> professores);
        Task UpdateMateriaDaTurma(TurmaMaterias turmaMateria);
        Task RemoverProfessorDaTurma(TurmaProfessor professor);
        void AtualizarTurmasMaterias(IEnumerable<TurmaMaterias> turmasMaterias);
        void Commit();
    }
}
