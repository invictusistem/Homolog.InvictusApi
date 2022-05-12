using System;

namespace Invictus.Dtos.PedagDto
{
    public class DocumentoEstagioDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public string nome { get; set; }
        public bool analisado { get; set; }
        public bool validado { get; set; }
        public string tipoArquivo { get; set; }
        public string contentArquivo { get; set; }
        public string nomeArquivo { get; set; }
        public byte[] dataFile { get; set; }
        public DateTime? dataCriacao { get; set; }
        public string observacao { get; set; }
        public string status { get; set; }
        public Guid matriculaEstagioId { get; set; }
        public Guid responsavelAnalise { get; set; }
    }
}
