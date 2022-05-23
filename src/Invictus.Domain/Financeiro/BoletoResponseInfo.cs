using System;

namespace Invictus.Domain.Financeiro
{
    public class BoletoResponseInfo //: Entity
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
        public BoletoResponseInfo() { }

        #endregion
    }
}
