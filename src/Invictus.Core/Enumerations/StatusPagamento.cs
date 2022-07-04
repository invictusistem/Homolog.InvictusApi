using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class StatusPagamento : Enumeration
    {
        public static StatusPagamento EmAberto = new(1, "Em aberto");
        public static StatusPagamento Pago = new(2, "Pago"); // pago mas não confirmado
        public static StatusPagamento Cancelado = new(3, "Cancelado");
        public static StatusPagamento Suspenso = new(4, "Suspenso");
        public static StatusPagamento Reparcelado = new(5, "Reparcelado");
        public static StatusPagamento Vencido = new(6, "Vencido");
        public static StatusPagamento Quitado = new(7, "Quitado");
        public static StatusPagamento Confirmado = new(8, "Confirmado"); // Confirmado pagamento
        public static StatusPagamento Estornado = new(9, "Estornado");
        public StatusPagamento()
        {

        }
        public StatusPagamento(int id, string name) : base(id, name) { }
    }
}
