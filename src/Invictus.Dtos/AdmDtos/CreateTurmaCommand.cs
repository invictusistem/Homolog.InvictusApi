using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class CreateTurmaCommand
    {
        public Guid pacoteId { get; set; }
        public string descricao { get; set; }
        public Guid salaId { get; set; }
        public int minVagas { get; set; }
        public DateTime prevInicio_1 { get; set; }
        public DateTime prevTermino_1 { get; set; }
        public DateTime prevInicio_2 { get; set; }
        public DateTime prevTermino_2 { get; set; }
        public DateTime prevInicio_3 { get; set; }
        public DateTime prevTermino_3 { get; set; }
        public List<DiasSemanaCommand> diasSemana { get; set; }
    }

    public class DiasSemanaCommand
    {
        public string diaSemana {get;set; }
        public string horarioInicio { get;set; }
        public string horarioFim { get; set; }
    }
}
