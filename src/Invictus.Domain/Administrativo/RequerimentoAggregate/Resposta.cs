using Invictus.Core;
using System;

namespace Invictus.Domain.Administrativo.RequerimentoAggregate
{
    public class Resposta : Entity
    {

        public DateTime DataResposta { get; private set; }
        public string Observacao { get; private set; }
        public Guid ResponsavelId { get; private set; }
        public string Documento64 { get; private set; }
        public Guid RequerimentoId { get; private set; }

        protected Resposta()
        {

        }
    }
}
