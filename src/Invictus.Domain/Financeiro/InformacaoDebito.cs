using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Financeiro
{
    public class InformacaoDebito : Entity
    {
        public InformacaoDebito(
                        int numeroParcelas,
                        decimal valorTotal,
                        string historico,
                        StatusPagamento status,
                        DebitoOrigem origem,
                        Guid unidadeCusto,
                        string subConta,
                        Guid matriculaId,
                        DateTime dataCadastro)
        {
            NumeroParcelas = numeroParcelas;
            ValorTotal = valorTotal;
            Historico = historico;
            StatusPagamento = status.DisplayName;
            Origem = origem.DisplayName;
            UnidadeCusto = unidadeCusto;
            SubConta = subConta;
            MatriculaId = matriculaId;
            DataCadastro = dataCadastro;
            Boletos = new List<Boleto>();

        }
       
        public int NumeroParcelas { get; private set; }
        public decimal ValorTotal { get; private set; }        
        public string Historico { get; private set; }
        public string StatusPagamento { get; private set; } // Pago, cancelado, em aberto
        public string Origem { get; private set; } // Enum.. se for de curso colocar curso etc...que ai busca débito só do curso etc.. ou só de trans
        public string Tipo { get; private set; }
        public Guid UnidadeCusto { get; private set; }
        public string SubConta { get; private set; }
        // NEW
        public Guid MatriculaId { get; private set; } // quer saber o curso etc? ver aqui
        public Guid FornecedorId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public ICollection<Boleto> Boletos { get; private set; }
       
        public void AddBoletos(List<Boleto> boletos)
        {
            Boletos = boletos;
        }

        #region EF
        public InformacaoDebito()
        {

        }

        #endregion
       
        
        /*
         
         Guid
nº lançamento
nº doc
parcelas (default 22)
valor
valorPago
diaVencimento (default dia 10) primeiro dia vnecimento = próximo dia 10
dataPagamento
historio (ex: mansalidade 01/22)
status (em aberto, pago, cancelado)
matriculaId (quer saber qual curso, qual aluno etcestamos na outra sala... etc? só ver pelo matrícula ID
unidadeCusto ???
formaPgm
subconta
boletoId
origem (criar padrao de string)
         
         */
    }
}
