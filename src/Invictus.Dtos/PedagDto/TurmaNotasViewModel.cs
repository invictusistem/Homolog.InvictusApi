using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class TurmaNotasViewModel
    {
        public string nome { get; set; }
        public Guid id { get; set; }
        public string avaliacaoUm { get; set; }
        public string segundaChamadaAvaliacaoUm { get; set; }
        public string avaliacaoDois { get; set; }
        public string segundaChamadaAvaliacaoDois { get; set; }
        public string avaliacaoTres { get; set; }
        public string segundaChamadaAvaliacaoTres { get; set; }
        public Guid materiaId { get; set; }
        public string materiaDescricao { get; set; }
        public string resultado { get; set; }
        public Guid matriculaId { get; set; }
        public Guid turmaId { get; set; }
        public int percentualPresenca { get; set; }
        public int qntFaltas { get; set; }
        public Guid typePacoteId { get; set; }
        public int ordem { get; set; }
    }

    public class TurmaNotasDto
    {
        public Guid id { get; set; }
        public string avaliacaoUm { get; set; }
        public string segundaChamadaAvaliacaoUm { get; set; }
        public string avaliacaoDois { get; set; }
        public string segundaChamadaAvaliacaoDois { get; set; }
        public string avaliacaoTres { get; set; }
        public string segundaChamadaAvaliacaoTres { get; set; }
        public Guid materiaId { get; set; }
        public string materiaDescricao { get; set; }
        public string resultado { get; set; }
        public Guid matriculaId { get; set; }
        public Guid turmaId { get; set; }
    }

    public class TurmaPresencaViewModel
    {
        public string IsPresentToString { get; set; }
        public Guid materiaId { get; set; }
    }
}
