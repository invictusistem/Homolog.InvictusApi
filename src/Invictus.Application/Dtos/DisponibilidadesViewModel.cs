using System;
using System.Collections.Generic;

namespace Invictus.Application.Dtos 
{ 
    public class DisponibilidadesViewModel
    {
        public DisponibilidadesViewModel()
        {
            availabilityTwo = new List<DisponibilidadeDto>();
        }
        public DisponibilidadeDto availabilityOne { get; set; }
        public List<DisponibilidadeDto> availabilityTwo { get; set; }
    }

    public class DisponibilidadeDto
    {
        public string horarioDisponivelView { get; set; }
        public string diaSemana { get; set; }
        public string diaSemanaView { get; set; }
        public DateTime diaData { get; set; }
        public List<HorariosViewDto> horarios { get; set; }
    }

    public class HorariosViewDto
    {
        public string inicio { get; set; } //ex: 09:00
        public string fim { get; set; } //ex: 12:00
    }

    // V2

    public class DisponibilidadesViewModelV2
    {
        public DisponibilidadesViewModelV2()
        {
            //availabilityDayOne = new List<DisponibilidadeDtoV2>();
            availabilityTwo = new List<DisponibilidadeDay2DtoV2>();
        }
        public string horarioDisponivelView { get; set; }
        public DisponibilidadeDayOnetoV2 availabilityDayOne { get; set; }

        public List<DisponibilidadeDay2DtoV2> availabilityTwo { get; set; }
    }

    public class DisponibilidadeDayOnetoV2
    {
        public DisponibilidadeDayOnetoV2()
        {
            ranges = new List<Tuple<DateTime, DateTime>>();
        }
        //public string horarioDisponivelView { get; set; }
        public string diaSemana { get; set; }

        public DateTime horaIni { get; set; }
        public DateTime horaFim { get; set; }
        public List<Tuple<DateTime, DateTime>> ranges {get;set;}
        //
        public string diaSemanaView { get; set; }
        public DateTime diaData { get; set; }
        ///public List<HorariosViewDtoV2> horarios { get; set; }
    }

    public class DisponibilidadeDay2DtoV2
    {
        //public DisponibilidadeDtoV2()
        //{
        //    horarios = new List<HorariosViewDtoV2>();
        //}
        //public string horarioDisponivelView { get; set; }
        public string diaSemana { get; set; }

    public DateTime horaIni { get; set; }
    public DateTime horaFim { get; set; }
    //
    public string diaSemanaView { get; set; }
    public DateTime diaData { get; set; }
    ///public List<HorariosViewDtoV2> horarios { get; set; }
}

public class HorariosViewDtoV2
    {
        public string inicio { get; set; } //ex: 09:00
        public string fim { get; set; } //ex: 12:00
    }
}
