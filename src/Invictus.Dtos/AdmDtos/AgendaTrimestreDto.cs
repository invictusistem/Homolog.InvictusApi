using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class AgendaTrimestreDto
    {
        public Guid id { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }
        public Guid unidadeId { get; set; }
    }
}
