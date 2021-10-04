using Invictus.Core.Enums;
using System;


namespace Invictus.Domain.Financeiro.Fornecedor
{
    public class FornecedorSaida
    {
        public FornecedorSaida() { }
        public FornecedorSaida(long id,
        DateTime dataCriacao,
        DateTime dataVencimento,
        decimal valor,
        //decimal valorPago,
        //DateTime diaPagamento,
        MeioPagamento meioPagamento,
        StatusPagamento statusPagamento,
        string comentario,
        string descricaoTransacao,
        long fornecedorId,
        int unidadeId

        )
        {
            Id = id;
            DataCriacao = dataCriacao;
            DataVencimento = dataVencimento;
            Valor = valor;
            //ValorPago = valorPago;
           // DiaPagamento = diaPagamento;
            MeioPagamento = meioPagamento.DisplayName;
            StatusPagamento = statusPagamento.DisplayName;
            DescricaoTransacao = descricaoTransacao;
            Comentario = comentario;
            FornecedorId = fornecedorId;
            UnidadeId = unidadeId;


        }
        public long Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public decimal Valor { get; private set; }
        public decimal ValorPago { get; private set; }
        public DateTime? DiaPagamento { get; private set; }
        public string MeioPagamento { get; private set; }
        public string StatusPagamento { get; private set; }
        public string DescricaoTransacao { get; private set; }
        public string Comentario { get; private set; }
        public long FornecedorId { get; private set; }
        public int UnidadeId { get; private set; }

    }
}
