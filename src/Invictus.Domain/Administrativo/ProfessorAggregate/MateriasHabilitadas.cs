using System;


namespace Invictus.Domain.Administrativo.ProfessorAggregate
{
    public class MateriasHabilitadas
    {
        public MateriasHabilitadas() { }

        public MateriasHabilitadas(int materiaId, int professorId)
        {
            MateriaId = materiaId;
            ProfessorId = professorId;
        }
        public int Id { get; private set; }
        public int MateriaId { get; private set; }
        public int ProfessorId { get; private set; }
       // public virtual Professor Professor { get; private set; }
    }
}
