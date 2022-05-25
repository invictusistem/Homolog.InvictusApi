using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class FornecedorDto
    {
        public Guid id { get; set; }
        public string razaoSocial { get; set; }
        public string ie_rg { get; set; }
        public string cnpj_cpf { get; set; }
        public string email { get; set; }
        public string telContato { get; set; }
        public string whatsApp { get; set; }
        public string nomeContato { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string numero { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public bool ativo { get; set; }
        public string unidadeSigla { get; set; }
        public Guid unidadeId { get; set; }

    }
}
