using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class TurmaViewModel
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

        //public Guid UnidadeId { get; private set; }
        //public Guid SalaId { get; private set; }
        //public Guid PacoteId { get; private set; }
    }
}
