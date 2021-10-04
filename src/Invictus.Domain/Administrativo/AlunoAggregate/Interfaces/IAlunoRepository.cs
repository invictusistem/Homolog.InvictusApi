using System;

namespace Invictus.Domain.Administrativo.AlunoAggregate.Interfaces
{
    public interface IAlunoRepository : IDisposable
    {
        int AddAluno(Aluno aluno);        
    }
}