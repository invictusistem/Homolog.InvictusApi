using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class PacoteMateriasViewModel
    {      
            public Guid id { get; set; }
            public Guid materiaId { get; set; }
            // public string descricao { get; set; }
            public string nome { get; set; }
            public int ordem { get; set; }
            public int cargaHoraria { get; set; }
            public string modalidade { get; set; }
            public Guid pacoteId { get; set; }
            public int qntAulas { get; set; }

        public void SetQntAulas(List<DiasSemanaCommand> diasSemana)
        {
            var cargaHorarioTotalEmMinutos = cargaHoraria * 60;
            var totalaulas = 0;
            while (cargaHorarioTotalEmMinutos > 0)
            {
                foreach (var dia in diasSemana)
                {
                    cargaHorarioTotalEmMinutos -= dia.totalMinutos;
                    totalaulas++; 
                    if(cargaHorarioTotalEmMinutos == 0 || cargaHorarioTotalEmMinutos  < 0) break;
                }
            }

            qntAulas = totalaulas;            
        }
    }    
}
