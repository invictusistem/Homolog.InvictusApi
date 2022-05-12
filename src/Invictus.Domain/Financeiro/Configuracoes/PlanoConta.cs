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


    public class SubConta : Entity
    {
        public SubConta(string descricao,
                        string tipo,
                        bool ativo)
        {
            Descricao = descricao;
            Tipo = tipo;
            Ativo = ativo;

        }
        public string Descricao { get; private set; }
        public string Tipo { get; private set; }
        public bool Ativo { get; private set; }
        public Guid PlanoContaId { get; private set; }
        public virtual PlanoConta PlanoConta { get; private set; }

        protected SubConta()
        {

        }
    }
}
