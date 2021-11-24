using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Parametros
{
    public class ParametrosValue : Entity
    {
        public ParametrosValue(string value,
                                string descricao,
                                string comentario,
                                Guid parametrosKeyId
)
        {
            Value = value;
            Descricao = descricao;
            Comentario = comentario;
            ParametrosKeyId = parametrosKeyId;
        }
       // public int Id { get; private set; }
        public string Value { get; private set; }
        public string Descricao { get; private set; }
        public string Comentario { get; private set; }
        


        #region EF
        public virtual ParametrosKey  ParametrosKey { get; private set; }
        public Guid ParametrosKeyId { get; private set; }
        public ParametrosValue() { }

        #endregion
    }
}
