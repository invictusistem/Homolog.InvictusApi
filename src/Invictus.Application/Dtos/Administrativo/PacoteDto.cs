using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Administrativo
{
    public class PacoteDto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int unidadeId { get; set; }
    }
}
