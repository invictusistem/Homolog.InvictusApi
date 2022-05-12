using Invictus.Core;
using System;

namespace Invictus.Domain.Administrativo.Parametros
{
    public class ParametrosValue : Entity
    {
        public ParametrosValue(string value,
                                string descricao,
                                string comentario,
                                bool ativo,
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
        public bool Ativo { get; private set; }
        public Guid ParametrosKeyId { get; private set; }

        public void SetValue(string value)
        {
            Value = value;
        }


        #region EF
        public virtual ParametrosKey  ParametrosKey { get; private set; }
        protected ParametrosValue() { }

        #endregion
    }
}
