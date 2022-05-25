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
                            bool ativo,
                            Guid unidadeId
                            )
        {
            Descricao = descricao;
            Ativo = ativo;
            UnidadeId = unidadeId;
        }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UnidadeId { get; private set; }

        protected MeioPagamento()
        {

        }
    }
}
