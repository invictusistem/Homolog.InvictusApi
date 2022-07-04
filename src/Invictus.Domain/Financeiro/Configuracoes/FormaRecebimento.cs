using Invictus.Core;
using System;

namespace Invictus.Domain.Financeiro.Configuracoes
{
    public class FormaRecebimento : Entity
    {
        public FormaRecebimento(string descricao,
                                  bool ativo,
                                  bool ehCartao
                                    )
        {
            Descricao = descricao;
            Ativo = ativo;
            EhCartao = ehCartao;
        }

        public FormaRecebimento(string descricao,
                                bool ativo,
                                bool ehCartao,
                                int? diasParaCompensacao,
                                decimal? taxa,
                                bool? permiteParcelamento,
                                Guid? bancoPermitidoParaCreditoId,
                                Guid? subcontaTaxaVinculadaId,
                                Guid? fornecedorTaxaVinculadaId,
                                Guid? centroDeCustoTaxaVinculadaId,
                                Guid? compensarAutomaticamenteId,
                                Guid unidadeId
                                )
        {
            Descricao = descricao;
            Ativo = ativo;
            EhCartao = ehCartao;
            DiasParaCompensacao = diasParaCompensacao;
            Taxa = taxa;
            PermiteParcelamento = permiteParcelamento;
            BancoPermitidoParaCreditoId = bancoPermitidoParaCreditoId;
            SubcontaTaxaVinculadaId = subcontaTaxaVinculadaId;
            FornecedorTaxaVinculadaId = fornecedorTaxaVinculadaId;
            CentroDeCustoTaxaVinculadaId = centroDeCustoTaxaVinculadaId;
            CompensarAutomaticamenteId = compensarAutomaticamenteId;
            UnidadeId = unidadeId;
        }

        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public bool EhCartao { get; private set; }
        public bool Dinheiro { get; private set; }
        public int? DiasParaCompensacao { get; private set; }
        public decimal? Taxa { get; private set; }
        public bool? PermiteParcelamento { get; private set; }
        public bool CompensacaoAutomatica { get; private set; }
        public Guid? BancoPermitidoParaCreditoId { get; private set; }
        public Guid? SubcontaTaxaVinculadaId { get; private set; }
        public Guid? FornecedorTaxaVinculadaId { get; private set; }
        public Guid? CentroDeCustoTaxaVinculadaId { get; private set; }
        public Guid? CompensarAutomaticamenteId { get; private set; }
        public Guid UnidadeId { get; private set; }

        protected FormaRecebimento()
        {

        }

    }
}
