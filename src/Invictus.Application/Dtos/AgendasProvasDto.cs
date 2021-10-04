using System;

namespace Invictus.Application.Dtos
{
    public class AgendasProvasDto
    {
        public int id { get; set; }
        public DateTime? avaliacaoUm { get; set; }
        public DateTime? avaliacaoDois { get; set; }
        public DateTime? avaliacaoTres { get; set; }
        public DateTime? segundaChamadaAvaliacaoUm { get; set; }
        public DateTime? segundaChamadaAvaliacaoDois { get; set; }
        public DateTime? segundaChamadaAvaliacaoTres { get; set; }
        public int materiaId { get; set; }
        public string materia { get; set; }
        public int turmaId { get; set; }
    }
}
