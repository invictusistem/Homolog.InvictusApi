using Invictus.Core;
using Invictus.Core.Interfaces;
using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.Parametros
{
    public class ParametrosKey : Entity, IAggregateRoot
    {
        public ParametrosKey(string key,
                              string descricao,
                              bool ativo
)
        {
            Key = key;
            Descricao = descricao;
            Ativo = ativo;
            ParametrosValue = new List<ParametrosValue>();
        }
       // public int Id { get; private set; }
        public string Key { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public IEnumerable<ParametrosValue> ParametrosValue { get; private set; }



        #region EF

        public ParametrosKey() { }
        #endregion
    }
}
