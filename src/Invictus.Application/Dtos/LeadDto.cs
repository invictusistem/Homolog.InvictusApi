using System;

namespace Invictus.Application.Dtos
{
    public class LeadDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string data { get; set; }
        public string telefone { get; set; }
        public string bairro { get; set; }
        public string cursoPretendido { get; set; }
        public string unidade { get; set; }
        public DateTime dataInclusaoSistema { get; set; }
        public string responsavelLead { get; set; }
    }
}
