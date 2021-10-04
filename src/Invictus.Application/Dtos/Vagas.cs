using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class Vagas
    {
        public string diaDaSemanaPesquisa { get; set; }
        public string diaDaSemanaPesquisaView { get; set; }
        public string turno { get; set; }
        public string turnoView { get; set; }
        public DateTime dia { get; set; }
        public DayOfWeek diaDaSemana { get; set; }
        public string diaDaSemanaView { get; set; }

    }
}
