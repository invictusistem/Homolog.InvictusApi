using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class BoletoViewModel
    {
        public Guid id { get; set; }
        public DateTime vencimento { get; set; }
        public DateTime dataPagamento { get; set; }
        public decimal valor { get; set; }
        public decimal valorPago { get; set; }
        public int juros { get; set; }
        public int jurosFixo { get; set; }
        public string multa { get; set; }
        public string multaFixo { get; set; }
        public decimal desconto { get; set; }
        public string tipo { get; set; }
        public string diasDesconto { get; set; }
        public string statusBoleto { get; set; }
        public string historico { get; set; }
        public string subConta { get; set; }
        public bool ativo { get; set; }
        public Guid? subContaId { get; set; }
        public Guid? bancoId { get; set; }
        public Guid? centroCustoId { get; set; }
        public Guid? meioPagamentoId { get; set; }
        public string formaPagamento { get; set; }
        public string digitosCartao { get; set; }
        public bool ehFornecedor { get; set; }
        public string tipoPessoa { get; set; }
        public Guid pessoaId { get; set; } 
        public DateTime dataCadastro { get; set; }
        public Guid? reparcelamentoId { get; set; }
        public Guid? centroCustoUnidadeId { get; set; }        
        public Guid responsavelCadastroId { get; set; }
        public BoletoResponseViewModel infoBoletos { get; set; }
    }

    public class BoletoResponseViewModel
    {
        public string id_unico { get; set; }
        public string id_unico_original { get; set; }
        public string status { get; set; }
        public string msg { get; set; }
        public string nossonumero { get; set; }
        public string linkBoleto { get; set; }
        public string linkGrupo { get; set; }
        public string linhaDigitavel { get; set; }
        public string pedido_numero { get; set; }
        public string banco_numero { get; set; }
        public string token_facilitador { get; set; }
        public string credencial { get; set; }
        //public Guid boletoId { get; set; }
      

    }

}
