using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class PacoteMateriaDto
    {
        public Guid id { get; set; }
        public Guid materiaId { get; set; }
       // public string descricao { get; set; }
        public string nome { get; set; }
        public int ordem { get; set; }
        public int cargaHoraria { get; set; }
        public string modalidade { get; set; }
        public Guid pacoteId { get; set; }

       
    }
}
