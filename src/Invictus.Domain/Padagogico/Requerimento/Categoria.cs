
using Invictus.Core;
using System.Collections.Generic;

namespace Invictus.Domain.Padagogico.Requerimento
{

    public class Categoria : Entity
    {
        public Categoria(string descricao,
                        bool ativo
                        )
        {
            Descricao = descricao;
            Ativo = ativo;

        }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public IEnumerable<Tipo> Tipos { get; private set; }

        protected Categoria()
        {

        }
    }
}

