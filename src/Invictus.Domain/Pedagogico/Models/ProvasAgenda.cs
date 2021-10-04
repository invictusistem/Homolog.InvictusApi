using System;

namespace Invictus.Domain.Pedagogico.Models
{
    public class ProvasAgenda
    {
        public ProvasAgenda() { }
        public ProvasAgenda(int id,
                            int turmaId,
                            DateTime? avaliacaoUm,
                            DateTime? avaliacaoDois,
                            DateTime? avaliacaoTres,
                            DateTime? segundaChamadaAvaliacaoUm,
                            DateTime? segundaChamadaAvaliacaoDois,
                            DateTime? segundaChamadaAvaliacaoTres,
                            int materiaId,
                            string materia)
        {
            Id = id;
            TurmaId = turmaId;
            AvaliacaoUm = avaliacaoUm;
            AvaliacaoDois = avaliacaoDois;
            AvaliacaoTres = avaliacaoTres;
            SegundaChamadaAvaliacaoUm = segundaChamadaAvaliacaoUm;
            SegundaChamadaAvaliacaoDois = segundaChamadaAvaliacaoDois;
            SegundaChamadaAvaliacaoTres = segundaChamadaAvaliacaoTres;

            MateriaId = materiaId;
            Materia = materia;
        }

        public int Id { get; private set; }
        public DateTime? AvaliacaoUm { get; private set; }
        public DateTime? SegundaChamadaAvaliacaoUm { get; private set; }
        public DateTime? AvaliacaoDois { get; private set; }
        public DateTime? SegundaChamadaAvaliacaoDois { get; private set; }
        public DateTime? AvaliacaoTres { get; private set; }
        public DateTime? SegundaChamadaAvaliacaoTres { get; private set; }
        public int MateriaId { get; private set; }
        public string Materia { get; private set; }
        public int TurmaId { get; private set; }

        public void Factory(int turmaId, int materiaId, string materia)
        {
            TurmaId = turmaId;
            Materia = materia;
            MateriaId = materiaId;
        }

        public void UpdateAvOne(DateTime? avOne, DateTime? SegChAvOne)
        {
            AvaliacaoUm = avOne;
            SegundaChamadaAvaliacaoUm = SegChAvOne;
        }

        public void UpdateAvTwo(DateTime? avOne, DateTime? SegChAvOne)
        {
            AvaliacaoDois = avOne;
            SegundaChamadaAvaliacaoDois = SegChAvOne;
        }

        public void UpdateAvTree(DateTime? avOne, DateTime? SegChAvOne)
        {
            AvaliacaoTres = avOne;
            SegundaChamadaAvaliacaoTres = SegChAvOne;
        }
    }
}

