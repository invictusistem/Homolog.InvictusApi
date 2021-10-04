using Invictus.Application.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Services
{
    public class BoletoService : IBoletoService
    {

        public List<BoletoLoteResponse> EnviaRequisicaoLote(List<BoletoLoteDto> boletos)
        {
            var parametross = new Dictionary<string, string>();
            var url = "https://sandbox.pjbank.com.br/recebimentos/27f8b64b8168ee9d9775d3b529e985fd8e3698aa/transacoes";
            //var url = "https://sandbox.pjbank.com.br/recebimentos/27f8b64b8168ee9d9775d3b529e985fd8e3698aa/transacoes

            //parametross.Add("key1", "value1");
            //parametross.Add("key2", "value2");

            for (int i = 0; i < boletos.Count(); i++)
            {


                parametross.Add("cobrancas" + "[" + i + "]" + "[vencimento]", boletos[i].vencimento);   // "12/30/2021");
                parametross.Add("cobrancas" + "[" + i + "]" + "[valor]", boletos[i].valor);
                parametross.Add("cobrancas" + "[" + i + "]" + "[juros]", "0");
                parametross.Add("cobrancas" + "[" + i + "]" + "[juros_fixo]", "0");
                parametross.Add("cobrancas" + "[" + i + "]" + "[multa]", "0");
                parametross.Add("cobrancas" + "[" + i + "]" + "[multa_fixo]", "0");
                parametross.Add("cobrancas" + "[" + i + "]" + "[desconto]", "20.00");
                parametross.Add("cobrancas" + "[" + i + "]" + "[diasdesconto1]", "");
                parametross.Add("cobrancas" + "[" + i + "]" + "[desconto2]", "");
                parametross.Add("cobrancas" + "[" + i + "]" + "[diasdesconto2]", "");
                parametross.Add("cobrancas" + "[" + i + "]" + "[desconto3]", "");
                parametross.Add("cobrancas" + "[" + i + "]" + "[diasdesconto3]", "");
                parametross.Add("cobrancas" + "[" + i + "]" + "[nunca_atualizar_boleto]", "0");
                parametross.Add("cobrancas" + "[" + i + "]" + "[nome_cliente]", boletos[i].nome_cliente);
                //parametross.Add("email_cliente", "value1");
                parametross.Add("cobrancas" + "[" + i + "]" + "[telefone_cliente]", boletos[i].telefone_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[cpf_cliente]", boletos[i].cpf_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[endereco_cliente]", boletos[i].endereco_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[complemento_cliente]", "");
                parametross.Add("cobrancas" + "[" + i + "]" + "[bairro_cliente]", boletos[i].bairro_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[cidade_cliente]", boletos[i].cidade_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[estado_cliente]", boletos[i].estado_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[cep_cliente]", boletos[i].cep_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[logo_url]", "https://pjbank.com.br/assets/images/logo-pjbank.png");
                parametross.Add("cobrancas" + "[" + i + "]" + "[texto]", "Texto opcional");
                parametross.Add("cobrancas" + "[" + i + "]" + "[instrucoes]", "Este é um boleto de exemplo");
                parametross.Add("cobrancas" + "[" + i + "]" + "[instrucao_adicional]", "Este boleto não deve ser pago pois é um exemplo");
                parametross.Add("cobrancas" + "[" + i + "]" + "[grupo]", "Boletos00" + i);
                parametross.Add("cobrancas" + "[" + i + "]" + "[webhook]", "http://example.com.br");
                parametross.Add("cobrancas" + "[" + i + "]" + "[pedido_numero]", "89724");
                parametross.Add("cobrancas" + "[" + i + "]" + "[especie_documento]", "DS");
                parametross.Add("cobrancas" + "[" + i + "]" + "[pix]", "pix-e-boleto");

            }

            IEnumerable<BoletoLoteResponse> boletosResponse = new List<BoletoLoteResponse>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(parametross)).Result;
                
                var retorno = response.Content.ReadAsStringAsync().Result;


                boletosResponse = JsonConvert.DeserializeObject<List<BoletoLoteResponse>>(retorno);

                // var resultado =  JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result);
                //result = JsonConvert.DeserializeObject<List<MediaPulseReturn>>(retorno);
            }

            return boletosResponse.ToList();
        }
    }

    public class BoletoLoteResponse
    {
        [JsonIgnore]
        public long id { get; set; }
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
    }

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
