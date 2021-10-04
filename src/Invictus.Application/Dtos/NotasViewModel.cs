using System.Collections.Generic;

namespace Invictus.Application.Dtos
{
    public class NotasViewModel
    {
        public NotasViewModel()
        {
            alunos = new List<AlunosENotas>();
        }
        public int id { get; set; }
        public string materia { get; set; }
        public string dataAv1 { get; set; }
        public string dataAv2 { get; set; }
        public string dataAv3 { get; set; }
        public List<AlunosENotas> alunos { get; set; }
    }

    public class AlunosENotas
    {
        public int alunoId{get;set;}
        public int listaNotasId { get; set; }
        public string materia { get; set; }
        public string nome { get; set; }
        public string av1 { get; set; }
        public string av2 { get; set; }
        public string av3 { get; set; }
        public bool disabledv1 { get; set; }
        public bool disabledv2 { get; set; }
        public bool disabledv3 { get; set; }
        public int historicoId { get; set; }
    }
}
