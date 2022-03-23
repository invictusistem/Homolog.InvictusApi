using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos.Utils
{
    public class ParametrosDTO
    {
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public bool ativo { get; set; }
        public bool todasUnidades { get; set; }
        public bool primeiraReq { get; set; }
    }
}
