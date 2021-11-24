using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class DocumentacaoTemplateDto
    {   
        public Guid id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public int validadeDias { get; set; }

    }
}
