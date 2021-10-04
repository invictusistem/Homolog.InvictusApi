using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class TurnosViewModel
    {
        public TurnosViewModel()
        {
            diasSemanaDisponiveis = new List<Tuple<string, string>>();
        }
        public string turno { get; set; }
        public Tuple<string, string> primeiroDiaSemana { get; set; }
        public List<Tuple<string, string>> diasSemanaDisponiveis { get; set; }
    }
}
