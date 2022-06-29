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
        public string numeroMatricula { get; set; }
        public string alunoNome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public DateTime diaMatricula { get; set; }
        public string descricao { get; set; }
        public string identificador { get; set; }
        public string colaboradorNome { get; set; }
        public string turma { get; set; }
        public string unidade { get; set; }
        public Guid unidadeId { get; set; }
        public Guid turmaId { get; set; }
    }
}
