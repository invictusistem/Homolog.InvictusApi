using System;

namespace Invictus.Dtos.PedagDto
{
    public class AlunoDocumentoDto
    {
        public Guid id { get; set; }
        public Guid matriculaId { get; set; }
        public string descricao { get; set; }
        public string comentario { get; set; }
        public string nome { get; set; }
        public bool docEnviado { get; set; }
        public bool analisado { get; set; }
        public int tamanho { get; set; }
        public bool validado { get; set; }
        public string tipoArquivo { get; set; }
        public string contentArquivo { get; set; }
        public byte[] dataFile { get; set; }
        public DateTime? dataCriacao { get; set; }
        public int prazoValidade { get; set; }
        public Guid turmaId { get; set; }

    }
}
