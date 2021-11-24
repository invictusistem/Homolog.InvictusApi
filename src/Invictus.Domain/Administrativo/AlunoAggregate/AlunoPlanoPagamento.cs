using Invictus.Core;
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.AlunoAggregate
{
    public class AlunoPlanoPagamento : Entity, IAggregateRoot
    {
        public AlunoPlanoPagamento(//int id,
                            string descricao,
                            decimal valor,
                            decimal taxaMatricula,
                            int parcelas,
                            bool materialGratuito,
                            decimal valorMaterial,
                            decimal bonusPontualidade,
                            Guid matriculaId
                            // Guid typePacoteId

                            )
        {
            // TypePacoteId = typePacoteId;
            Descricao = descricao;
            Valor = valor;
            TaxaMatricula = taxaMatricula;
            Parcelas = parcelas;
            MaterialGratuito = materialGratuito;
            ValorMaterial = valorMaterial;
            BonusPontualidade = bonusPontualidade;
            MatriculaId = matriculaId;


        }

        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public decimal TaxaMatricula { get; private set; }
        public int Parcelas { get; private set; }
        public bool MaterialGratuito { get; private set; }
        public decimal ValorMaterial { get; private set; }
        public decimal BonusPontualidade { get; private set; }
        // public Guid UnidadeId { get; private set; }
        // public Guid ContratoId { get; private set; }
        public Guid MatriculaId { get; private set; }
        // public Guid TypePacoteId { get; private set; } //Criar SERÁ O TIPO


        #region EF
        public AlunoPlanoPagamento()
        {

        }
        #endregion
    }
}
