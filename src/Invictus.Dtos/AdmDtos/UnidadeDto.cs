using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class UnidadeDto
    {
        public Guid id { get; set; }
        public string sigla { get; set; }
        public string cnpj { get; set; }
        public string descricao { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string complemento { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public bool isUnidadeGlobal { get; set; }
        public bool ativo { get; set; }

        //public List<Modulo> Modulos { get; private set; }
        public List<SalaDto> salas { get; set; }
    }
}
