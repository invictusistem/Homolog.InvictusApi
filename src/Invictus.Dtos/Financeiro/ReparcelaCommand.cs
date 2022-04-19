using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class ReparcelaCommand
    {

        public decimal valorEntrada { get; set; }
        public Guid[] debitosIds { get; set; }
        public List<Reparcelas> parcelas { get; set; }
    }

    public class Reparcelas
    {
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }
    }
}
