using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class ReceberBoletoCommand
    {
        public Guid boletoId { get; set; }
        public decimal valorReceber { get; set; }
        public decimal valorRecebido { get; set; }
        //public string formaRecebimento { get; set; }
        public Guid formaRecebimentoId { get; set; }
        public Guid bancoId { get; set; }
        //public string transferencia { get; set; }
        public string digitosCartao { get; set; }
    }
}
