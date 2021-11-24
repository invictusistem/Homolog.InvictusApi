using Invictus.Core;
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Models
{
    public class PlanoPagamentoTemplate : Entity, IAggregateRoot
    {
        public PlanoPagamentoTemplate(//int id,
                            Guid typePacoteId,
                            string descricao,
                            decimal valor,
                            decimal taxaMatricula,
                            //string parcelamento,
                            bool materialGratuito,
                            decimal valorMaterial,
                            decimal bonusPontualidade,
                            Guid contratoId,
                            bool ativo
                            )
        {
            TypePacoteId = typePacoteId;
            Descricao = descricao;
            Valor = valor;
            TaxaMatricula = taxaMatricula;
            //Parcelamento = parcelamento;
            MaterialGratuito = materialGratuito;
            ValorMaterial = valorMaterial;
            BonusPontualidade = bonusPontualidade;
            ContratoId = contratoId;
            Ativo = ativo;

        }
        
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public decimal TaxaMatricula { get; private set; }
       // public string Parcelamento { get; private set; }
        public bool MaterialGratuito { get; private set; }
        public decimal ValorMaterial { get; private set; }
        public decimal BonusPontualidade { get; private set; }
       // public Guid UnidadeId { get; private set; }
        public Guid ContratoId { get; private set; }
        public Guid TypePacoteId { get; private set; } //Criar SERÁ O TIPO
        public bool Ativo { get; private set; }

        #region EF
        public PlanoPagamentoTemplate()
        {

        }
        #endregion
    }
}
