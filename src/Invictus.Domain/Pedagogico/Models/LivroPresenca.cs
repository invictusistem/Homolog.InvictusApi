using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Pedagogico.Models
{
    public class LivroPresenca
    {
        public LivroPresenca() { }
        public LivroPresenca(long id,
                            int calendarioId,
                            bool isPresent,
                            int alunoId,
                            string isPresentToString
)
        {
            Id = id;
            CalendarioId = calendarioId;
            IsPresent = isPresent;
            AlunoId = alunoId;
            IsPresentToString = isPresentToString;

        }
        public long Id { get; private set; }
        public int CalendarioId { get; private set; }
        public bool IsPresent { get; private set; }
        public string IsPresentToString { get; private set; }
        public int AlunoId { get; private set; }
        //public int TurmaId { get; private set; }
        

    }
}
