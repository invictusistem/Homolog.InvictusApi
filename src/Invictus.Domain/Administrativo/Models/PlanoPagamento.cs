using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Models
{
    public class PlanoPagamento
    {
        public PlanoPagamento(//int id,
                            int pacoteId,
                            string descricao,
                            decimal valor,
                            decimal taxaMatricula,
                            string parcelamento,
                            bool materialGratuito,
                            decimal bonusMensalidade,
                            int contratoId,
                            bool ativo
                            )
        {
            PacoteId = pacoteId;
            Descricao = descricao;
            Valor = valor;
            TaxaMatricula = taxaMatricula;
            Parcelamento = parcelamento;
            MaterialGratuito = materialGratuito;
            BonusMensalidade = bonusMensalidade;
            ContratoId = contratoId;
            Ativo = ativo;

        }

        public int Id { get; private set; }
        public int PacoteId { get; private set; } //Criar SERÁ O TIPO
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public decimal TaxaMatricula { get; private set; }
        public string Parcelamento { get; private set; }
        public bool MaterialGratuito { get; private set; }
        public decimal BonusMensalidade { get; private set; }
        public int UnidadeId { get; private set; }
        public int ContratoId { get; private set; }
        public bool Ativo { get; private set; }
    }
}
