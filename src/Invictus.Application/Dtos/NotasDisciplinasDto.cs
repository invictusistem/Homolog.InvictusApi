using Invictus.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class NotasDisciplinasDto
    {

        public int id { get; set; }
        public string nome { get; set; }
        public int trimestre { get; set; }
        public string avaliacaoUm { get; set; }
        public string segundaChamadaAvaliacaoUm { get; set; }
        public string avaliacaoDois { get; set; }
        public string segundaChamadaAvaliacaoDois { get; set; }
        public string avaliacaoTres { get; set; }
        public string segundaChamadaAvaliacaoTres { get; set; }
        public int materiaId { get; set; }
        public string materiaDescricao { get; set; }
        public string resultado { get; set; }
        public int alunoId { get; set; }
        public int turmaId { get; set; }
        //public int boletimEscolarId { get; set; }
        //public virtual BoletimEscolar BoletimEscolar { get; private set; }
    }

    public class NotasDisciplinasDtoV2
    {

        public int id { get; set; }
        //public string nome { get; set; }
        public int trimestre { get; set; }
        public string avaliacaoUm { get; set; }
        public string segundaChamadaAvaliacaoUm { get; set; }
        public string avaliacaoDois { get; set; }
        public string segundaChamadaAvaliacaoDois { get; set; }
        public string avaliacaoTres { get; set; }
        public string segundaChamadaAvaliacaoTres { get; set; }
        public int materiaId { get; set; }
        public string materiaDescricao { get; set; }
        public string resultado { get; set; }
        public int alunoId { get; set; }
        public int turmaId { get; set; }
        //public int boletimEscolarId { get; set; }
        //public virtual BoletimEscolar BoletimEscolar { get; private set; }
    }
}
