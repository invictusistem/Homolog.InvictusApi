using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Repository
{
    public interface ICursoRepository : IDisposable
    {
       // Turma AddCurso(Turma curso);
       // void AddAlunoTurma(AlunosTurma aluno, int turmaId, string cienciaCurso);
        void DeleteCurso(int cursoId);
        //void AddProfsCurso(List<Tuple<int, int, int>> addProfsCommmands);
        void ExcluirProfessorTurma(int profId, int turmaId);
        void IniciarTurma(int turmaId);
        void AdiarInicio(int turmaId);
    }
}
