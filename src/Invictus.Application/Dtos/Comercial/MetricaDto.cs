
using System;

namespace Invictus.Application.Dtos.Comercial
{
    public class MetricaDto
    {
        public int ColaboradorId { get; set; }
        public string EmailColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public int TotalMatriculados { get; set; }
        public DateTime DiaLead { get; set; }
        public DateTime DiaMatricula { get; set; }
    }
}
