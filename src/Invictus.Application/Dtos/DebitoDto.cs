using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class DebitoDto
    {
        public int id { get; set; }
        public DateTime competencia { get; set; }
        public int idUnidadeResponsavel { get; set; }
        public string status { get; set; }
        public decimal valorTitulo { get; set; }
        public decimal valorPago { get; set; }
        public DateTime dataVencimento { get; set; }
        public DateTime dataPagamento { get; set; }
        public string meioPagamento { get; set; }
        public int boletoId { get; set; }
        public int transacaoId { get; set; }
        public string descricao { get; set; }
    }
}
