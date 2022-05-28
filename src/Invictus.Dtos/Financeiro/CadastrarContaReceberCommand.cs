using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro
{
    public class CadastrarContaReceberCommand
    {
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }
        public bool ehFornecedor { get; set; }
        public bool ehColaborador { get; set; }
        public Guid pessoaId { get; set; }
        public string historico { get; set; }
        public Guid? meioPgmId { get; set; }
        public Guid? centrocustoId { get; set; }
    public Guid? subcontaId { get; set; }
        public Guid? bancoId { get; set; }
    }
}
