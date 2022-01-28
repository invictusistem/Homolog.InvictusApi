using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class SalaDto
    {
        public Guid id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string comentarios { get; set; }
        public int capacidade { get; set; }
        public bool ativo { get; set; }
        public DateTime dataCriacao { get; set; }
        public Guid unidadeId { get; set; }
    }
}
