
namespace Invictus.Core.Enumerations
{
    public class StatusDocumento : Enumeration
    {
        public static StatusDocumento NaoEnviado = new(1, "Não enviado");
        public static StatusDocumento AguardandoAnalise = new(2, "Aguardando análise");
        public static StatusDocumento Aprovado = new(2, "Aprovado");
        public static StatusDocumento Reprovado = new(2, "Reprovado");

        public StatusDocumento() { }
        public StatusDocumento(int id, string name) : base(id, name) { }

        //public static StatusDocumento TryParse(string compare)
        //{
        //    if (compare == "Outros")
        //    {
        //        return Outros;

        //    }
        //    else if (compare == "Contrato")
        //    {
        //        return Contrato;

        //    }

        //    throw new NotImplementedException();
        //}

        //public static string TryParseType(StatusDocumento compare)
        //{
        //    if (compare.DisplayName == "Outros")
        //    {
        //        return "Outros";

        //    }
        //    else if (compare.DisplayName == "Contrato")
        //    {
        //        return "Contrato";

        //    }

        //    throw new NotImplementedException();
        //}
    }
}