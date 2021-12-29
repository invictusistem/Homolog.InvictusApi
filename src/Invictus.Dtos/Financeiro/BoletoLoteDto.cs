using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class BoletoLoteDto
    {
        public string vencimento { get; set; }
        public string valor { get; set; }
        public string juros { get; set; }
        public string juros_fixo { get; set; }
        public string multa { get; set; }
        public string multa_fixo { get; set; }
        public string desconto { get; set; }
        public string diasdesconto1 { get; set; }
        public string desconto2 { get; set; }
        public string diasdesconto2 { get; set; }
        public string desconto3 { get; set; }
        public string diasdesconto3 { get; set; }
        public string nunca_atualizar_boleto { get; set; }
        public string nome_cliente { get; set; }
        public string telefone_cliente { get; set; }
        public string cpf_cliente { get; set; }
        public string endereco_cliente { get; set; }
        public string complemento_cliente { get; set; }
        public string bairro_cliente { get; set; }
        public string cidade_cliente { get; set; }
        public string estado_cliente { get; set; }
        public string cep_cliente { get; set; }
        public string logo_url { get; set; }
        public string texto { get; set; }
        public string instrucoes { get; set; }
        public string instrucao_adicional { get; set; }
        public string grupo { get; set; }
        public string webhook { get; set; }
        public string pedido_numero { get; set; }
        public string especie_documento { get; set; }
        public string pix { get; set; }
    }
}
