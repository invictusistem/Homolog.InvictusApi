using System;

namespace Invictus.Core.Enumerations
{
    public class TipoConta : Enumeration
    {
        public static TipoConta Corrente = new(1, "Corrente");
        public static TipoConta Poupanca = new(2, "Poupança");
        
        public TipoConta()
        {

        }
        public TipoConta(int id, string name) : base(id, name) { }

        public static TipoConta TryParse(string resp)
        {
            if (resp == "Corrente")
            {
                return Corrente;

            }
            else if (resp == "Poupança")
            {
                return Poupanca;

            }
            else if (resp == null)
            {
                return null;

            }

            throw new NotImplementedException();
        }
    }
}