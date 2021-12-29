using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class PacoteDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        //public int duracaoMeses { get; set; }
        public int totalHoras { get; set; }
       // public decimal preco { get; set; }
        public bool ativo { get; set; }
        public DateTime dataCriacao { get; set; }
        public Guid typePacoteId { get; set; }
        public Guid unidadeId { get; set; }
        public List<PacoteMateriaDto> materias { get; set; }
        public List<DocumentacaoExigidaDto> DocumentosExigidos { get; set; }
    }


    public class PacoteDtoTeste
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        //public int duracaoMeses { get; set; }
        public int totalHoras { get; set; }
        // public decimal preco { get; set; }
        public bool ativo { get; set; }
        public DateTime dataCriacao { get; set; }
        public Guid typePacoteId { get; set; }
        public Guid unidadeId { get; set; }
        public List<PacoteMateriaDto> materias { get; set; }
        public List<DocumentacaoExigDto> DocumentosExigidos { get; set; }
    }

    public class DocumentacaoExigDto
    {
        public Guid id { get; set; }
        //public Guid documentoId { get; set; }
        public string descricao { get; set; }
        public string comentario { get; set; }
        public string titular { get; set; }
        public int validadeDias { get; set; }
        public bool obrigatorioParaMatricula { get; set; }
        public Guid pacoteId { get; set; }
    }



}
