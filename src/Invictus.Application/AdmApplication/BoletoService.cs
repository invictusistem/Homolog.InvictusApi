using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Logs;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.PedagDto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class DadosPessoaDto
    {
        public string nome { get; set; }
        public string telefone { get; set; }
        public string cpf { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
    }
    public class BoletoService : IBoletoService
    {
        private readonly ILogger<BoletoService> _logger;
        private readonly InvictusDbContext _db;
        public BoletoService(ILogger<BoletoService> logger, InvictusDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        //public List<BoletoLoteResponse> GerarBoletosEmLote(List<BoletoLoteDto> boletos)
        public List<BoletoLoteResponse> GerarBoletosEmLote(List<Parcela> boletos, DadosPessoaDto pessoa)
        {
            var parametross = new Dictionary<string, string>();
            var url = "https://sandbox.pjbank.com.br/recebimentos/27f8b64b8168ee9d9775d3b529e985fd8e3698aa/transacoes";
            //var url = "https://sandbox.pjbank.com.br/recebimentos/27f8b64b8168ee9d9775d3b529e985fd8e3698aa/transacoes

            //parametross.Add("key1", "value1");
            //parametross.Add("key2", "value2");

            // criar numeros sequenciais baseado no count

            for (int i = 0; i < boletos.Count(); i++)
            {


                parametross.Add("cobrancas" + "[" + i + "]" + "[vencimento]", boletos[i].vencimento.ToString("MM/dd/yyyy"));   // "12/30/2021");
                parametross.Add("cobrancas" + "[" + i + "]" + "[valor]", boletos[i].valor.ToString());
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
                parametross.Add("cobrancas" + "[" + i + "]" + "[nome_cliente]", pessoa.nome); //boletos[i].nomete);// ;// ;
                //parametross.Add("email_cliente", "value1");
                parametross.Add("cobrancas" + "[" + i + "]" + "[telefone_cliente]", pessoa.telefone); //boletos[i].telefone_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[cpf_cliente]", pessoa.cpf); //boletos[i].cpf_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[endereco_cliente]", pessoa.logradouro); //boletos[i].endereco_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[complemento_cliente]", "");
                parametross.Add("cobrancas" + "[" + i + "]" + "[bairro_cliente]", pessoa.bairro); //boletos[i].bairro_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[cidade_cliente]", pessoa.cidade); //boletos[i].cidade_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[estado_cliente]", pessoa.estado); //boletos[i].estado_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[cep_cliente]", pessoa.cep); //boletos[i].cep_cliente);
                parametross.Add("cobrancas" + "[" + i + "]" + "[logo_url]", "https://pjbank.com.br/assets/images/logo-pjbank.png");
                parametross.Add("cobrancas" + "[" + i + "]" + "[texto]", "Texto opcional");
                parametross.Add("cobrancas" + "[" + i + "]" + "[instrucoes]", "Este é um boleto de exemplo");
                parametross.Add("cobrancas" + "[" + i + "]" + "[instrucao_adicional]", "Este boleto não deve ser pago pois é um exemplo");
                parametross.Add("cobrancas" + "[" + i + "]" + "[grupo]", "Boletos00" + i);
                parametross.Add("cobrancas" + "[" + i + "]" + "[webhook]", "http://example.com.br");
                parametross.Add("cobrancas" + "[" + i + "]" + "[pedido_numero]", (i+1).ToString());
                parametross.Add("cobrancas" + "[" + i + "]" + "[especie_documento]", "DS");
                parametross.Add("cobrancas" + "[" + i + "]" + "[pix]", "pix-e-boleto");

            }

            IEnumerable<BoletoLoteResponse> boletosResponse = new List<BoletoLoteResponse>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(parametross)).Result;

                var retorno = response.Content.ReadAsStringAsync().Result;


                boletosResponse = JsonSerializer.Deserialize<List<BoletoLoteResponse>>(retorno);

                // var resultado =  JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result);
                //result = JsonConvert.DeserializeObject<List<MediaPulseReturn>>(retorno);
            }

            return boletosResponse.ToList();
        }

        public async Task<List<BoletoLoteResponse>> GerarBoletosUnicos(List<Parcela> boletos, decimal valorBonusPontualidade, DadosPessoaDto pessoa)
        {
            _logger.LogInformation($"Start to create boletos at {DateTime.UtcNow.TimeOfDay}");

            var url = "https://sandbox.pjbank.com.br/recebimentos/27f8b64b8168ee9d9775d3b529e985fd8e3698aa/transacoes";

            var boletosResponse = new List<BoletoLoteResponse>();

            for (int i = 0; i < boletos.Count(); i++)
            {
                int qntBoletosSalvos = _db.LogBoletos.Count();

                NumberFormatInfo config = new NumberFormatInfo();
                config.NumberDecimalSeparator = ".";

                var parametross = new Dictionary<string, string>();
                parametross.Add("vencimento", boletos[i].vencimento.ToString("MM/dd/yyyy"));   // "12/30/2021");
                parametross.Add("valor", boletos[i].valor.ToString());
                parametross.Add("juros", "1");
                parametross.Add("juros_fixo", "0");
                parametross.Add("multa", "0");
                parametross.Add("multa_fixo", "0");
                parametross.Add("desconto", valorBonusPontualidade.ToString(config)); // colocar bonus pontualidade AQUI
                parametross.Add("diasdesconto1", "");
                parametross.Add("desconto2", "");
                parametross.Add("diasdesconto2", "");
                parametross.Add("desconto3", "");
                parametross.Add("diasdesconto3", "");
                parametross.Add("nunca_atualizar_boleto", "1");
                parametross.Add("nome_cliente", pessoa.nome); //boletos[i].nomete);// ;// ;
                //parametross.Add("email_cliente", "value1");
                parametross.Add("telefone_cliente", pessoa.telefone); //boletos[i].telefone_cliente);
                parametross.Add("cpf_cliente", pessoa.cpf); //boletos[i].cpf_cliente);
                parametross.Add("endereco_cliente", pessoa.logradouro); //boletos[i].endereco_cliente);
                parametross.Add("complemento_cliente", "");
                parametross.Add("bairro_cliente", pessoa.bairro); //boletos[i].bairro_cliente);
                parametross.Add("cidade_cliente", pessoa.cidade); //boletos[i].cidade_cliente);
                parametross.Add("estado_cliente", pessoa.estado); //boletos[i].estado_cliente);
                parametross.Add("cep_cliente", pessoa.cep); //boletos[i].cep_cliente);
                parametross.Add("logo_url", "https://pjbank.com.br/assets/images/logo-pjbank.png");
                parametross.Add("texto", "");
                parametross.Add("instrucoes", "");
                parametross.Add("instrucao_adicional", "");
                parametross.Add("grupo", "Boletos00" + i);
               // parametross.Add("webhook", "http://example.com.br");
                parametross.Add("pedido_numero", qntBoletosSalvos.ToString());
                parametross.Add("especie_documento", "DS");
                parametross.Add("pix", "pix-e-boleto");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(parametross)).Result;

                    var retorno = response.Content.ReadAsStringAsync().Result;

                    var boletoLog = new LogBoletos(Guid.NewGuid(), retorno, DateTime.Now);

                    _db.LogBoletos.Add(boletoLog);

                    _db.SaveChanges();

                    boletosResponse.Add(JsonSerializer.Deserialize<BoletoLoteResponse>(retorno));

                    _logger.LogInformation($"Boleto {i + 1} create at {DateTime.UtcNow.TimeOfDay}");

                }



            }

            return boletosResponse;
        }
    }
}
