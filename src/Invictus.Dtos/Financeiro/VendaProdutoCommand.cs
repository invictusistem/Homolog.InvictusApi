using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class VendaProdutoCommand
    {
        public Guid bancoId { get; set; }
        public string digitosCartao { get; set; }
        public Guid formaRecebimentoId { get; set; }
        public bool parcelar { get; set; }
        public int parcelas { get; set; }
        public List<ProdutoDto> produtos { get; set; }
        public decimal valorReceber { get; set; }
        public decimal valorRecebido { get; set; }
        public Guid matriculaId { get; set; }
    }
}
