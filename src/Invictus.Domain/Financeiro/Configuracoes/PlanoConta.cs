using Invictus.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Configuracoes
{
    public class PlanoConta : Entity
    {
        public PlanoConta(string descricao,
                          bool ativo
                            )
        {
            Descricao = descricao;
            Ativo = ativo;
            Subcontas = new List<SubConta>();
        }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public IEnumerable<SubConta> Subcontas { get; private set; }

        protected PlanoConta()
        {

        }
    }
}
