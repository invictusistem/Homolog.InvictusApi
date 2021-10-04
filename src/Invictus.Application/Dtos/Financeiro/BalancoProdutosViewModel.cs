using System;

namespace Invictus.Application.Dtos.Financeiro
{
    public class BalancoProdutosViewModel
    {
        public string nome { get; set; }
        public int quantidade { get; set; }
        public decimal valorUnitario { get; set; }
        public decimal valorTotal { get; set; }
        public DateTime dataVenda { get; set; }
        public string MeioPagamento { get; set; }
        public int parcelas { get; set; }

    }
}
