using System;

namespace Invictus.Dtos.AdmDtos
{
    public class MateriaTemplateDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string modalidade { get; set; }
        public int cargaHoraria { get; set; }
        public Guid typePacoteId { get; set; }
        public string typePacoteNome { get; set; }
        public bool ativo { get; set; }

    }
}
