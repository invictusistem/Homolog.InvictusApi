using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro
{
    public class Boleto : Entity
    {

        public Boleto(DateTime vencimento,
                   // DateTime dataPagamento,
                    decimal valor,
                   // decimal valorPago,
                    int juros,
                    int jurosFixo,
                    string multa,
                    string multaFixo,
                    string desconto,
                    string diasDesconto,
                    StatusPagamento statusBoleto,
                    //Guid reparcelamentoId,
                     Guid centroCustoUnidadeId,
                    Guid informacaoDebitoId,
                    BoletoResponseInfo infoBoletos
                    )
        {
            Vencimento = vencimento;
          //  DataPagamento = dataPagamento;
            Valor = valor;
          //  ValorPago = valorPago;
            Juros = juros;
            JurosFixo = jurosFixo;
            Multa = multa;
            MultaFixo = multaFixo;
            Desconto = desconto;
            DiasDesconto = diasDesconto;
            StatusBoleto = statusBoleto.DisplayName;
            //ReparcelamentoId = reparcelamentoId;
            CentroCustoUnidadeId = centroCustoUnidadeId;
            InformacaoDebitoId = informacaoDebitoId;
            InfoBoletos = infoBoletos;

        }

        public DateTime Vencimento { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public decimal Valor { get; private set; }
        public decimal ValorPago { get; private set; }
        public int Juros { get; private set; }
        public int JurosFixo { get; private set; }
        public string Multa { get; private set; }
        public string MultaFixo { get; private set; }
        public string Desconto { get; private set; }
        public string DiasDesconto { get; private set; }
        public string StatusBoleto { get; private set; }   
        public Guid ReparcelamentoId { get; private set; }
        public Guid CentroCustoUnidadeId { get; private set; }
        public Guid InformacaoDebitoId { get; private set; }
        public BoletoResponseInfo InfoBoletos { get; private set; }

        #region EF
        
        public Boleto() { }
        public virtual InformacaoDebito InformacaoDebito { get; private set; }

        #endregion

    }

    public class BoletoResponseInfo : Entity
    {
        public BoletoResponseInfo(string id_unico,
                                string id_unico_original,
                                 string status,
                                string msg,
                                string nossonumero,
                                string linkBoleto,
                                 string linkGrupo,
                                string linhaDigitavel,
                                string pedido_numero,
                                string banco_numero,
                                string token_facilitador,
                                string credencial
                                )
        {
            Id_unico = id_unico;
            Id_unico_original = id_unico_original;
            Status = status;
            Msg = msg;
            Nossonumero = nossonumero;
            LinkBoleto = linkBoleto;
            LinkGrupo = linkGrupo;
            LinhaDigitavel = linhaDigitavel;
            Pedido_numero = pedido_numero;
            Banco_numero = banco_numero;
            Token_facilitador = token_facilitador;
            Credencial = credencial;

        }
        public string Id_unico { get; private set; }
        public string Id_unico_original { get; private set; }
        public string Status { get; private set; }
        public string Msg { get; private set; }
        public string Nossonumero { get; private set; }
        public string LinkBoleto { get; private set; }
        public string LinkGrupo { get; private set; }
        public string LinhaDigitavel { get; private set; }
        public string Pedido_numero { get; private set; }
        public string Banco_numero { get; private set; }
        public string Token_facilitador { get; private set; }
        public string Credencial { get; private set; }

        #region EF

        public Guid BoletoId { get; private set; }
        public virtual Boleto Boleto { get; private set; }
        public BoletoResponseInfo(){ }

        #endregion
    }
}
