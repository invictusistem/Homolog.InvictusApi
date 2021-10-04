using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Pedagogico
{
    public class ListaPresencaViewModel
    {
        public ListaPresencaViewModel()
        {
            listaPresencas = new List<ListaPresencaDto>();
        }
        public InfoDia infos { get; set; }
        public List<ListaPresencaDto> listaPresencas { get; set; }
    }

    public class InfoDia
    {
        public int id { get; set; }
        public DateTime diaAula { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string nome { get; set; }
    }
}
