using System.Collections.Generic;

namespace Invictus.Application.Dtos
{
    public class ProfessoresDto
    {
        public ProfessoresDto()
        {
            materias = new List<ProfessoresMateriaDto>();
        }
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public int ProfId { get; set; }
        public int TurmaPedagogicoId { get; set; }
        public List<ProfessoresMateriaDto> materias { get; set; }
    }
}
