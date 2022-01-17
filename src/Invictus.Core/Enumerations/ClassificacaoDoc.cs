using System;


namespace Invictus.Core.Enumerations
{
    public class ClassificacaoDoc : Enumeration
    {
        public static ClassificacaoDoc Outros = new(1, "Outros");
        public static ClassificacaoDoc Contrato = new(2, "Contrato");

        public ClassificacaoDoc() { }
        public ClassificacaoDoc(int id, string name) : base(id, name) { }

        public static ClassificacaoDoc TryParse(string compare)
        {
            if (compare == "Outros")
            {
                return Outros;

            }
            else if (compare == "Contrato")
            {
                return Contrato;

            }

            throw new NotImplementedException();
        }

        public static string TryParseType(ClassificacaoDoc compare)
        {
            if (compare.DisplayName == "Outros")
            {
                return "Outros";

            }
            else if (compare.DisplayName == "Contrato")
            {
                return "Contrato";

            }

            throw new NotImplementedException();
        }
    }
}
