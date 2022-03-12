using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class Presenca : Entity
    {
        public Presenca() { }
        public Presenca(
                            Guid calendarioId,
                            bool? isPresent,
                            Guid alunoId,
                            string isPresentToString
)
        {
            CalendarioId = calendarioId;
            IsPresent = isPresent;
            AlunoId = alunoId;
            IsPresentToString = isPresentToString;
        }

        public Presenca(Guid id,
                            Guid calendarioId,
                            bool? isPresent,
                            Guid alunoId,
                            string isPresentToString
)
        {
            Id = id;
            CalendarioId = calendarioId;
            IsPresent = isPresent;
            AlunoId = alunoId;
            IsPresentToString = isPresentToString;
        }

        public Guid CalendarioId { get; private set; }
        public bool? IsPresent { get; private set; }
        public string IsPresentToString { get; private set; }
        public Guid AlunoId { get; private set; }


        public void SetPresenca(string isPresentToString)
        {
            if (isPresentToString.ToLower() == "f")
            {
                IsPresent = true;
            }
            else
            {
                IsPresent = false;
            }
        }
    }
}
