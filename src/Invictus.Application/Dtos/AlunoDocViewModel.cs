using System.Collections.Generic;

namespace Invictus.Application.Dtos
{
    public class AlunoDocViewModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string CPF { get; set; }
        public string unidadeCadastrada { get; set; }
        public List<DocumentDto> documentos { get; set; }
    }
}
