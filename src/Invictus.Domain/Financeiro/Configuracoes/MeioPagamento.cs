using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Configuracoes
{
    public class MeioPagamento : Entity
    {
        public MeioPagamento(string descricao,
                            bool ativo
                            )
        {
            Descricao = descricao;
            Ativo = ativo;
        }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }

        protected MeioPagamento()
        {

        }
    }
}
