namespace Invictus.Domain.Pedagogico.TurmaAggregate
{
    public class LivroMatriculaAlunos
    {
        public LivroMatriculaAlunos() { }
        public LivroMatriculaAlunos(int id,
                           int alunoId,
                           string status,
                           int lvroMatId)
        {
            Id = id;
            AlunoId = alunoId;
            Status = status;
            LivroMatriculaId = lvroMatId;
        }

        public int Id { get; private set; }
        public int AlunoId { get; private set; }
        public string Status { get; private set; }
        public int LivroMatriculaId { get; private set; }
        //public virtual Turma Turma { get; private set; }
        public virtual LivroMatricula LivroMatricula { get; private set; }
    }
}
