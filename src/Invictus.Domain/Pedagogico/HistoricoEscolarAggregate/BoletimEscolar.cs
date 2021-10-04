using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.HistoricoEscolarAggregate
{

    public class BoletimEscolar
    {
        public BoletimEscolar()
        {

        }
        public BoletimEscolar(int id,
                              string disciplina)
        {
            Id = id;
            Disciplina = disciplina;
            //Notas = new List<NotasDisciplinas>();

        }
        public int Id { get; set; }
        public string Disciplina { get; set; }
        //public List<NotasDisciplinas> Notas { get; set; }
        public int HistoricoId { get; set; }
        public virtual HistoricoEscolar HistoricoEscolar { get; set; }

    }   

}
