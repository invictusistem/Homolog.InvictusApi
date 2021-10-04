using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Administrativo
{
    public class TurmaCalendarioViewModel
    {
        public int id {get;set;}
        public DateTime diaaula { get; set; }
        public string diadasemana { get; set; }
        public string horainicial { get; set; }
        public string horafinal { get; set; }
        public bool aulainiciada { get; set; }
        public bool aulaconcluida { get; set; }
        public string observacoes { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string Nome { get; set; }
    }
}
