namespace Invictus.Core.Enums
{
    //public enum TipoResponsavel
    //{
    //    ResponsavelFinanceiro,
    //    ResponsavelMenor
    //}

    public class TipoResponsavel : Enumeration
    {
        public static TipoResponsavel ResponsavelFinanceiro = new(1, "Responsável financeiro");
        public static TipoResponsavel ResponsavelMenor = new(2, "Responsável menor");

        public TipoResponsavel()
        {

        }
        public TipoResponsavel(int id, string name) : base(id, name) { }
    }

}
