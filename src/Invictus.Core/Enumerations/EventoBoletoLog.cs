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
        public static EventoBoletoLog Recebimento = new(3, "Recebimento");
        public static EventoBoletoLog Exclusao = new(4, "Exclusão");
        public static EventoBoletoLog Criacao = new(5, "Criação");

        public EventoBoletoLog() { }
        public EventoBoletoLog(int id, string name) : base(id, name) { }

        public static EventoBoletoLog TryParse(string compare)
        {
           

            throw new NotImplementedException();
        }
    }
}

// Edição, Pagamento, Recebimento, Exclusão, Criação
