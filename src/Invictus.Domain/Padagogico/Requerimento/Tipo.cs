
using Invictus.Core;
using System;

namespace Invictus.Domain.Padagogico.Requerimento
{
    public class Tipo : Entity
    {
        public Tipo(string descricao,
                    bool ativo,
                    Guid categoriaId)
        {
            Descricao = descricao;
            Ativo = ativo;
            CategoriaId = categoriaId;

        }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public Guid CategoriaId { get; private set; }
        public virtual Categoria Categoria { get; private set; }

        protected Tipo()
        {

        }

    }

}

