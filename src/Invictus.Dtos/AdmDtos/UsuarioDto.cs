using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class UsuarioDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string unidadeSigla { get; set; }
        public string roleName { get; set; }
        public bool ativo { get; set; }
        public List<Claims> claims { get; set; }// = new List<Claims>();
    }

    public class Claims
    {
        public int Clamid { get; set; }
        public string clamType { get; set; }
        public string clamKey { get; set; }
    }

    public class CreateUsuarioView
    {

        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string cargo { get; set; }
        public bool isProfessor { get; set; }

    }
}
