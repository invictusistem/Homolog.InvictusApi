using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Administrativo
{
    public class FornecedorDto
    {
        public int id { get; set; }
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
        public string uf { get; set; }
        public string bairro { get; set; }
        public bool ativo { get; set; }
        public int unidadeId { get; set; }
        public string observacoes { get; set; }
    }

    public class FornecedorSaidaDto
    {        
        public long iId { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataVencimento { get; set; }
        public decimal valor { get; set; }
        public decimal valorPago { get; set; }
        public DateTime? diaPagamento { get; set; }
        public string meioPagamento { get; set; }
        public string statusPagamento { get; set; }
        public string descricaoTransacao { get; set; }
        public string comentario { get; set; }
        public long fornecedorId { get; set; }
        public int unidadeId { get; set; }

    }


    public class FornecedorEntradaDto
    {    
        public long id { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataVencimento { get; set; }
        public decimal valor { get; set; }
        public decimal valorComDesconto { get; set; }
        public decimal valorPago { get; set; }
        public DateTime? diaPagamento { get; set; }
        public string meioPagamento { get; set; }
        public string statusPagamento { get; set; }
        public string descricaoTransacao { get; set; }
        public string comentario { get; set; }
        public long fornecedorId { get; set; }
        public int unidadeId { get; set; }
    }
}
