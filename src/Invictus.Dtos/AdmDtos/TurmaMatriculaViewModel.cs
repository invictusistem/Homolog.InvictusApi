using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class TurmaMatriculaViewModel
    {
        public Guid id { get; set; }
        public string identificador { get; set; }
        public string descricao { get; set; }
        public int totalAlunos { get; set; }
        public int minimoAlunos { get; set; }
        public string statusAndamento { get; set; }
        public Guid unidadeId { get; set; }
        //public Guid salaId { get; set; }
        //public Guid pacoteId { get; set; }
        public Guid typePacoteId { get; set; }
        //public virtual Previsao Previsao { get; set; }
        public List<TurmaHorarioDto> Horarios { get; set; }
        //public List<TurmaMaterias> Materias { get; set; }
    }

    public class TurmaHorarioDto
    {
        public Guid turmaHorarioId { get; set; }
        public string DiaSemanada { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioFim { get; set; }

    }
}
