using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Administrativo
{
    public class TurmaCreateViewModel
    {
        public List<ModuloDto> modulos { get; set; }

        public List<SalaDto> salas { get; set; }
        //public List<PacoteDto> pacotes { get; set; }
        public List<PlanoPagamentoDto> planos { get; set; }
    }
}
