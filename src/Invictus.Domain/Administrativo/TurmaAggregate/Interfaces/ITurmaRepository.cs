using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate.Interfaces
{
    public interface ITurmaRepository<T> where T : IAggregateRoot
    {
        //void AddAlunoTurma(AlunosTurma aluno, int turmaId, string cienciaCurso);
    }
}
