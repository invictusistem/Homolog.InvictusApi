using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class ProdutoVendasViewModel
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public int qntItems { get; set; }
        public decimal valorTotal { get; set; }
        public int parcelas { get; set; }
        public string meioPagamento { get; set; }
        public string infoItems { get; set; }
        public DateTime dataVenda { get; set; }

    }
}
