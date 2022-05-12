using System;

namespace Invictus.Dtos.Financeiro.Configuracoes
{
    public class SubContaDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public string tipo { get; set; }
        public bool ativo { get; set; }
        public Guid planoContaId { get; set; }
        //public virtual PlanoConta PlanoConta { get; set; }

    }



}
