using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro.Configuracoes
{
    public class BancoDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public bool ehCaixaEscola { get; set; }
        public string agencia { get; set; }
        public string agenciaDigito { get; set; }
        public string conta { get; set; }
        public string contaDigito { get; set; }
        public bool ativo { get; set; }
        public DateTime dataCadastro { get; set; }
        public bool utilizadoParaImpressao { get; set; }
        public Guid unidadeId { get; set; }
    }



}
