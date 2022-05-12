using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class TipoRequerimentoDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public bool ativo { get; set; }
        public string observacao { get; set; }
    }
}
