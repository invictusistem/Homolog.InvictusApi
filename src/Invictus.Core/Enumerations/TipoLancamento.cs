using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class TipoLancamento : Enumeration
    {
        public static TipoLancamento Debito = new(1, "Débito");
        public static TipoLancamento Credito = new(2, "Crédito");

        public TipoLancamento() { }
        public TipoLancamento(int id, string name) : base(id, name) { }

        public static TipoLancamento TryParse(string compare)
        {
            if (compare == "debito")
            {
                return Debito;

            }
            else if (compare == "credito")
            {
                return Credito;

            }

            throw new NotImplementedException();
        }       
    }
}
