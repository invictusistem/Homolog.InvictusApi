using Invictus.Core.Enumerations;
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
        public string Evento { get; private set; } // Edição, Pagamento, Recebimento, Exclusão, Criação 
        public string ProdutosVenda { get; private set; }
        public Guid? UserId { get; private set; } 
        public DateTime DataCriacao { get; private set; }

        public static LogBoletos BoletoLog(Guid boletoId, EventoBoletoLog evento, Guid userId)
        {
            var log = new LogBoletos()
            {
                BoletoId = boletoId,
                Evento = evento.DisplayName,
                UserId = userId,
                DataCriacao = DateTime.Now
            };

            return log;
        }


        public static LogBoletos BoletoProdutoVendaLog(Guid boletoId, string produtosJson, EventoBoletoLog evento, Guid userId)
        {
            var log = new LogBoletos()
            {
                BoletoId = boletoId,
                Evento = evento.DisplayName,
                UserId = userId,
                DataCriacao = DateTime.Now,
                ProdutosVenda = produtosJson
            };

            return log;
        }
        public LogBoletos()
        {

        }
    }
}
