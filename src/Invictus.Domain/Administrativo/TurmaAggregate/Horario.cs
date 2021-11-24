using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class Horario : Entity
    {
        public Horario(DiaDaSemana diaSemanada,
                        string horarioInicio,
                        string horarioFim
                        )
        {
            DiaSemanada = diaSemanada.DisplayName;
            HorarioInicio = horarioInicio;
            HorarioFim = horarioFim;

        }
        public string DiaSemanada { get; private set; }
        public string HorarioInicio { get; private set; }
        public string HorarioFim { get; private set; }

        #region

        public Horario() { }
        public Guid TurmaId { get; set; }
        public virtual Turma Turma { get; private set; }


        #endregion
    }
}
