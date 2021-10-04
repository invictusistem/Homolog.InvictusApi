using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class CreateTurmaViewModel
    {
        public CreateTurmaViewModel()
        {
            turnos = new List<TurnosViewModel>();
            // diasSemanaDisponiveis = new List<string>();
        }
        public List<TurnosViewModel> turnos { get; set; }
        // public List<string> diasSemanaDisponiveis { get; set; }

    }
}
