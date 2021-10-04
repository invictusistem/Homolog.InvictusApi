namespace Invictus.Core.Enums
{
    public class Previsao : Enumeration
    {
        public static Previsao PrimeiraPrevisao = new(1, "1ª previsão");
        public static Previsao SegundaPrevisao = new(2, "2ª previsão");
        public static Previsao TerceiraPrevisao = new(3, "3ª previsão");
        
        public Previsao()
        {

        }
        public Previsao(int id, string name) : base(id, name) { }
    }
}
