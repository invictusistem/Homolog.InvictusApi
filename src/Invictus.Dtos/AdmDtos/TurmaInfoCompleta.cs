using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class TurmaInfoCompleta
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
        public PrevisoesDto previsoes { get; set; }
    }

    public class PrevisoesDto
    {
        public DateTime previsionStartOne { get; set; }
        public DateTime previsionStartTwo { get; set; }
        public DateTime previsionStartThree { get; set; }
        public DateTime previsionEndingOne { get; set; }
        public DateTime previsionEndingTwo { get; set; }
        public DateTime previsionEndingThree { get; set; }

    }
}
