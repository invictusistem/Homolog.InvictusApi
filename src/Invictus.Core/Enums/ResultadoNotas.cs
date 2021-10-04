using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enums
{
    public class ResultadoNotas : Enumeration
    {
        public static ResultadoNotas Aguardo = new(1, "Aguardo");
        public static ResultadoNotas Aprovado = new(2, "Aprovado");
        public static ResultadoNotas Reprovado = new(3, "Reprovado");
        //public static MeioPagamento Credito = new(3, "Cartão de crédito");
        //public static MeioPagamento Boleto = new(4, "Boleto");
        //public static MeioPagamento Pix = new(5, "Pix");
        //public static Status Encerrada = new(4, "Encerrada");
        public ResultadoNotas()
        {

        }

        public static ResultadoNotas TryParse(string value)
        {
            if (value == "Aguardo") return Aguardo;
            if (value == "Aprovado") return Aprovado;
            if (value == "Reprovado") return Reprovado;

            throw new NotImplementedException();

        }
        public ResultadoNotas(int id, string name) : base(id, name) { }

    }
}
