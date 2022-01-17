using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class BolsaDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public int percentualDesconto { get; set; }
        public string senha { get; set; }
        public Guid colaborador { get; set; }
        public Guid unidadeId { get; set; }
        public Guid typePacoteId { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataExpiracao { get; set; }
    }
}
