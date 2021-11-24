using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class PlanoPagamentoDto
    {
        public Guid id { get; set; }
        
        public string descricao { get; set; }
        public decimal valor { get; set; }
        public decimal taxaMatricula { get; set; }
        // public string Parcelamento { get; private set; }
        public bool materialGratuito { get; set; }
        public decimal valorMaterial { get; set; }
        public decimal bonusPontualidade { get; set; }
        // public Guid UnidadeId { get; private set; }
        
        public bool ativo { get; set; }
        public Guid typePacoteId { get; set; } //Criar SERÁ O TIPO
        public Guid contratoId { get; set; }
    }
}
