using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class DocumentoAlunoDto
    {
        public int id { get; set; }
        public int alunoId { get; set; }
        public string descricao { get; set; }
        public string comentario { get; set; }
        public string nome { get; set; }
        public bool docEnviado { get; set; }
        public bool analisado { get; set; }
        public bool validado { get; set; }
        public string tipoArquivo { get; set; }
        public string contentArquivo { get; set; }
        public byte[] dataFile { get; set; }
        public DateTime? dataCriacao { get; set; }
        public int turmaId { get; set; }
    }
}
