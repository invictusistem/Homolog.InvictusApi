using System;

namespace Invictus.Domain.Administrativo.Logs
{
    public class LogBoletos
    {
        public LogBoletos(Guid boletoId,
                          string boletoJson,
                          DateTime dataCriacao
                        )
        {   
            BoletoId = boletoId;
            BoletoJson = boletoJson;
            DataCriacao = dataCriacao;
        }

        public long Id { get; private set; }
        public Guid BoletoId { get; private set; }
        public string BoletoJson { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public LogBoletos()
        {

        }
    }
}
