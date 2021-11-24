using Invictus.Core;
using Invictus.Domain.Administrativo.ContratosAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.ContratoAggregate
{
    public class Conteudo : Entity
    {
        public Conteudo(int order,
                        string content
                        )
        {
            Order = order;
            Content = content;

        }
        
        public int Order { get; private set; }
        public string Content { get; private set; }
        public Guid ContratoId { get; private set; }

        public void SetContratoId(Guid contratId)
        {
            ContratoId = contratId;
        }
        #region EF
        public virtual Contrato Contrato { get; private set; }
        public Conteudo()
        {

        }

        #endregion
    }
}
