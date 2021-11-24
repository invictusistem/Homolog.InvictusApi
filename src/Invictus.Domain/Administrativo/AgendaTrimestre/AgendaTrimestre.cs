using Invictus.Core;
using System;


namespace Invictus.Domain.Administrativo.Models
{
    public class AgendaTrimestre : Entity
    {

        public AgendaTrimestre(DateTime inicio,
                               DateTime fim,
                               Guid unidadeId)
        {
            Inicio = inicio;
            Fim = fim;
            UnidadeId = unidadeId;

        }
        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }
        public Guid UnidadeId { get; private set; }


    }
}
