using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Configuracoes
{
    public class CentroCusto : Entity
    {
        public CentroCusto(string descricao,
                        bool ativo,
                        bool alertaMediaGastos
                        )
        {
            Descricao = descricao;
            Ativo = ativo;
            AlertaMediaGastos = alertaMediaGastos;

        }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public bool AlertaMediaGastos { get; private set; }
        public Guid UnidadeId { get; private set; }

        protected CentroCusto()
        {

        }
    }
}
