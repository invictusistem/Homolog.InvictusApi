using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class ResultadoNotas : Enumeration
    {
        public static ResultadoNotas Aguardo = new(1, "Aguardo");
        public static ResultadoNotas Aprovado = new(2, "Aprovado");
        public static ResultadoNotas Reprovado = new(3, "Reprovado");
       
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
