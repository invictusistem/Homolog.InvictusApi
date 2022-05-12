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
        public Presenca(
                            Guid calendarioId,
                            bool? isPresent,
                            Guid alunoId,
                            Guid matriculaId,
                            string isPresentToString
)
        {
            CalendarioId = calendarioId;
            IsPresent = isPresent;
            AlunoId = alunoId;
            MatriculaId = matriculaId;
            IsPresentToString = isPresentToString;
        }

        public Presenca(Guid id,
                            Guid calendarioId,
                            bool? isPresent,
                            Guid alunoId,
                            Guid matriculaId,
                            string isPresentToString
)
        {
            Id = id;
            CalendarioId = calendarioId;
            IsPresent = isPresent;
            AlunoId = alunoId;
            MatriculaId = matriculaId;
            IsPresentToString = isPresentToString;
        }

        public Guid CalendarioId { get; private set; }
        public bool? IsPresent { get; private set; }
        public string IsPresentToString { get; private set; }
        public Guid ReposicaoId { get; private set; }
        public Guid MatriculaId { get; private set; }
        public Guid AlunoId { get; private set; }


        public void SetPresenca(string isPresentToString)
        {
            if (isPresentToString.ToLower() == "f")
            {
                IsPresent = false;
            }
            else
            {
                IsPresent = true;
            }
        }


        #region EF
        protected Presenca() { }

        #endregion
    }
}
