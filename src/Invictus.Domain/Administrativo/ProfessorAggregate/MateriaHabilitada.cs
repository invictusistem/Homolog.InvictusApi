using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.ProfessorAggregate
{
    public class MateriaHabilitada : Entity
    {
        public MateriaHabilitada() { }

        public MateriaHabilitada(Guid pacoteMateriaId, Guid professorId)
        {
            PacoteMateriaId = pacoteMateriaId;
            ProfessorId = professorId;
        }
        //public int Id { get; private set; }
        public Guid PacoteMateriaId { get; private set; } //Id da materia do PACOTE e nao do MateriaTemplate

        #region EF
        public Guid ProfessorId { get; private set; }
       // public virtual Professor Professor { get; private set; }
        #endregion
    }
}
