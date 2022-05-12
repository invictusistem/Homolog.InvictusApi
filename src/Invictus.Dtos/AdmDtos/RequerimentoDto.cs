using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class RequerimentoDto
    {
        public Guid matriculaRequerenteId { get; set; }
        public DateTime dataRequerimento { get; set; }
        public Guid descricao { get; set; }
        public string observacao { get; set; }
        public bool chamadoEncerrado { get; set; }
        public DateTime dataEncerramento { get; set; }
        public Guid responsaveEncerramentolId { get; set; }
        public Guid unidadeId { get; set; }
        public List<RespostaDto> respostas { get; set; }
    }

    public class RespostaDto
    {
        public DateTime dataResposta { get; set; }
        public string observacao { get; set; }
        public Guid responsavelId { get; set; }
        public string documento64 { get; set; }
        public Guid requerimentoId { get; set; }
    }
}
