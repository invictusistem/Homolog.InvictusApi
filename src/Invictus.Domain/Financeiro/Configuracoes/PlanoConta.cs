using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Configuracoes
{
    public class PlanoConta : Entity
    {
        public PlanoConta(string descricao,
                          bool ativo,
                          Guid unidadeId
                            )
        {
            Descricao = descricao;
            Ativo = ativo;
            UnidadeId = unidadeId;
            Subcontas = new List<SubConta>();
        }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UnidadeId { get; private set; }
        public IEnumerable<SubConta> Subcontas { get; private set; }

        protected PlanoConta()
        {

        }
    }
}
