using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class SalaDto
    {        
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string comentarios { get; set; }
        public int capacidade { get; set; }
        public bool ativo { get; set; }
        public int unidadeId { get; set; }
    }
}
