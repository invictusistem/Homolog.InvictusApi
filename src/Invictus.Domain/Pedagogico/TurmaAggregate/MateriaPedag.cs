using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.TurmaAggregate
{
    public class MateriaPedag
    {
        public MateriaPedag() { }
        public MateriaPedag(int id,
                       string descricao,
                       int materiaId,
                       int turmaPedagId
                       //int professorId
            )
        {
            Id = id;
            Descricao = descricao;
            MateriaId = materiaId;
            TurmaPedagId = turmaPedagId;
            //ProfessorId = professorId;
            Professores = new List<Professor>();
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public int MateriaId { get; private set; }
        public List<Professor> Professores { get; private set; }
        public int TurmaPedagId { get; private set; }
        public virtual TurmaPedagogico TurmaPedag { get; private set; }
    }
}