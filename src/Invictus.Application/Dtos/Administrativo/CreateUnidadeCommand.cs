using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Administrativo
{
    public class CreateUnidadeCommand
    {
        public UnidadeDto unidade { get; set; }
        public ColaboradorDto colaborador { get; set; }
    }
}
