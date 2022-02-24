using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class MateriaHabilitadaViewModel
    {
        public Guid id { get; set; }
        public Guid PacoteMateriaId { get; set; }
        public Guid ProfessorId { get; set; }
        public string nome { get; set; }
        public string nomePacote { get; set; }       

    }
}
