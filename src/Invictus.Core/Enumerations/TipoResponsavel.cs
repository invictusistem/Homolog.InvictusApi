using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class TipoResponsavel : Enumeration
    {
        public static TipoResponsavel ResponsavelFinanceiro = new(1, "Responsável financeiro");
        public static TipoResponsavel ResponsavelMenor = new(2, "Responsável menor");

        public TipoResponsavel() { }
        public TipoResponsavel(int id, string name) : base(id, name) { }

        public static TipoResponsavel TryParse(string resp)
        {
            if (resp == "Responsável financeiro")
            {
                return ResponsavelFinanceiro;

            }
            else if (resp == "Responsável menor")
            {
                return ResponsavelMenor;

            }

            throw new NotImplementedException();
        }
    }
}
