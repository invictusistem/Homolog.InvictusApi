using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class ContratoDto
    {
        public Guid id { get; set; }
        public string codigoContrato { get; set; }
        public string titulo { get; set; }
        public Guid typepacoteId { get; set; }
        public bool podeEditar { get; set; }
        public DateTime dataCriacao { get; set; }
        public bool ativo { get; set; }
        public string conteudo { get; set; }
        public string observacao { get; set; }
        public List<ContratoConteudoDto> conteudos { get; set; } = new List<ContratoConteudoDto>();
        // public string pacoteNome { get; set; }
    }

    public class ContratoView
    {
        public Guid id { get; set; }
        public string codigoContrato { get; set; }
        public string titulo { get; set; }
        public Guid pacoteId { get; set; }
        public bool podeEditar { get; set; }
        public DateTime dataCriacao { get; set; }
        public bool ativo { get; set; }
        public List<ContratoConteudoDto> conteudos { get; set; } = new List<ContratoConteudoDto>();
        // public string conteudo { get; set; }
        public string observacao { get; set; }
        //public string pacoteNome { get; set; }
    }
}
