using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class TurmaDiarioClasseViewModel // select id, diaAula, horaInicial, horaFinal
    {
        public Guid id { get; set; }
        public string identificador { get; set; }
        public string descricao { get; set; }
        public int totalAlunos { get; set; }
        public int minimoAlunos { get; set; }
        public string statusAndamento { get; set; }
        public string previsaoInfo { get; set; }
        public DateTime previsaoAtual { get; set; }
        public DateTime previsaoTerminoAtual { get; set; }
        public int vagas { get; set; }
        // aula do DIA
        public Guid calendarioId { get; set; }
        public bool podeIniciarAula { get; set; }
        // próxima aula
        public DateTime diaAula { get; set; }
        public string horaInicial { get; set; }
        public string horaFinal { get; set; }
        public DateTime proximaAula { get; set; }
        public DateTime proximaAulaFinal { get; set; }
        public string descAula { get; set; }
        public string professor { get; set; }
    }
}
