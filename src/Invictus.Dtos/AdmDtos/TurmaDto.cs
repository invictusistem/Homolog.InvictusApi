using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class TurmaDto
    {
        public Guid id { get; set; }
        public string identificador { get; set; }
        public string descricao { get; set; }
        public int totalAlunos { get; set; }
        public int minimoAlunos { get; set; }
        public string statusAndamento { get; set; }
        public Guid unidadeId { get; set; }
        public Guid salaId { get; set; }
        public Guid pacoteId { get; set; }
        public Guid typePacoteId { get; set; }
        public DateTime previsaoAtual { get; set; }
        public DateTime previsaoTerminoAtual { get; set; }
        public string previsaoInfo { get; set; }
        public DateTime dataCriacao { get; set; }
    }
}
