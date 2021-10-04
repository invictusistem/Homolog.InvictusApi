using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Administrativo
{
    public class DocumentacaoExigidaDto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string comentario { get; set; }
        public string titular { get; set; }
        public int moduloId { get; set; }
    }
}
