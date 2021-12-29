using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class DisponibilidadeDto
    {
        public Guid id { get; set; }
        public bool domingo { get; set; }
        public bool segunda { get; set; }
        public bool terca { get; set; }
        public bool quarta { get; set; }
        public bool quinta { get; set; }
        public bool sexta { get; set; }
        public bool sabado { get; set; }
        public Guid unidadeId { get; set; }
        public Guid pessoaId { get; set; }
        public DateTime dataAtualizacao { get; set; }
    }

    public class DisponibilidadeView 
    {   
        public Guid id { get; set; }
        public bool domingo { get; set; }
        public bool segunda { get; set; }
        public bool terca { get; set; }
        public bool quarta { get; set; }
        public bool quinta { get; set; }
        public bool sexta { get; set; }
        public bool sabado { get; set; }
        public string descricao { get; set; }
        public Guid unidadeId { get; set; }
        public Guid pessoaId { get; set; }
        public DateTime dataAtualizacao { get; set; }
    }


}
