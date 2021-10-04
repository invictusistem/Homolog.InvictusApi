using System;

namespace Invictus.Application.Dtos
{
    public class TurmaViewModel
    {
        public int id { get; set; }
        public string identificador { get; set; }
        public string descricao { get; set; }
        public int moduloId { get; set; }
        public bool podeIniciar { get; set; }
        public bool aulaIniciada { get; set; }
        public string turno { get; set; }
        public int vagas { get; set; }
        public string StatusDaTurma { get; set; }
        public int totalAlunos { get; set; }
        public int minimoAlunos { get; set; }
        public bool iniciada { get; set; }
        public DateTime previsaoAtual { get; set; }
        public DateTime previsaoTerminoAtual { get; set; }
        public string previsao { get; set; }
        public string weekDayOne { get; set; }
        public string initialHourOne { get; set; }
        public string finalHourOne { get; set; }
        public int calendarioId { get; set; }
        public PrivisoesDto previsoes { get; set; }

    }

    public class PrivisoesDto
    {
        public int PrevisoesId { get; set; }
        public bool iniciada { get; set; }
        public string previsaoAtual { get; set; }
        public DateTime previsionStartOne { get; set; }
        public DateTime previsionEndingOne { get; set; }
        public DateTime previsionStartTwo { get; set; }
        public DateTime previsionEndingTwo { get; set; }
        public DateTime previsionStartThree { get; set; }
        public DateTime previsionEndingThree { get; set; }
        public int turmaId { get; set; }
    }
}
