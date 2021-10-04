using System;

namespace Invictus.Application.Dtos
{
    public class CalendarioDto
    {       
        public int Id { get; set; }
        public DateTime DiaAula { get; set; }
        public string Turno { get; set; }
        public string DiaDaSemana { get; set; }
        public string Materia { get; set; } //Set reserva previsao!
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string ProfessorId { get; set; }
        public string Turma { get; set; }
        public string Unidade { get; set; }
        public string Sala { get; set; }
    }
}
