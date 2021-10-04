using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.TurmaAggregate
{
    public class LivroMatricula
    {
        public LivroMatricula() { }
        public LivroMatricula(int id,
                           //int alunoId,
                           int turmaId)
        {
            Id = id;
           //AlunoId = alunoId;
            TurmaId = turmaId;
            Alunos = new List<LivroMatriculaAlunos>();
        }

        public int Id { get; private set; }
        //public int AlunoId { get; private set; }
        public List<LivroMatriculaAlunos> Alunos { get; private set; }
        public virtual int TurmaId { get; private set; }
        public virtual TurmaPedagogico Turma { get; private set; }
    }
}
