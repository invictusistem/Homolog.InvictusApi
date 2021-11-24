using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class ParametrosKeyDto
    {
        public Guid id { get; set; }
        public string key { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; }
        public IEnumerable<ParametroValueDto> parametrosValue { get; set; } = new List<ParametroValueDto>();
    }    
}
