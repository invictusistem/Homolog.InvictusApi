
using System;

namespace Invictus.Core.Enums
{
    public class MeioPagamento : Enumeration
    {
        public static MeioPagamento Dinheiro = new(1, "Dinheiro");
        public static MeioPagamento Debito = new(2, "Cartão de débito");
        public static MeioPagamento Credito = new(3, "Cartão de crédito");
        public static MeioPagamento Boleto = new(4, "Boleto");
        public static MeioPagamento Pix = new(5, "Pix");
        //public static Status Encerrada = new(4, "Encerrada");
        public MeioPagamento()
        {

        }
        public MeioPagamento(int id, string name) : base(id, name) { }

        public static MeioPagamento TryParse(string compare)
        {
            if (compare == "dinheiro") 
            {
                return Dinheiro;

            } else if (compare == "debito") 
            {
                return Debito;

            } else if(compare == "credito")
            {
                return Credito;
            }else if (compare == "pix")
            {
                return Pix;
            }
            else if (compare == "boleto")
            {
                return Boleto;
            }

            throw new NotImplementedException();
        }

        public static MeioPagamento TryParseMatricula(string compare)
        {
            if (compare == "dinheiro")
            {
                return Dinheiro;

            }
            else if (compare == "cartaoDébito")
            {
                return Debito;

            }
            else if (compare == "cartaoCredito")
            {
                return Credito;

            }
            else if (compare == "pix")
            {
                return Pix;

            }
            else
            {
                return Boleto;
            }
        }

        /*
         boleto
cartaoDébito
cartaoCredito
pix
dinheiro
        */
    }
}
