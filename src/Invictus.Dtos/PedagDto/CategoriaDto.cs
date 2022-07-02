using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class CategoriaDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; }
        public IEnumerable<TipoDto> tipos { get; set; }
    }

    public class TipoDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
