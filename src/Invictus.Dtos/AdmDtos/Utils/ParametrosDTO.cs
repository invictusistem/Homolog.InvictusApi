using System;

namespace Invictus.Dtos.AdmDtos.Utils
{
    public class ParametrosDTO
    {
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public bool ativo { get; set; }
        public bool todasUnidades { get; set; }
        public bool primeiraReq { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
