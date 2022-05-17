using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.Financeiro.Configuracoes
{
    public class FormaRecebimentoDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; }
        public bool ehCartao { get; set; }
        public int? diasParaCompensacao { get; set; }
        public decimal? taxa { get; set; }
        public bool? permiteParcelamento { get; set; }
        public Guid? bancoPermitidoParaCreditoId { get; set; }
        public Guid? subcontaTaxaVinculadaId { get; set; }
        public Guid? fornecedorTaxaVinculadaId { get; set; }
        public Guid? centroDeCustoTaxaVinculadaId { get; set; }
        public Guid? compensarAutomaticamenteId { get; set; }
        public Guid unidadeId { get; set; }
    }
}
