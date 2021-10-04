using System;
using System.Collections.Generic;

namespace Invictus.Application.Dtos
{
    public class Dias
    {
        public Dias()
        {
            calendarios = new List<CalendarioDto>();
        }
        public DateTime dia { get; set; }
        public List<CalendarioDto> calendarios { get; set; }
    }
}
