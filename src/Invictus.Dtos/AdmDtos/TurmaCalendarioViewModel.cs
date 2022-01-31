using System;

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
}
