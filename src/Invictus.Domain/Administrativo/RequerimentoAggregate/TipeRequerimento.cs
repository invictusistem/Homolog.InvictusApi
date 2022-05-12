using Invictus.Core;

namespace Invictus.Domain.Administrativo.RequerimentoAggregate
{
    public class TipoRequerimento : Entity
    {
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public string Observacao { get; private set; }

        protected TipoRequerimento()
        {

        }
    }
}
