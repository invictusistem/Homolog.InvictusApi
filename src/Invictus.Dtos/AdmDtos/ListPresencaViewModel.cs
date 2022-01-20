using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class ListPresencaViewModel
    {

        public ListPresencaViewModel()
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

    public class ListaPresencaDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
       
        public Guid calendarioId { get; set; }
        public bool isPresent { get; set; }
        public string isPresentToString { get; set; }
        public Guid alunoId { get; set; }
    }
}

