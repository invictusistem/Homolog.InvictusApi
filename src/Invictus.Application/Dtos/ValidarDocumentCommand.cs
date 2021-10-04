using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class ValidarDocumentCommand
    {
        public int alunoId { get; set; }
        public int docId { get; set; }
        public bool validado { get; set; }
    }
}
