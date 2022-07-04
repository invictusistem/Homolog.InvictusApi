using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class EventoBoletoLog : Enumeration
    {
        public static EventoBoletoLog Edicao = new(1, "Edição");
        public static EventoBoletoLog Pagamento = new(2, "Pagamento");
        public static EventoBoletoLog Compensacao = new(3, "Compensação");
        public static EventoBoletoLog Recebimento = new(4, "Recebimento");
        public static EventoBoletoLog Exclusao = new(5, "Exclusão");
        public static EventoBoletoLog Criacao = new(6, "Criação");
        public static EventoBoletoLog Estorno = new(7, "Estorno");

        public EventoBoletoLog() { }
        public EventoBoletoLog(int id, string name) : base(id, name) { }

        public static EventoBoletoLog TryParse(string compare)
        {
           

            throw new NotImplementedException();
        }
    }
}

// Edição, Pagamento, Recebimento, Exclusão, Criação
