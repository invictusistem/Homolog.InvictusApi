using Invictus.Core;
using System;

namespace Invictus.Domain.Padagogico.Estagio
{
    public class TypeEstagio : Entity
    {
        public TypeEstagio(
            Guid id,
            string nome,
            string observacao,
            string nivel,
            bool ativo
            )
        {
            Id = id;
            Nome = nome;
            Observacao = observacao;
            Nivel = nivel;
            Ativo = ativo;

        }
        public string Nome { get; private set; }
        public string Nivel { get; private set; }
        public bool Ativo { get; private set; }
        public string Observacao { get; private set; }

        #region EF 

        protected TypeEstagio()
        {

        }

        #endregion
    }
}