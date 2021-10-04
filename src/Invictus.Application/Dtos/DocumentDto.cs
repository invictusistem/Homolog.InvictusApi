using System;

namespace Invictus.Application.Dtos
{
    public class DocumentDto
    {

        public int docId { get; set; }
        public int alunoId { get; set; }
        public string descricao { get; set; }
        public string nome { get; set; }
        public bool analisado { get; set; }
        public bool validado { get; set; }
        public string tipoArquivo { get; set; }
        public string contentArquivo { get; set; }
        public byte[] dataFile { get; set; }
        public DateTime? dataCriacao { get; set; }
    }
}
