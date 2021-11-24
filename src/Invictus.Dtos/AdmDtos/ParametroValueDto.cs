using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class ParametroValueDto
    {
        public Guid id { get; set; }
        public string value { get; set; }
        public string descricao { get; set; }
        public string comentario { get; set; }
        public Guid parametrosKeyId { get; set; }

    }
}
