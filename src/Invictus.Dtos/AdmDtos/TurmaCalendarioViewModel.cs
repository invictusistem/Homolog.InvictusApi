using System;
using System.Collections.Generic;

namespace Invictus.Dtos.AdmDtos
{
    public class TurmaCalendarioViewModel
    {
        public Guid id { get; set; }
        public DateTime diaaula { get; set; }
        public string diadasemana { get; set; }
        public string horainicial { get; set; }
        public string horafinal { get; set; }
        public bool aulainiciada { get; set; }
        public bool aulaconcluida { get; set; }
        public string observacoes { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string Nome { get; set; }
        public bool? podeVerRelatorioAula { get; set; }
        public string professor { get; set; }
    }

    public class ProfessorCalendarioViewModel
    {
        public Guid id { get; set; }
        public DateTime diaaula { get; set; }
        public string diadasemana { get; set; }
        public string horainicial { get; set; }
        public string horafinal { get; set; }
        public bool aulainiciada { get; set; }
        public bool aulaconcluida { get; set; }
        public DateTime dateAulaIniciada { get; set; }
        public DateTime dateAulaConcluida { get; set; }
        public int totalClassroomMinutes { get; set; }
        public string observacoes { get; set; }
        public string turma { get; set; }
        public string unidadeDescricao { get; set; }
        public string titulo { get; set; }
        public string materiaDescricao { get; set; }
        public bool? podeVerRelatorioAula { get; set; }
        public string professor { get; set; }
    }


    public class ProfessorRelatorioViewModel
    {
        public ProfessorRelatorioViewModel()
        {
            calendars = new List<ProfessorCalendarioViewModel>();
        }
        public string TotalHoursToString { get; set; }
        public int totalMinutes { get; set; }

        public IEnumerable<ProfessorCalendarioViewModel> calendars { get; set; }
    }


}
