using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Comercial
{
    public class LeadDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime data { get; set; }
        public string telefone { get; set; }
        public string bairro { get; set; }
        public string cursoPretendido { get; set; }
        public Guid unidadeId { get; set; }
        public DateTime dataInclusaoSistema { get; set; }
        public Guid responsavelLead { get; set; }
        public bool efetivada { get; set; }
        public Guid? matriculaId { get; set; }

    }
}
