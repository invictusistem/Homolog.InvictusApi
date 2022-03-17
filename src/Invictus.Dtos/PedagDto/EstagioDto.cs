using System;

namespace Invictus.Dtos.PedagDto
{
    public class EstagioDto
    {       
        public Guid id { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public DateTime dataInicio { get; set; }
        public int vagas { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public bool ativo { get; set; }
        public Guid supervisorId { get; set; }        
    }
}
