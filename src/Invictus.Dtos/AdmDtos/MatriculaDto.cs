using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class MatriculaDto
    {
        public Guid alunoId { get; set; }
        public string numeroMatricula { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string status { get; set; }
        public Guid turmaId { get; set; }
        public Guid colaboradorResponsavelMatricula { get; set; }
        public DateTime diaMatricula { get; set; }
        public Guid bolsaId { get; set; }
        public string ciencia { get; set; }
        public Guid cienciaAlunodId { get; set; }
        public bool matriculaConfirmada { get; set; }
    }
}
