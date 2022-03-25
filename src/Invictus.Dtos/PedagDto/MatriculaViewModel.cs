using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class MatriculaViewModel
    {
        public Guid matriculaId { get; set; }
        public string alunoNome { get; set; }
        public DateTime diaMatricula { get; set; }
        public string descricao { get; set; }
        public string identificador { get; set; }
        public string colaboradorNome { get; set; }
    }
}
