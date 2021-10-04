using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Models
{
    public class SenhaBolsas
    {
        public SenhaBolsas(int emissorId,
                            string pacoteId,
                            decimal percentualBolsa
                            )
        {
            EmissorId = emissorId;
            PacoteId = pacoteId;
            PercentualBolsa = percentualBolsa;
            DataExpiracao = GerarDatas();

        }
        public int Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataExpiracao { get; private set; }
        public int EmissorId { get; private set; }
        public string PacoteId { get; private set; }
        public decimal PercentualBolsa { get; private set; }

        public DateTime GerarDatas()
        {
            DataCriacao = DateTime.Now;

            DataExpiracao = DataCriacao.AddDays(1);

            return DataCriacao;

        }
    }
}
