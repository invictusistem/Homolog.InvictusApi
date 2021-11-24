using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class CreateUnidadeDto
    {
        public UnidadeDto unidade { get; set; }
        public ColaboradorDto colaborador { get; set; }
    }    
}
