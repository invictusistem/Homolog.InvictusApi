using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class DoacaoCommand
    {
        public Guid produtoId { get; set; }
        public Guid unidadeDonatariaId { get; set; }
        public int qntDoada { get; set; }
    }
}
