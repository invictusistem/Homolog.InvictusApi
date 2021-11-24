using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class AnotacaoDto
    {
        public Guid id { get; set; }
        public string titulo { get; set; }
        public string comentario { get; set; }
        public DateTime dataRegistro { get; set; }
        public Guid userId { get; set; }
        public Guid matriculaId { get; set; }
    }
}
