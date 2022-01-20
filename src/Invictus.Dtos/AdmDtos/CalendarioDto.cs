using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class CalendarioDto
    {
        public Guid id { get; set; }
        public DateTime diaAula { get; set; }
        public string diaDaSemana { get; set; }
        public Guid materiaId { get; set; }
        public string horaInicial { get; set; }
        public string horaFinal { get; set; }
        public Guid professorId { get; set; }
        public Guid turmaId { get; set; }
        public Guid unidadeId { get; set; }
        public bool aulaIniciada { get; set; }
        public bool temReposicao { get; set; }
        public DateTime dateAulaIniciada { get; set; }
        public bool aulaConcluida { get; set; }
        public DateTime dateAulaConcluida { get; set; }
        public string observacoes { get; set; }
        public Guid salaId { get; set; }
    }
}
