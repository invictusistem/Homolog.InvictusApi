using Invictus.Domain.Administrativo.AlunoAggregate;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.TurmaAggregate.Interfaces
{
    public interface ITurmaPedagRepository : IDisposable
    {
        void CreateTurmaPedag(TurmaPedagogico turmaPedag);
        void CreateMateriasPedag(List<MateriaPedag> turmaPedag);
        Aluno AddAlunoTurma(int idAluno, int idTurma, string ciencia);
        void AddProfInTurma(List<int> profsIds, int turmaId);
    }
}
