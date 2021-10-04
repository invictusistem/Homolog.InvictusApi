using System;


namespace Invictus.Application.Dtos.Administrativo
{
    public class ContratoDto
    {
        public long id { get; set; }
        public string codigoContrato { get; set; }
        public string titulo { get; set; }
        public int pacoteId { get; set; }
        public bool podeEditar { get; set; }
        public DateTime dataCriacao { get; set; }
        public bool ativo { get; set; }
        public string conteudo { get; set; }
        public string observacao { get; set; }

        public string pacoteNome { get; set; }
    }

    public class ContratoConteudoDto
    {
        public long id { get; set; }
        public string order { get; set; }
        public string content { get; set; }
    }
}
