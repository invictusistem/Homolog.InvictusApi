namespace Invictus.Core.Enums
{
    public class StatusPagamento : Enumeration
    {
        public static StatusPagamento EmAberto = new(1, "Em aberto");
        public static StatusPagamento Pago = new(2, "Pago");
        public static StatusPagamento Cancelado = new(3, "Cancelado");
        public static StatusPagamento Suspenso = new(4, "Suspenso");
        public static StatusPagamento Reparcelado = new(5, "Reparcelado");
        public static StatusPagamento Vencido = new(6, "Vencido");
        public StatusPagamento()
        {

        }
        public StatusPagamento(int id, string name) : base(id, name) { }
    }
}
