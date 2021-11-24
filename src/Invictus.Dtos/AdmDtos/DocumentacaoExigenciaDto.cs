using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class DocumentacaoExigidaDto
    {
        public Guid id { get; set; }
        //public Guid documentoId { get; set; }
        public string descricao { get; set; }
        public string comentario { get; set; }
        public string titular { get; set; }
        public int validadeDias { get; set; }
        public bool obrigatorioParaMatricula { get; set; }
    }
}
