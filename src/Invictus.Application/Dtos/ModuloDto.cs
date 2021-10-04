using Invictus.Application.Dtos.Administrativo;
using System;
using System.Collections.Generic;

namespace Invictus.Application.Dtos
{
    public class ModuloDto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int duracaoMeses { get; set; }
        public int TotalHoras { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal preco { get; set; }
        public int unidadeId { get; set; }
        public int typePacoteId { get; set; }
        public List<MateriaDto> materias { get; set; }
        public List<DocumentacaoExigidaDto> documentos { get; set; }
    }
}
