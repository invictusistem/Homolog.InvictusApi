using System;


namespace Invictus.Domain.Administrativo.ProfessorAggregate
{
    public class MateriasHabilitadas
    {
        public MateriasHabilitadas() { }

        public MateriasHabilitadas(int materiaId)
        {
            MateriaId = materiaId;
        }
        public Guid Id { get; private set; }
        public int MateriaId { get; private set; }
        public Guid ProfessorId { get; private set; }
        public virtual Professor Professor { get; private set; }
    }
}
