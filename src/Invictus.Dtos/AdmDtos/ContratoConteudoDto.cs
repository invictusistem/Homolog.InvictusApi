using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class ContratoConteudoDto
    {
        public Guid id { get; set; }
        public string order { get; set; }
        public string content { get; set; }
        public Guid contratoId { get; set; }
    }
}
