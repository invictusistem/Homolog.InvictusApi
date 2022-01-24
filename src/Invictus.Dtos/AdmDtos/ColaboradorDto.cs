using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class ColaboradorDto
    {
        //public ColaboradorDto()
        //{
        //    id = Guid.NewGuid();
        //}
        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string celular { get; set; }
        //public string cargo { get; set; }
        public Guid cargoId { get; set; }
        public Guid unidadeId { get; set; }
        public string numero { get; set; }
        //public string perfil { get; set; }
        // public bool perfilAtivo { get; set; }
        public bool ativo { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public DateTime dataCriacao { get; set; }
       // public string qualquerCoisa { get; set; }
    }
}
