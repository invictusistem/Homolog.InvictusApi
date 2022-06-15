using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class PessoaDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string nomeSocial { get; set; }
        public string pai { get; set; }
        public string mae { get; set; }
        public DateTime nascimento { get; set; }
        public string naturalidade { get; set; }
        public string naturalidadeUF { get; set; }
        public string razaoSocial { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string cnpj { get; set; }
        //public string CNPJ_CPF { get; private set; }
        public string ie_rg { get; set; }
        public Guid? cargoId { get; set; }
        public string email { get; set; }
        public string nomeContato { get; set; }
        public string telefoneContato { get; set; }
        public string celular { get; set; }
        public string telResidencial { get; set; }
        public string telWhatsapp { get; set; }
        public DateTime dataCadastro { get; set; }
        public Guid pessoaRespCadastroId { get; set; }
        public string tipoPessoa { get; set; }
        public bool ativo { get; set; }
        public DateTime? dataEntrada { get; set; }
        public DateTime? dataSaida { get; set; }
        public Guid unidadeId { get; set; }
        public string unidadeSigla { get; set; }
        public EnderecoDto endereco { get;  set; } 
        //public Guid bairroId { get; set; }
        //public string bairro { get; set; }
        //public string cep { get; set; }
        //public string complemento { get; set; }
        //public string logradouro { get; set; }
        //public string numero { get; set; }
        //public string cidade { get; set; }
        //public string uf { get; set; }
        //public Guid pessoaId { get; set; }

    }

    public class EnderecoDto
    {
        public Guid id { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string complemento { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public Guid pessoaId { get; set; }
    }
}
