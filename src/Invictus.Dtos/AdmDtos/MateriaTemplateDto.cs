using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class MateriaTemplateDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string modalidade { get; set; }
        public int cargaHoraria { get; set; }
        public Guid typePacoteId { get; set; }
        public bool ativo { get; set; }

    }
}
