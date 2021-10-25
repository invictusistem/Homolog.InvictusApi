using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Administrativo
{
    public class ProdutoViewModel
    {
        public int id { get; set; }
        public string codigoProduto { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal preco { get; set; }
        public int quantidade { get; set; }
        public int nivelMinimo { get; set; }
        public int unidadeId { get; set; }
        public DateTime dataCadastro { get; set; }
        public string observacoes { get; set; }
        public string unidade { get; set; }
    }
}
