using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.ContratosAggregate
{
    public class Conteudo
    {
        public Conteudo(int order,
                        string content
                        )
        {
            Order = order;
            Content = content;

        }
        public int Id { get; private set; }
        public int Order { get; private set; }
        public string Content { get; private set; }
        public long ContratoId { get; private set; }
        public virtual Contrato Contrato { get; private set; }
    }
}
