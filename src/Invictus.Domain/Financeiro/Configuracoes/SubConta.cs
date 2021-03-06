using Invictus.Core;
using System;

namespace Invictus.Domain.Financeiro.Configuracoes
{
    public class SubConta : Entity
    {
        public SubConta(string descricao,
                        string tipo,
                        bool ativo)
        {
            Descricao = descricao;
            Tipo = tipo;
            Ativo = ativo;
            //UnidadeId = unidadeId;
        }
        public string Descricao { get; private set; }
        public string Tipo { get; private set; }
        public bool Ativo { get; private set; }
        public Guid PlanoContaId { get; private set; }
       //public Guid UnidadeId { get; private set; }
        public virtual PlanoConta PlanoConta { get; private set; }

        public void Atualizar(string valor)
        {
            if(valor == "debito")
            {
                Tipo = "Débito";
            }

            if(valor == "credito")
            {
                Tipo = "Crédito";
            }
        }
        protected SubConta()
        {

        }
    }
}
