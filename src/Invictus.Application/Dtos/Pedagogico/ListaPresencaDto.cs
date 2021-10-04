
using System.Collections.Generic;

namespace Invictus.Application.Dtos.Pedagogico
{
    public class ListaPresencaDto
    {
        public string nome { get; set; }
        public int id { get; set; }
        public int calendarioId { get; set; }
        public bool isPresent { get; set; }
        public string isPresentToString { get; set; }
        public int alunoId { get; set; }
    }

    public class SavePresencaCommand
    {
        public List<ListaPresencaDto> listaPresencaDto { get; set; }
        public int calendarId { get; set; }
        public string observacoes { get; set; }
    }
}
