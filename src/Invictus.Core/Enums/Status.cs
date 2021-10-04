namespace Invictus.Core.Enums
{
    public class Status : Enumeration
    {
        public static Status AguardandoInicio = new(1, "Aguardando início");
        public static Status EmAndamento = new(2, "Em andamento");
        public static Status Suspensa = new(3, "Suspensa");
        public static Status Encerrada = new(4, "Encerrada");
        public static Status Cancelada = new(5, "Cancelada");
        public Status()
        {

        }
        public Status(int id, string name) : base(id, name) { }
    }
}
