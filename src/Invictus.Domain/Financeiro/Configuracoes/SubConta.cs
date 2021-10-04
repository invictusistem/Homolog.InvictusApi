using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Configuracoes
{
    public class SubConta
    {
        public SubConta()
        {

        }

        public SubConta(string descricao)
        {
            Descricao = descricao;
        }
        public int Id { get; private set; }
        public string Descricao { get; private set; }

    }
}
